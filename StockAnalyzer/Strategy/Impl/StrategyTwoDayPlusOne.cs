using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.DB;
using FinanceAnalyzer.Strategy.Judger;

namespace FinanceAnalyzer.Strategy.Impl
{
    class StrategyTwoDayPlusOne : IFinanceStrategy
    {
        public override ICollection<StockOper> GetOper(int day, IAccount account)
        {
            IStockData curProp = stockHistory.GetStock(day);
            IStockData stockYesterdayProp = stockHistory.GetStock(day - 1);

            //DateTime prevDate = stockHistory.GetPreviousDay(day);
            IStockData stockprevProp = stockHistory.GetStock(day - 2);
            //DateTime prevNextDate = stockHistory.GetPreviousDay(prevDate);

            if (!CheckStock(curProp, day) || !CheckStock(stockYesterdayProp, day - 1)
                || !CheckStock(stockprevProp, day - 2))
            {
                return null;
            }

            ICollection<StockOper> opers = new List<StockOper>();

            // IsRise? Condition questionable. 
            if (
                StockJudger.IsRise(stockYesterdayProp, stockprevProp)
                && 
                StockJudger.IsUp(stockYesterdayProp)
                && StockJudger.IsUp(stockprevProp))
            {
                if (stockHolder.HasStock())
                {
                    StockOper oper = new StockOper(curProp.StartPrice, stockHolder.StockCount(), OperType.Sell);
                    opers.Add(oper);
                    return opers;
                }
            }
            else if (
                !StockJudger.IsRise(stockYesterdayProp, stockprevProp)
                && 
                !StockJudger.IsUp(stockYesterdayProp)
                && !StockJudger.IsUp(stockprevProp))
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                    curProp.StartPrice);
                if (stockCount > 0)
                {
                    StockOper oper = new StockOper(curProp.StartPrice, stockCount, OperType.Buy);
                    opers.Add(oper);
                    return opers;
                }
            }

            return null;
        }

        public override string Name
        {
            get { return "2Day Plus"; }
        }
    }
}
