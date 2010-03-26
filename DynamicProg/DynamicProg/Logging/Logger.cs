using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Web;
using log4net;
using log4net.Config;

namespace DynamicProg.Logging
{
    ///<summary>
    /// An implementation of <c>ILogger</c>
    ///</summary>
    public class Logger : ILogger
    {
        private readonly ILog _logger;

        ///<summary>
        /// Constructor
        ///</summary>
        public Logger()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(
                                                               AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                               "log4net.config")));
            MethodBase m = new StackTrace().GetFrame(1).GetMethod();
            _logger = LogManager.GetLogger(string.Format("{0}.{1}", m.DeclaringType, m.Name));
        }

        #region ILogger Members

        /// <summary>
        /// Logs an exception into the database
        /// </summary>
        /// <param name="e">The exception to log into the database</param>
        public void LogException(Exception e)
        {
            _logger.Error(e.Message, e);
        }

        /// <summary>
        /// Logs an exception into the database
        /// </summary>
        /// <param name="message">A message to add to this log</param>
        /// <param name="e">The exception to log into the database</param>
        public void LogException(string message, Exception e)
        {
            _logger.Error(message, e);
        }

        /// <summary>
        /// Logs an exception from a web site
        /// </summary>
        /// <param name="message">The message to identify the error</param>
        /// <param name="e">The Exception to log</param>
        /// <param name="context">The <c>HttpContext</c> of the application</param>
        public void LogException(string message, Exception e, HttpContext context)
        {
            getServerInfo(context);
            _logger.Error(message, e);
        }

        ///<summary>
        /// Logs only when debugging if not this is completely ignored
        /// Used to log information while debugging an application.
        ///</summary>
        ///<param name="message">The message to log</param>
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// Logs only when debugging if not this is completely ignored
        /// Used to log information while debugging an application.
        /// </summary>
        /// <param name="message">The message to identify the error</param>
        /// <param name="e">The Exception to log</param>
        public void LogDebug(string message, Exception e)
        {
            _logger.Debug(message,e);
        }

        ///<summary>
        /// Logs only when debugging level is enabled, if not this is completely ignored
        ///</summary>
        ///<param name="message">The message to log </param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        public void LogDebug(string message, HttpContext context)
        {
            getServerInfo(context);
            _logger.Debug(message);
        }

        /// <summary>
        /// Logs information about what the system is doing.
        /// Ex: During a long running process we may want to log when the process starts
        /// When every step is executed and once the process ends.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        ///<summary>
        /// Logs a warning.
        /// A warning is when there is something wrong happening but we can recuperate from it.
        /// This is an "expected Exception".
        ///</summary>
        ///<param name="message">The message to log</param>
        public void LogWarning(string message)
        {
            _logger.Warn(message);
        }

        ///<summary>
        /// Logs a warning from a web application
        ///</summary>
        ///<param name="message">The message to log</param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        public void LogWarming(string message, HttpContext context)
        {
            getServerInfo(context);
            _logger.Warn(message);
        }

        ///<summary>
        /// Logs a warning.
        /// A warning is when there is something wrong happening but we can recuperate from.
        /// This is an "expected Exception".
        ///</summary>
        ///<param name="message">The message to log</param>
        /// <param name="e">The exception to log</param>
        public void LogWarning(string message, Exception e)
        {
            _logger.Warn(message, e);
        }

        ///<summary>
        /// Logs a warning from a web application
        ///</summary>
        ///<param name="message">The message to log</param>
        /// <param name="e">The exception to log</param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        public void LogWarming(string message, Exception e, HttpContext context)
        {
            getServerInfo(context);
            _logger.Warn(message, e);
        }

        /// <summary>
        /// Logs a fatal error, ex an error on the UI
        /// </summary>
        /// <param name="message">A message to identify the error</param>
        /// <param name="e">The exception</param>
        public void LogFatal(string message, Exception e)
        {
            _logger.Fatal(message, e);
        }

        /// <summary>
        /// Logs a fatal error, ex an error on the UI
        /// </summary>
        /// <param name="message">A message to identify the error</param>
        /// <param name="e">The exception</param>
        ///<param name="context">The <see cref="HttpContext"/> of the application</param>
        public void LogFatal(string message, Exception e, HttpContext context)
        {
            getServerInfo(context);
            _logger.Fatal(message, e);
        }

        #endregion

        private void getServerInfo(HttpContext currentContext)
        {
            if (currentContext == null) return;

            string domain = string.Empty;
            string url = string.Empty;
            string serverIP = string.Empty;
            string method = string.Empty;
            string requestData = string.Empty;
            string lang = string.Empty;

            try
            {
                domain = Utils.ToString(currentContext.Request.ServerVariables["HTTP_HOST"]);
            }
            catch (Exception e)
            {
                LogDebug("Getting the Domain", e);
            }

            try
            {
                url = Utils.ToString(currentContext.Request.FilePath);
            }
            catch (Exception e)
            {
                LogDebug("Getting the Url", e);
            }

            try
            {
                serverIP = Utils.ToString(currentContext.Request.ServerVariables["LOCAL_ADDR"]);
            }
            catch (Exception e)
            {
                LogDebug("Getting the Server IP", e);
            }
            try
            {
                method = Utils.ToString(currentContext.Request.ServerVariables["REQUEST_METHOD"]);
            }
            catch (Exception e)
            {
                LogDebug("Getting the Method", e);
            }

            if (method.ToUpper() == "POST")
            {
                requestData = Utils.ToString(currentContext.Request.Form);
            }
            else if (method == "GET")
            {
                requestData = Utils.ToString(currentContext.Request.QueryString);
            }

            if (requestData.Length > 500)
            {
                requestData = requestData.Substring(0, 500);
            }

            try
            {
                lang = Utils.ToString(currentContext.Session["Lang"]);
            }
            catch (Exception e)
            {
                LogDebug("Getting the Language", e);
            }
            if (lang.Length > 2)
            {
                lang = lang.Substring(0, 2);
            }

            setGlobalContext(method, requestData, domain, url, serverIP, lang);
        }

        private static void setGlobalContext(string method, string requestData, string domain, string url,
                                             string serverIP, string lang)
        {
            if (!String.IsNullOrEmpty(requestData))
            {
                GlobalContext.Properties["requestData"] = string.Format("METHOD: {0} - {1}", method, requestData);
            }
            GlobalContext.Properties["domain"] = domain;
            GlobalContext.Properties["url"] = url;
            GlobalContext.Properties["serverIP"] = serverIP;
            GlobalContext.Properties["language"] = lang;
        }
    }
}