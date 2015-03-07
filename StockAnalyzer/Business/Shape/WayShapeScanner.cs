using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility.Stock;

namespace FinanceAnalyzer.Business.Shape
{
    class WayShapeScanner : IShapeScanner
    {
        public WayShapeScanner()
        {
        }

        public WayShapeScanner(double ratio)
        {
            riseRatio_ = ratio;
        }

        public OperType Analyse(IStockData stock, IStockData prevStock)
        {
            if ((stock == null) || (prevStock == null))
            {
                return OperType.NoOper;
            }

            double deltapercent = StockDataCalc.GetRisePercent(stock);

            double prevPercent = StockDataCalc.GetRisePercent(prevStock);

            // Only for converse
            if (NumbericHelper.IsSameSign(deltapercent, prevPercent))
            {
                return OperType.NoOper;
            }

            // ratio should be larger than yesterday
            if (Math.Abs(deltapercent) < Math.Abs(prevPercent))
            {
                return OperType.NoOper;
            }

            if ((deltapercent > riseRatio_) && (stock.EndPrice > prevStock.StartPrice))
            {
                return OperType.Buy;
            }

            if ((deltapercent < -riseRatio_) && (stock.EndPrice < prevStock.StartPrice))
            {
                return OperType.Sell;
            }

            return OperType.NoOper;
        }

        public string GetName()
        {
            return "WAY " + riseRatio_;
        }

        private double riseRatio_ = 0.02;
    }
}
