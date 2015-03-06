using System;
using System.Collections.Generic;
using System.Text;
using Stock.Common.Data;

namespace FinanceAnalyzer.Stock
{
    /// <summary>
    /// �������н�����ÿһ�����ֵ����Ʊ�ʲ����ֽ� 
    /// </summary>
    public interface IStockValues
    {
        // �õ�ĳ�����ֵ
        double GetTotalValue(DateTime dt);

        // ����ĳ�����ֵ
        void SetTotalValue(int dateIndex, DateTime dt, double val);

        // �õ�ĳ�����ֵ
        double GetTotalValue(int dt);

        // �õ����е�����
        ICollection<DateTime> GetAllDate();

        // �õ����е�����
        ICollection<int> GetAllDateIndex();
        
        // ���õ��ղ�������
        void SetOperationSignal(int dateIndex, OperType val);

        // ��ȡ���ղ�������
        OperType GetOperationSignal(int dateIndex);

        // ��ȡ���ղ�������
        OperType GetOperationSignal(DateTime currentDate);

        // Get the total count of specified opertype 
        int GetOperCount(OperType oper);
    }
}
