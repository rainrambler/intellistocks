using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Statistics.Vertex
{
    public enum VertexType
    {
        Max,
        Min
    };

    public enum VertexFindType
    {
        Manual,
        Automatic
    };

    /// <summary>
    /// The max or min vertex of the shape
    /// </summary>
    public class StockVertex
    {
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

        public int DateID
        {
            get;
            set;
        }
    }
}
