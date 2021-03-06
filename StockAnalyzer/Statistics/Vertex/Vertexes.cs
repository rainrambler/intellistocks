﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinanceAnalyzer.Statistics.Vertex
{
    /// <summary> 
    /// All vertexes
    /// </summary>
    class Vertexes
    {
        public void Add(StockVertex sv)
        {
            if (sv == null)
            {
                return;
            }

            if (vertexes_.ContainsKey(sv.DateID))
            {
                return;
            }

            vertexes_.Add(sv.DateID, sv);
        }

        public ICollection<StockVertex> GetAll()
        {
            return vertexes_.Values;
        }

        Dictionary<int, StockVertex> vertexes_ = new Dictionary<int, StockVertex>(); // Key is DateID
    }
}
