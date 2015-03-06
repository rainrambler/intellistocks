using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Utility.Stock;

namespace FinanceAnalyzer.Strategy.Indicator.Signal
{
    /// <summary>
    /// See: http://en.wikipedia.org/wiki/Accumulation/distribution_index
    /// and: http://www.investopedia.com/terms/c/chaikinoscillator.asp
    /// A Chaikin oscillator is formed by subtracting a 10-day exponential moving average 
    /// from a 3-day exponential moving average of the accumulation/distribution index. 
    /// Being an indicator of an indicator, it can give various sell or buy signals, 
    /// depending on the context and other indicators.
    /// </summary>
    class ChaikinOscillator
    {
        public bool AddStock(IStockData sd)
        {
            if (sd == null)
            {
                return false;
            }

            accdist_ = accdist_ + sd.VolumeHand * CalcCLV(sd);
            Prediction_.AddPrice(accdist_);

            if (!Prediction_.IsCountEnough())
            {
                return false;
            }
            
            return true;
        }

        public static double CalcCLV(IStockData sd)
        {
            return ((sd.EndPrice - sd.MinPrice) - (sd.MaxPrice - sd.EndPrice))
                / (sd.MaxPrice - sd.MinPrice);
        }
        
        MovingAveragePrediction Prediction_ = new MovingAveragePrediction(10, 3);
        double accdist_ = 0;
    }
}
