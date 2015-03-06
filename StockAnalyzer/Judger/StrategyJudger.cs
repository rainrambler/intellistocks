using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Judger
{
    /// <summary>
    /// Compare stock total value of each days to the hold strategy, and accumulate it to the total score. 
    /// </summary>
    class StrategyJudger : IStrategyJudger
    {
        public void Judge(IStrategyResults res)
        {
            ICollection<string> allStrategies = res.AllStrategyNames;

            IStockValues holderValues = res.GetResult("Hold");
            List<int> dates = holderValues.GetAllDateIndex().ToList<int>();

            int curDate = dates.Min();
            while (curDate < dates.Max())
            {
                foreach (string name in allStrategies)
                {
                    IStockValues values = res.GetResult(name);

                    if (!IsValidStrategy(name, values))
                    {
                        continue;
                    }

                    double holderTotalValue = holderValues.GetTotalValue(curDate);

                    double strategyTotalValue = values.GetTotalValue(curDate);

                    Scores_.AddScore(name, CalcSimpleScore(holderTotalValue, strategyTotalValue));
                }

                curDate++;
            }
        }

        /// <summary>
        /// Filter all no-trade strategy
        /// </summary>
        /// <param name="strategyName">strategy Name</param>
        /// <param name="sv">Stock performance</param>
        /// <returns>is strategy valid</returns>
        static bool IsValidStrategy(string strategyName, IStockValues sv)
        {
            if (strategyName == "Hold")
            {
                return true;
            }

            if (sv.GetOperCount(OperType.Buy) == 0)
            {
                return false;
            }

            return sv.GetOperCount(OperType.Sell) != 0;
        }

        public ICollection<IStrategyScores> ScoresArr
        {
            get
            {
                ICollection<IStrategyScores> arr = new List<IStrategyScores>();
                arr.Add(Scores_);
                return arr;
            }
        }

        // 按照每天的策略市值与持有市值的相对百分比计算
        private static double CalcSimpleScore(double holderToday, double valToday)
        {
            double delta = (valToday - holderToday) / holderToday;
            return delta;
        }

        IStrategyScores Scores_ = new StrategyScores("Daily Prices Sigma");
    }
}
