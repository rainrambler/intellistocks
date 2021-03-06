// <copyright file="DateFuncTest.cs" company="huawei">版权所有 (C) huawei 2008</copyright>
using System;
using FinanceAnalyzer;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;

namespace FinanceAnalyzer
{
    /// <summary>This class contains parameterized unit tests for DateFunc</summary>
    [PexClass(typeof(DateFunc))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestFixture]
    public partial class DateFuncTest
    {
        /// <summary>Test stub for IsHoliday(DateTime)</summary>
        [PexMethod]
        public bool IsHoliday(DateTime dt)
        {
            // TODO: add assertions to method DateFuncTest.IsHoliday(DateTime)
            bool result = DateFunc.IsHoliday(dt);
            return result;
        }
        [PexMethod]
        public DateTime ParseString01(object str)
        {
            // TODO: add assertions to method DateFuncTest.ParseString01(Object)
            DateTime result = DateFunc.ParseString(str);
            return result;
        }
        [PexMethod]
        public DateTime ParseString(string str)
        {
            // TODO: add assertions to method DateFuncTest.ParseString(String)
            DateTime result = DateFunc.ParseString(str);
            return result;
        }
        [PexMethod]
        public DateTime GetPrevWorkDay(DateTime day)
        {
            // TODO: add assertions to method DateFuncTest.GetPrevWorkDay(DateTime)
            DateTime result = DateFunc.GetPrevWorkDay(day);
            return result;
        }
    }
}
