using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Utility.Stock
{
    class StockDataCalc
    {
        private StockDataCalc()
        {
        }

        /// <summary>
        /// End price compare to start
        /// </summary>
        /// <param name="stock">Stock data of one day</param>
        /// <returns>percent. maybe negtive</returns>
        public static double GetRisePercent(IStockData stock)
        {
            return GetRisePercent(stock.StartPrice, stock.EndPrice);
        }

        public static double GetRisePercent(double basePrice, double anotherPrice)
        {
            return (anotherPrice - basePrice) / basePrice;
        }

        public static bool IsInRange(double val1, double val2)
        {
            if (val1 > val2 * (1 + COMPRATIO))
            {
                return false;
            }

            if (val1 < val2 * (1 - COMPRATIO))
            {
                return false;
            }

            return true;
        }

        public static double GetAveragePrice(IStockData sd)
        {
            return (sd.StartPrice + sd.EndPrice + sd.MaxPrice + sd.MinPrice) / 4;
        }

        private const double COMPRATIO = 0.1; // 表示每日上涨或者下跌的最大允许值 
    }
}
