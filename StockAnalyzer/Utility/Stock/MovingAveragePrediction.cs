using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FinanceAnalyzer.Utility.Stock
{
    class MovingAveragePrediction
    {
        public MovingAveragePrediction(int longDays, int shortDays)
        {
            LongDays = longDays;
            ShortDays = shortDays;
        }

        public int LongDays
        {
            get;
            set;
        }

        public int ShortDays
        {
            get;
            set;
        }

        public double CalcNextPredictionValue()
        {
            if (!IsCountEnough())
            {
                return double.NaN;
            }

            double curLongAvg = LongPrices_.Average();
            double curShortAvg = ShortPrices_.Average();

            double val1 = (curLongAvg - LongPrices_.First()) * ShortDays;
            double val2 = (curShortAvg - ShortPrices_.First()) * LongDays;

            double val = (val1 - val2) / (LongDays - ShortDays);
            return val;
        }

        public void AddPrice(double price)
        {
            if (price < 0.01)
            {
                throw new ArgumentOutOfRangeException("MovingAveragePrediction.AddPrice: " 
                    + price.ToString(CultureInfo.CurrentCulture));
            }

            LongPrices_.AddLast(price);
            if (LongPrices_.Count > LongDays)
            {
                LongPrices_.RemoveFirst();
            }

            ShortPrices_.AddLast(price);
            if (ShortPrices_.Count > ShortDays)
            {
                ShortPrices_.RemoveFirst();
            }
        }

        public bool IsCountEnough()
        {
            return (LongPrices_.Count >= LongDays) && (ShortPrices_.Count >= ShortDays);
        }

        public double GetLongAverage()
        {
            return LongPrices_.Average();
        }

        public double GetShortAverage()
        {
            return ShortPrices_.Average();
        }

        LinkedList<double> LongPrices_ = new LinkedList<double>();
        LinkedList<double> ShortPrices_ = new LinkedList<double>();
    }
}
