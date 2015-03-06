using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Log;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    public class StockHolder : IStockHolder
    {
        public StockHolder()
        {
        }

        public void BuyStock(int count, double unitPrice)
        {
            CurrentStock_.Buy(count, unitPrice);
        }

        public void AddBonusStock(int count)
        {
            BuyStock(count, 0);
        }

        public bool HasStock()
        {
            return CurrentStock_.Count > 0;
        }

        public int StockCount()
        {
            return CurrentStock_.Count;
        }

        public IStockHistory History
        {
            get
            {
                return StockHistory_;
            }
            set
            {
                StockHistory_ = value;
            }
        }

        // 返回值为获得的金额
        public double SellStock(int count, double unitPrice)
        {
            // 卖出股票
            if (CurrentStock_.Count == 0)
            {
                return 0;
            }

            double price = CurrentStock_.Sell(count, unitPrice);
            double cash = price - Transaction.GetDutyCharge(price);

            LogMgr.Logger.LogInfo("Action: Sell Stock Count: {0}, Unit Price: {1}",
                    count,
                    unitPrice);
            return cash;
        }

        // 股票市值
        public double MarketValue(int dateIndex)
        {
            IStockData stockProp = History.GetStock(dateIndex);
            if (stockProp == null)
            {
                stockProp = History.GetStock(dateIndex - 1);
            }

            if (stockProp != null)
            {
                return CurrentStock_.Count * stockProp.EndPrice;
            }
            else
            {
                return 0;
            }
        }

        // 单位成本
        public double UnitPrice
        {
            get
            {
                if (CurrentStock_.Count == 0)
                {
                    throw new NotSupportedException();
                }

                return CurrentStock_.UnitPrice;
            }
        }

        MarketStock CurrentStock_ = new MarketStock();
        private IStockHistory StockHistory_;
    }
}
