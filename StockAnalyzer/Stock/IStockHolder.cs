using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// manage buyed stocks and bonuses
    /// </summary>
    public interface IStockHolder
    {
        bool HasStock();

        void BuyStock(int count, double unitPrice);

        void AddBonusStock(int count);

        int StockCount();

        // 股票市值
        double MarketValue(int dateIndex);

        // 返回值为获得的金额
        double SellStock(int count, double unitPrice);

        // 单位成本
        double UnitPrice
        {
            get;
        }

        IStockHistory History
        {
            get;
            set;
        }
    }
}
