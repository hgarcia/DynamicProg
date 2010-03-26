using System;
using System.Web;

namespace DynamicProg.Logging
{
    ///<summary>
    /// Defines an ILogger object
    ///</summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs an exception into the database
        /// </summary>
        /// <param name="e">The exception to log</param>
        void LogException(Exception e);

        /// <summary>
        /// Logs an exception into the database
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="e">The exception to log</param>
        void LogException(string message, Exception e);

        /// <summary>
        /// Logs an exception from a web site
        /// </summary>
        /// <param name="message">The message to identify the error</param>
        /// <param name="e">The Exception to log</param>
        /// <param name="context">The <c>HttpContext</c> of the application</param>
        void LogException(string message, Exception e, HttpContext context);

        ///<summary>
        /// Logs only when debugging if not this is completely ignored
        /// Used to log information while debugging an application.
        ///</summary>
        ///<param name="message">The message to log</param>
        void LogDebug(string message);

        /// <summary>
        /// Logs only when debugging if not this is completely ignored
        /// Used to log information while debugging an application.
        /// </summary>
        /// <param name="message">The message to identify the error</param>
        /// <param name="e">The Exception to log</param>
        void LogDebug(string message, Exception e);

        ///<summary>
        /// Logs only when debugging level is enabled, if not this is completely ignored
        ///</summary>
        ///<param name="message">The message to log </param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        void LogDebug(string message, HttpContext context);

        /// <summary>
        /// Logs information about what the system is doing.
        /// Ex: During a long running process we may want to log when the process starts
        /// When every step is executed and once the process ends.
        /// </summary>
        /// <param name="message">The message to log</param>
        void LogInfo(string message);

        ///<summary>
        /// Logs a warning.
        /// A warning is when there is something wrong happening but we can recuperate from it.
        /// This is an "expected Exception".
        ///</summary>
        ///<param name="message">The message to log</param>
        void LogWarning(string message);

        ///<summary>
        /// Logs a warning from a web app
        ///</summary>
        ///<param name="message">The message to log</param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        void LogWarming(string message, HttpContext context);

        ///<summary>
        /// Logs a warning.
        /// A warning is when there is something wrong happening but we can recuperate from.
        /// This is an "expected Exception".
        ///</summary>
        ///<param name="message">The message to log</param>
        /// <param name="e">The exception to log</param>
        void LogWarning(string message, Exception e);

        ///<summary>
        /// Logs a warning from a web app
        ///</summary>
        ///<param name="message">The message to log</param>
        /// <param name="e">The exception to log</param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        void LogWarming(string message, Exception e, HttpContext context);

        /// <summary>
        /// Logs a fatal error, ex an error on the UI
        /// </summary>
        /// <param name="message">A message to identify the error</param>
        /// <param name="e">The exception</param>
        void LogFatal(string message, Exception e);

        /// <summary>
        /// Logs a fatal error, ex an error on the UI
        /// </summary>
        /// <param name="message">A message to identify the error</param>
        /// <param name="e">The exception</param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        void LogFatal(string message, Exception e, HttpContext context);
    }
}