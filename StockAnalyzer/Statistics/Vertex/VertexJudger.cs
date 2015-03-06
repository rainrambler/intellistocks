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
    /// <summary>
    /// Find Vertex by min and max value
    /// </summary>
    class VertexJudger
    {
        /// <summary>
        /// Find vertex points of stock curve
        /// </summary>
        /// <param name="hist">Stock history</param>
        /// <returns>All max and min vertex</returns>
        public ICollection<StockVertex> FindVertex(IStockHistory hist)
        {
            Vertexes vertexes = new Vertexes();

            FixedSizeLinkedList<SortStock> fixedvertexes = new FixedSizeLinkedList<SortStock>(TIME_WINDOW_MARGIN);

            int currentDate = hist.MinDateId;

            while (currentDate < hist.MaxDateId)
            {
                IStockData stock = hist.GetStock(currentDate);

                if (stock != null)
                {
                    double avgPrice = StockDataCalc.GetAveragePrice(stock);
                    fixedvertexes.AddLast(new SortStock(currentDate, avgPrice));
                }

                if (fixedvertexes.IsEnough())
                {
                    SortStock stockMax = fixedvertexes.FindMax();
                    SortStock stockMin = fixedvertexes.FindMin();

                    if (IsValidVertexPosition(stockMax, currentDate))
                    {
                        vertexes.Add(CreateVertex(stockMax, VertexType.Max));
                    }

                    if (IsValidVertexPosition(stockMin, currentDate))
                    {
                        vertexes.Add(CreateVertex(stockMin, VertexType.Min));
                    }
                }

                currentDate++;
            }

            return vertexes.GetAll();
        }

        static bool IsValidVertexPosition(SortStock ss, int currentDateIndex)
        {
            int middleDatePos = currentDateIndex - (TIME_WINDOW_MARGIN / 2);
            return (ss.DateIndex == middleDatePos) || (ss.DateIndex == middleDatePos + 1);
        }
        
        static StockVertex CreateVertex(SortStock sd, VertexType vtp)
        {
            if (sd == null)
            {
                return null;
            }

            StockVertex sv = new StockVertex();
            sv.FindType = VertexFindType.Automatic;
            sv.VertType = vtp;
            sv.DateID = sd.DateIndex;

            return sv;
        }

        const int TIME_WINDOW_MARGIN = 20; // Days
    }
}
