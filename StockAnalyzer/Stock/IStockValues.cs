using System;
using System.Collections.Generic;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// 保存所有交易日每一天的市值（股票资产加现金） 
    /// </summary>
    public interface IStockValues
    {
        // 得到某天的市值
        double GetTotalValue(DateTime dt);

        // 设置某天的市值
        void SetTotalValue(int dateIndex, DateTime dt, double val);

        // 得到某天的市值
        double GetTotalValue(int dt);

        // 得到所有的日期
        ICollection<DateTime> GetAllDate();

        // 得到所有的日期
        ICollection<int> GetAllDateIndex();
        
        // 设置当日操作类型
        void SetOperationSignal(int dateIndex, OperType val);

        // 获取当日操作类型
        OperType GetOperationSignal(int dateIndex);

        // 获取当日操作类型
        OperType GetOperationSignal(DateTime currentDate);

        // Get the total count of specified opertype 
        int GetOperCount(OperType oper);
    }
}
