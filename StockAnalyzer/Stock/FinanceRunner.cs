using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using FinanceAnalyzer.Business;
using FinanceAnalyzer.Log;
using FinanceAnalyzer.Strategy;
using FinanceAnalyzer.Strategy.Result;
using FinanceAnalyzer.Utility;
using FinanceAnalyzer.Strategy.Factory;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// Calculate results using specified stock and strategies
    /// </summary>
    public class FinanceRunner
    {
        private const double INITCASH = 100000; // ��ʼ�����˻���� 

        public void Go(IStockHistory history, IStrategyFactory strategies)
        {
            History_ = history;

            foreach (IFinanceStrategy strategy in strategies.AllStrategies)
            {
                ExecTask(strategy);
            }
        }

        // ����ѡ���Ĳ��Կ�ʼ����
        private void ExecTask(IFinanceStrategy strategy)
        {
            IStockValues values = new StockValues();

            Start(values, strategy);

            _results.AddResult(strategy.Name, values);
        }
        
        private void Start(IStockValues values, IFinanceStrategy strategy)
        {
            LogMgr.Logger.LogInfo("==>Start With Strategy: " + strategy.Name + "...");
            Account acc = new Account();
            acc.BankRoll = INITCASH;

            IStockHolder holder = new StockHolder();            
            holder.History = History_;
            acc.Holder = holder;
            strategy.Holder = holder;
            HolderManager.Instance().Holder = holder;
            
            acc.Processor = CurrentBonusProcessor;

            int currentDate = History_.MinDateId;            

            LogMgr.Logger.LogInfo("Min Date = {0}, Max Date = {1}",
                History_.FindDateTime(currentDate), History_.FindDateTime(History_.MaxDateId));

            while (currentDate < History_.MaxDateId)
            {
                DateTime curDateTime = History_.FindDateTime(currentDate);
                double totalvalue = acc.TotalValue(currentDate);
                values.SetTotalValue(currentDate, curDateTime, totalvalue);
                
                //acc.ProcessBonus(startDate);

                ICollection<StockOper> opers = strategy.GetOper(currentDate, acc);

                if (opers != null)
                {
                    foreach (StockOper oper in opers)
                    {
                        if (History_.IsOperSuccess(currentDate, oper))
                        {
                            acc.DoBusiness(oper);
                            values.SetOperationSignal(currentDate, oper.Type);
                        }                        
                    }
                }

                //startDate = DateFunc.GetNextWorkday(startDate);
                currentDate++;
            }

            LogMgr.Logger.LogInfo("Strategy " + strategy.Name + " End Value: "
                + acc.TotalValue(currentDate).ToString(CultureInfo.CurrentCulture));
            LogMgr.Logger.LogInfo("Strategy " + strategy.Name 
                + ": Buys: " + acc.BuyTransactionCount
                + ", Sells: " + acc.SellTransactionCount);
            LogMgr.Logger.LogInfo("<== End With Strategy: " + strategy.Name);
        }

        public StrategyResults Results
        {
            get { return _results; }
        }

        StrategyResults _results = new StrategyResults();
        IStockHistory History_;

        public IBonusProcessor CurrentBonusProcessor
        {
            get;
            set;
        }
    }
}