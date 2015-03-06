using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using FinanceAnalyzer.DB;
using Stock.Common.Data;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer.Strategy.Impl
{
    public class StrategyMinMax : IFinanceStrategy
    {
        // 最简单的算法，指令为前一日最低值买入，最高值卖出
        public override ICollection<StockOper> GetOper(int day, IAccount account)
        {
            IStockData prevStock = stockHistory.GetStock(day - 1);
            if (prevStock == null)
            {
                Debug.WriteLine("StrategyMinMax -- GetPrevDayStock ERROR: Cur Day Index: " + day);
                //Debug.Assert(false);
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();
            int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    prevStock.EndPrice);
            if (stockCount > 0)
            {
                StockOper oper = new StockOper(prevStock.EndPrice, stockCount, OperType.Buy);
                opers.Add(oper);
            }

            if (stockHolder.HasStock())
            {
                StockOper oper2 = new StockOper(prevStock.EndPrice * 1.01, stockHolder.StockCount(), OperType.Sell);
                //StockOper oper2 = new StockOper(prevStock.MaxPrice, stockHolder.StockCount(), OperType.Sell);
                opers.Add(oper2);
            }

            return opers;
        }

        public override string Name
        {
            get
            {
                return "MinMax";
            }
        }
    }
}
