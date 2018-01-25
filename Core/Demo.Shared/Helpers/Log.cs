using NLog;
using System;

namespace Demo.Shared.Helpers
{
    /// <summary>
    /// The log.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The log exception.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public static void LogException(Exception exception)
        {
            var logMessage = string.Format(exception.StackTrace);
            Logger.Error(logMessage);
        }

        /// <summary>
        /// The log warning.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public static void LogWarning(string message)
        {
            Logger.Warn(message);
        }
    }
}