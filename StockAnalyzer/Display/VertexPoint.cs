using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinanceAnalyzer.Statistics.Vertex;

namespace FinanceAnalyzer.Display
{
    public class VertexPoint
    {
        public VertexPoint()
        {
        }

        public VertexPoint(VertexType vt, VertexFindType vft, DateTime dt)
        {
            VertType = vt;
            FindType = vft;
            VertexDate = dt;
        }

        public VertexType VertType
        {
            get;
            set;
        }

        public VertexFindType FindType
        {
            get;
            set;
        }

        public DateTime VertexDate
        {
            get;
            set;
        }
    }
}
