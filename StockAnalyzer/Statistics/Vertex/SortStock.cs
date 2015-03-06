using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Statistics.Vertex
{
    class SortStock : IComparable 
    {
        public SortStock(int dateIdx, double price)
        {
            DateIndex = dateIdx;
            EndPrice = price;
        }

        public int DateIndex
        {
            get;
            set;
        }
        
        public double EndPrice
        {
            get;
            set;
        }

        public int CompareTo(object obj)
        {
            if (obj is SortStock)
            {
                SortStock s = (SortStock)obj;
                return this.EndPrice.CompareTo(s.EndPrice);
            }
            else
            {
                throw new ArgumentException("Object is not a SortStock");
            }
        }
    }
}
