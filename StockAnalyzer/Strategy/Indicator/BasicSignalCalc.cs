using System;
using System.Collections.Generic;
using FinanceAnalyzer.Stock;
using FinanceAnalyzer.Utility;
using Stock.Common.Data;

namespace FinanceAnalyzer.Strategy.Indicator
{
    /// <summary>
    /// Simple signal calculator
    /// </summary>
    class BasicSignalCalc : IIndicatorCalc
    {
        public BasicSignalCalc(ISignalCalculator signalCalc)
        {
            signalCalc_ = signalCalc;
        }

        public string Name
        {
            get
            {
                return signalCalc_.GetName();
            }
        }

        public void Calc(IStockHistory hist)
        {
            int currentDate = hist.MinDateId;

            while (currentDate < hist.MaxDateId)
            {
                IStockData stock = hist.GetStock(currentDate);
                CalculateSignal(currentDate, stock);

                currentDate++;
            }
        }

        void CalculateSignal(int dt, IStockData stock)
        {
            if (stock == null)
            {
                return;
            }

            if (!signalCalc_.AddStock(stock))
            {
                return;
            }

            OperType ot = signalCalc_.GetSignal();
            if (ot != OperType.NoOper)
            {
                DateToOpers_.Add(dt, ot);
            }
        }

        /// <summary>
        /// Return the buy or sell signal based on the indicator value
        /// </summary>
        /// <param name="dt">Current date</param>
        /// <param name="prev">Previous date</param>
        /// <returns>buy or sell signal</returns>
        public OperType MatchSignal(int dt, int prev)
        {
            if (DateToOpers_.ContainsKey(dt))
            {
                return DateToOpers_[dt];
            }

            return OperType.NoOper;
        }

        private Dictionary<int, OperType> DateToOpers_ = new Dictionary<int, OperType>();

        ISignalCalculator signalCalc_;
    }
}
