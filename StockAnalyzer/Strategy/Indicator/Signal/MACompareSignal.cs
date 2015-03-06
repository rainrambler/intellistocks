using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Utility.Stock;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    class MACompareSignal : ISignalCalculator
    {
        #region ISignalCalculator Members

        public bool AddStock(IStockData sd)
        {
            prediction_.AddPrice(sd.EndPrice);

            if (!prediction_.IsCountEnough())
            {
                return false;
            }

            // 短周期日线
            double shortEMA = prediction_.GetShortAverage();

            if ((sd.StartPrice < shortEMA) && (sd.EndPrice < shortEMA))
            {
                SigmaUpper_ = 0;
                SigmaLower_++;
            }
            else if ((sd.StartPrice > shortEMA) && (sd.EndPrice > shortEMA))
            {
                SigmaUpper_++;
                SigmaLower_ = 0;
            }
            else
            {
                SigmaLower_ = 0;
                SigmaUpper_ = 0;
            }

            if (SigmaUpper_ >= 3)
            {
                TodayOper_ = OperType.Sell;
            }
            else if (SigmaLower_ >= 3)
            {
                TodayOper_ = OperType.Buy;
            }
            else
            {
                TodayOper_ = OperType.NoOper;
            }

            return true;
        }

        public OperType GetSignal()
        {
            return TodayOper_;
        }

        public string GetName()
        {
            return "MASigma";
        }

        #endregion

        public MACompareSignal()
        {
            SigmaLower_ = 0;
            SigmaUpper_ = 0;
        }

        MovingAveragePrediction prediction_ = new MovingAveragePrediction(10, 5);

        double previousMA_ = double.NaN;

        OperType TodayOper_;

        int SigmaUpper_;
        int SigmaLower_;
    }
}
