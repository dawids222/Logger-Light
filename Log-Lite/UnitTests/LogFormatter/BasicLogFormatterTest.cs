﻿using Log_Lite.Enum;
using Log_Lite.LogFormatter;
using Log_Lite.Model;
using Log_Lite.Model.Invoker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests.LogFormatter
{
    [TestClass]
    public class BasicLogFormatterTest
    {
        private ILogFormatter Formatter { get; set; }

        private LogInfo LogInfoInfo { get; } = new LogInfo(LogType.INFO, new InvokerModel("INFO", "INFO"), "INFO");
        private LogInfo LogInfoWarning { get; } = new LogInfo(LogType.WARNING, new InvokerModel("WARNING", "WARNING"), "WARNING");
        private LogInfo LogInfoError { get; } = new LogInfo(LogType.ERROR, new InvokerModel("ERROR", "ERROR"), "ERROR");
        private LogInfo LogInfoFatal { get; } = new LogInfo(LogType.FATAL, new InvokerModel("FATAL", "FATAL"), "FATAL");

        [TestInitialize]
        public void Before()
        {
            Formatter = new BasicLogFormatter();
        }

        [TestMethod]
        public void ReturnsProperlyFormatedLog()
        {
            var infoLog = Formatter.Format(LogInfoInfo);
            var warningLog = Formatter.Format(LogInfoWarning);
            var errorLog = Formatter.Format(LogInfoError);
            var fatalLog = Formatter.Format(LogInfoFatal);

            Assert.AreEqual($"{DateTime.Now}   {LogInfoInfo.LogType}      {LogInfoInfo.InvokerInfo}  ->  {LogInfoInfo.Message}", infoLog);
            Assert.AreEqual($"{DateTime.Now}   {LogInfoWarning.LogType}   {LogInfoWarning.InvokerInfo}  ->  {LogInfoWarning.Message}", warningLog);
            Assert.AreEqual($"{DateTime.Now}   {LogInfoError.LogType}     {LogInfoError.InvokerInfo}  ->  {LogInfoError.Message}", errorLog);
            Assert.AreEqual($"{DateTime.Now}   {LogInfoFatal.LogType}     {LogInfoFatal.InvokerInfo}  ->  {LogInfoFatal.Message}", fatalLog);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ThrowsWhenInputIsNull()
        {
            Formatter.Format(null);
        }
    }
}