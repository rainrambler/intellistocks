using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Stock;
using Stock.Common.Data;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Utility.Stock;

namespace FinanceAnalyzer.Statistics.Vertex
{
    class VertexFinder
    {
        public ICollection<StockVertex> FindVertex(IStockHistory hist)
        {
            Vertexes vertexes = new Vertexes();

            int currentDate = hist.MinDateId;

            while (currentDate < hist.MaxDateId)
            {
                IStockData stock = hist.GetStock(currentDate);

                if (stock != null)
                {
                    double avgPrice = StockDataCalc.GetAveragePrice(stock);

                    if (avgPrice > AveragePriceMax_)
                    {

                    }
                }

                currentDate++;
            }

            return null;
        }

        int currentMinDate_;
        int currentMaxDate_;
        double AveragePriceMin_ = double.MaxValue;
        double AveragePriceMax_ = double.MinValue;

        bool IsPrevPriceUp_;
    }
}
