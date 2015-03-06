using System;
using System.Collections.Generic;
using FinanceAnalyzer.Business.Shape;
using FinanceAnalyzer.Display;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// The main stock data storage
    /// 保存一只股票一段时间内的历史信息
    /// </summary>
    public class StockHistory : IStockHistory
    {
        public DateTime MaxDate
        {
            get
            {
                return MaxDate_;
            }
            set
            {
                MaxDate_ = value;
            }
        }

        public DateTime MinDate
        {
            get
            {
                return MinDate_;
            }
            set
            {
                MinDate_ = value;
            }
        }

        public int MinDateId
        {
            get;
            set;
        }

        public int MaxDateId
        {
            get;
            set;
        }

        public DateTime FindDateTime(int dateId)
        {
            return convertor_.GetIDDate(dateId);
        }

        public int StockId
        {
            get;
            set;
        }               

        /// <summary>
        /// Add stock data to this stock history
        /// </summary>
        /// <param name="stockId">Specified stock ID</param>
        /// <param name="arr">Stock data</param>
        public void InitAllStocks(int stockId, IEnumerable<StockData> arr)
        {
            Clear();

            StockId = stockId;

            foreach (StockData val in arr)
            {
                AddStock(DateFunc.ConvertToLocal(val.TradeDate), val);
            }

            InitDateIdtoStock();            
        }

        private void InitDateIdtoStock()
        {
            // Init Date ID to Stock
            convertor_.Init(DailyStocks_.Keys);
            foreach (var item in DailyStocks_)
            {
                DateIDtoStocks_.Add(convertor_.GetDateID(item.Key), item.Value);
            }

            MinDateId = convertor_.MinDateId;
            MaxDateId = convertor_.MaxDateId;
        }

        public void AddStock(DateTime dt, IStockData stock)
        {
            if (!DailyStocks_.ContainsKey(dt))
            {
                DailyStocks_.Add(dt, stock);
            }
            else
            {
                // ???
                // 2009-08-25
            }

            MinDate = (MinDate > dt) ? dt : MinDate;
            MaxDate = (MaxDate < dt) ? dt : MaxDate;
        }

        public void Clear()
        {
            DailyStocks_.Clear();
            DateIDtoStocks_.Clear();

            MaxDate_ = new DateTime(1900, 1, 1);
            MinDate_ = new DateTime(2100, 1, 1);

            StockId = -1;
        }

        // 得到某一天的股票属性
        public IStockData GetStock(DateTime dt)
        {
            if (!DailyStocks_.ContainsKey(dt))
            {
                return null;
            }
            else
            {
                return DailyStocks_[dt];
            }
        }

        // 得到某一天的股票属性
        public IStockData GetStock(int id)
        {
            if (!DateIDtoStocks_.ContainsKey(id))
            {
                return null;
            }
            else
            {
                return DateIDtoStocks_[id];
            }
        }

        public bool IsOperSuccess(int dt, StockOper oper)
        {
            if (oper == null)
            {
                return false;
            }

            if (oper.Type == OperType.NoOper)
            {
                return true;
            }

            if (!DateIDtoStocks_.ContainsKey(dt))
            {
                return false;
            }

            IStockData stock = DateIDtoStocks_[dt];
            return IsValidOperation(stock, oper);
        }

        static bool IsValidOperation(IStockData stock, StockOper oper)
        {
            switch (oper.Type)
            {
                case OperType.Buy:
                    return (oper.UnitPrice >= stock.MinPrice);
                case OperType.Sell:
                    return (oper.UnitPrice <= stock.MaxPrice);
                default:
                    return false;
            }
        }

        // 绘制股票曲线
        public IStockDrawer GetStockDrawer()
        {
            StockPropDrawer drawer = new StockPropDrawer();

            drawer.MaxDate = MaxDate_;
            drawer.MinDate = MinDate_;

            foreach (KeyValuePair<DateTime, IStockData> entry in DailyStocks_)
            {
                drawer.AddDayStock(entry.Key, entry.Value);
            }

            return drawer;
        }

        public void Check(ICustomLog log)
        {
            DateTime startDate = MinDate;
            while (startDate < MaxDate)
            {
                if ((GetStock(startDate) == null) && !Holidays.IsWeekend(startDate))
                {
                    log.LogInfo("Date: " + startDate.ToLongDateString() + ", has no stock data.");
                }
                startDate = startDate.AddDays(1);
            }

            JudgeShape(log);
        }

        public void JudgeShape(ICustomLog log)
        {
            int tTypeCount = 0;
            int revTTypeCount = 0;
            int crossTypeCount = 0;
            foreach (KeyValuePair<DateTime, IStockData> entry in DailyStocks_)
            {
                if (ShapeJudger.IsCross(entry.Value))
                {
                    crossTypeCount++;
                }

                if (ShapeJudger.IsT(entry.Value))
                {
                    tTypeCount++;
                }

                if (ShapeJudger.IsReverseT(entry.Value))
                {
                    revTTypeCount++;
                }
            }

            log.LogInfo("Total Count = " + DailyStocks_.Count
                + ", Cross = " + crossTypeCount
                + ", T = " + tTypeCount
                + ", Rev T = " + revTTypeCount);
        }

        public IStockHistory GetPartStockHistory(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException();
            }

            IStockHistory hist = new StockHistory();
            DateTime currentDate = startDate;
            while (currentDate < endDate)
            {
                IStockData sd = GetStock(currentDate);
                if (sd != null)
                {
                    hist.AddStock(currentDate, sd);
                }
                currentDate = DateFunc.GetNextWorkday(currentDate);
            }

            return hist;
        }
        
        private Dictionary<DateTime, IStockData> DailyStocks_ = new Dictionary<DateTime, IStockData>();
        private Dictionary<int, IStockData> DateIDtoStocks_ = new Dictionary<int, IStockData>();

        DateIDConvertor convertor_ = new DateIDConvertor();

        private DateTime MaxDate_ = new DateTime(1900, 1, 1);
        private DateTime MinDate_ = new DateTime(2100, 1, 1);
    }
}
