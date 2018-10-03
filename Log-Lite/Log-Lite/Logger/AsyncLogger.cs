﻿using Log_Lite.Enum;
using Log_Lite.LogCreator;
using Log_Lite.LogWriter;
using Log_Lite.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Log_Lite.Logger
{
    public class AsyncLogger : Logger
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);


        #region ctors
        public AsyncLogger() : base()
        { }

        public AsyncLogger(params ILogWriter[] logWriters) :
            base(logWriters)
        { }

        public AsyncLogger(ILogCreator logCreator) :
            base(logCreator)
        { }

        public AsyncLogger(ILogCreator logCreator, params ILogWriter[] logWriters) :
            base(logCreator, logWriters)
        { }
        #endregion


        protected override async void Log(object message, LogType type)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var invokerInfo = GetInvokerInfo();
                var logInfo = new LogInfo(type, invokerInfo, message);
                var loggingTask = new Task(() => HandleLogging(logInfo));
                loggingTask.Start();
                await loggingTask;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}