using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;

        public Log4NetLogger(string CategoryName, XmlElement Configuration)
        {
            var logger_repository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _log = LogManager.GetLogger(logger_repository.Name, CategoryName);

            log4net.Config.XmlConfigurator.Configure(logger_repository, Configuration);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel Level)
        {
            switch (Level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);
                case LogLevel.None: return false;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    return _log.IsDebugEnabled;

                case LogLevel.Information:
                    return _log.IsInfoEnabled;

                case LogLevel.Warning:
                    return _log.IsWarnEnabled;

                case LogLevel.Error:
                    return _log.IsErrorEnabled;

                case LogLevel.Critical:
                    return _log.IsFatalEnabled;
            }
        }


        public void Log<TState>(
            LogLevel Level,
            EventId Id,
            TState State,
            Exception Error,
            Func<TState, Exception, string> Formatter)
        {
            if (Formatter is null)
                throw new ArgumentNullException(nameof(Formatter));

            if (!IsEnabled(Level)) return;

            var log_message = Formatter(State, Error);

            if (string.IsNullOrEmpty(log_message) && Error is null) return;

            switch (Level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);
                case LogLevel.None: break;

                case LogLevel.Trace:
                case LogLevel.Debug:
                    _log.Debug(log_message);
                    break;

                case LogLevel.Information:
                    _log.Info(log_message);
                    break;

                case LogLevel.Warning:
                    _log.Warn(log_message);
                    break;

                case LogLevel.Error:
                    _log.Error(log_message, Error);
                    break;

                case LogLevel.Critical:
                    _log.Fatal(log_message, Error);
                    break;
            }
        }
    }
}
