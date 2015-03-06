using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using FinanceAnalyzer.Log;
using System.Linq;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    // 保存所有交易日每一天的市值（股票资产加现金） 
    public class StockValues : IStockValues
    {
        public double GetTotalValue(DateTime dt)
        {
            int dateIdx;
            if (dateToIndex_.TryGetValue(dt, out dateIdx))
            {
                return GetTotalValue(dateIdx);
            }
            else
            {
                //LogMgr.Logger.LogInfo("StockValues: GetTotalValue error at: " + dt.ToLongDateString());
                return -1;
            }
        }

        public double GetTotalValue(int dt)
        {
            double val;
            if (_DateValues.TryGetValue(dt, out val))
            {
                return val;
            }
            else
            {
                //LogMgr.Logger.LogInfo("StockValues: GetTotalValue error at: " + dt.ToLongDateString());
                return -1;
            }
        }

        public void SetTotalValue(int dateIndex, DateTime dt, double val)
        {
            if (val <= 0)
            {
                //LogMgr.Logger.LogInfo("StockValues: SetTotalValue error at: " + dt.ToLongDateString());
            }
            else
            {
                _DateValues.Add(dateIndex, val);
                dateToIndex_.Add(dt, dateIndex);
            }
        }

        public ICollection<DateTime> GetAllDate()
        {
            return dateToIndex_.Keys;
        }

        public ICollection<int> GetAllDateIndex()
        {
            return _DateValues.Keys;
        }

        // 设置当日操作类型
        // 当前不支持同一天买入和卖出
        public void SetOperationSignal(int dt, OperType val)
        {
            if (_Signals.ContainsKey(dt))
            {
                return;
            }
            _Signals.Add(dt, val);
        }

        // 获取当日操作类型
        public OperType GetOperationSignal(DateTime dt)
        {
            OperType val;

            if (!dateToIndex_.ContainsKey(dt))
            {
                return OperType.NoOper;
            }

            if (_Signals.TryGetValue(dateToIndex_[dt], out val))
            {
                return val;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        // 获取当日操作类型
        public OperType GetOperationSignal(int dt)
        {
            OperType val;
            if (_Signals.TryGetValue(dt, out val))
            {
                return val;
            }
            else
            {
                return OperType.NoOper;
            }
        }

        public int GetOperCount(OperType oper)
        {
            var result = from item in _Signals.Values where item == oper select item;
            return result.Count();
        }

        private SortedDictionary<int, double> _DateValues = new SortedDictionary<int, double>();
        private Dictionary<DateTime, int> dateToIndex_ = new Dictionary<DateTime, int>();
        private Dictionary<int, OperType> _Signals = new Dictionary<int, OperType>();
    }
}
