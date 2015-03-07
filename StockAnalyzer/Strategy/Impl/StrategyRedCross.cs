using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Business.Shape;

namespace FinanceAnalyzer.Strategy.Impl
{
    class StrategyRedCross : IFinanceStrategy
    {
        public override ICollection<StockOper> GetOper(int day, IAccount account)
        {
            IStockData curProp = stockHistory.GetStock(day);
            IStockData stockPrev1Prop = stockHistory.GetStock(day - 1);
            IStockData stockPrev2Prop = stockHistory.GetStock(day - 2);
            IStockData stockPrev3Prop = stockHistory.GetStock(day - 3);

            //DateTime prevDate = stockHistory.GetPreviousDay(day);
            IStockData stockprevProp = stockHistory.GetStock(day - 2);
            //DateTime prevNextDate = stockHistory.GetPreviousDay(prevDate);

            if (!CheckStock(curProp, day) 
                || !CheckStock(stockPrev1Prop, day - 1)
                || !CheckStock(stockPrev2Prop, day - 2)
                || !CheckStock(stockPrev3Prop, day - 3))
            {
                return null;
            }
            
            ICollection<StockOper> opers = new List<StockOper>();

            if ((curProp.AvgPrice > stockPrev1Prop.AvgPrice)
                && (stockPrev1Prop.AvgPrice > stockPrev2Prop.AvgPrice)
                && (stockPrev2Prop.AvgPrice > stockPrev3Prop.AvgPrice)
                && ShapeJudger.IsGreen(curProp))
            {
                StockOper oper = new StockOper(curProp.EndPrice, stockHolder.StockCount(), OperType.Sell);
                opers.Add(oper);
                return opers;
            }

            if ((curProp.AvgPrice < stockPrev1Prop.AvgPrice)
                && (stockPrev1Prop.AvgPrice < stockPrev2Prop.AvgPrice)
                && (stockPrev2Prop.AvgPrice < stockPrev3Prop.AvgPrice)
                && ShapeJudger.IsRed(curProp))
            {
                int stockCount = Transaction.GetCanBuyStockCount(account.BankRoll,
                        curProp.EndPrice);
                StockOper oper = new StockOper(curProp.EndPrice, stockCount, OperType.Buy);
                opers.Add(oper);
                return opers;
            }

            return null;
        }

        public override string Name
        {
            get { return "RedCross"; }
        }
    }
}
