using System;
using System.Collections.Generic;
using System.Text;
using FinanceAnalyzer.Log;
using NUnit.Framework;
using FinanceAnalyzer.Stock;

namespace FinanceAnalyzer
{
    [TestFixture]
    public class StockValuesTest
    {
        [SetUp]
        public void Init()
        {
            LogMgr.Logger = new DummyLog();
        }

        [Test]
        public void SetTotalValue()
        {
            IStockValues values = new StockValues();

            values.SetTotalValue(0, new DateTime(2012,10,1), 10.0);
            Assert.AreEqual(values.GetAllDate().Count, 1);

            values.SetTotalValue(1, new DateTime(2012,10,2), 12.0);
            Assert.AreEqual(values.GetAllDate().Count, 2);

            values.SetTotalValue(2, new DateTime(2012, 10, 3) , -1); // invalid
            Assert.AreEqual(values.GetAllDate().Count, 2);
        }

        [Test]
        public void GetTotalValue()
        {
            IStockValues values = new StockValues();

            values.SetTotalValue(0, new DateTime(2012,10,1), 10.0);

            double val = values.GetTotalValue(0);
            Assert.AreEqual(val, 10.0);

            val = values.GetTotalValue(-1);
            Assert.IsTrue(val < 0);
        }
    }
}
