using System;
using FinanceAnalyzer.DB;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// 保存一只股票的每天的信息
    /// </summary>
    public interface IStockHistory
    {
        DateTime MaxDate
        {
            get;
        }
        DateTime MinDate
        {
            get;
        }

        int MinDateId
        {
            get;
        }

        int MaxDateId
        {
            get;
        }

        DateTime FindDateTime(int dateId);

        /// <summary>
        /// 股票代码
        /// </summary>
        int StockId
        {
            get;
        }
        
        // 得到某一天的股票属性
        IStockData GetStock(DateTime dt);

        // 得到某一天的股票属性
        IStockData GetStock(int id);

        bool IsOperSuccess(int dateid, StockOper oper);

        void AddStock(DateTime dt, IStockData stock);

        IStockHistory GetPartStockHistory(DateTime startDate, DateTime endDate);
    }
}
