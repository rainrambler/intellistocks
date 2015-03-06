using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Utility.Stock
{
    /// <summary>
    /// Simulate a slope on a stock chart
    /// </summary>
    class TwoDaySlope
    {
        public static double CalcSlope(double priceOld, double priceNew)
        {
            double percent = (priceNew - priceOld) / priceOld;

            if ((percent > 0.1) || (percent < -0.1))
            {
                throw new ArgumentOutOfRangeException();
            }

            return (percent * 100 * 80); // degree, 10% => 80 degree
        }
    }
}
