using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web.Configuration;
using DynamicProg.Extensions;
using LaTrompa.Extensions;

namespace DynamicProg
{
    public class Utils
    {
        public static string CleanCreditCardNumber(string creditCard)
        {
            if (String.IsNullOrEmpty(creditCard))
            {
                return string.Empty;
            }
            var regex = new Regex(RegexPattern.CREDIT_CARD_STRIP_NON_NUMERIC,
                                  RegexOptions.IgnoreCase | RegexOptions.Singleline);
            return regex.Replace(creditCard, String.Empty);
        }

        public static string GetAppSetting(string key)
        {
            if (WebConfigurationManager.AppSettings[key] != null)
            {
                return WebConfigurationManager.AppSettings[key];
            }
            if (ConfigurationManager.AppSettings[key] != null)
            {
                return ConfigurationManager.AppSettings[key];
            }
            return string.Empty;
        }

        public static String GetConnectionString(string connectionStringKey)
        {
            if (ConfigurationManager.ConnectionStrings[connectionStringKey] != null)
            {
                return ConfigurationManager.ConnectionStrings[connectionStringKey].ToString();
            }
            if (WebConfigurationManager.ConnectionStrings[connectionStringKey] != null)
            {
                return WebConfigurationManager.ConnectionStrings[connectionStringKey].ToString();
            }
            return GetAppSetting(connectionStringKey);
        }

        public static bool ToBool(object req)
        {
            return ToBool(req, false);
        }

        public static bool ToBool(object req, bool defValue)
        {
            if (req == null)
            {
                return defValue;
            }
            if (req.ToString().Trim().ToLower() == "false" || req.ToString().Trim() == string.Empty ||
                req.ToString().Trim() == "0")
            {
                return false;
            }
            return true;
        }

        public static decimal ToDecimal(object req)
        {
            return ToDecimal(req, 0);
        }

        public static decimal ToDecimal(object req, decimal defValue)
        {
            if (req == null)
            {
                return defValue;
            }
            if (req.ToString().IsNumber())
            {
                return Convert.ToDecimal(req.ToString());
            }
            return defValue;
        }

        public static double ToDouble(object req)
        {
            return ToDouble(req, 0);
        }

        public static double ToDouble(object req, int defValue)
        {
            if (req == null)
            {
                return defValue;
            }
            if (req.ToString().IsNumber())
            {
                return Convert.ToDouble(req.ToString());
            }
            return defValue;
        }

        public static int ToInt(object req)
        {
            return ToInt(req, 0);
        }

        public static int ToInt(object req, int defValue)
        {
            if (req == null)
            {
                return defValue;
            }
            if (req.ToString().IsInteger())
            {
                return Convert.ToInt32(req.ToString());
            }
            return defValue;
        }

        public static short ToShort(object req)
        {
            return ToShort(req, 0);
        }

        public static short ToShort(object req, short defValue)
        {
            if (req == null)
            {
                return defValue;
            }
            if (req.ToString().IsInteger())
            {
                return Convert.ToInt16(req.ToString());
            }
            return defValue;
        }

        public static string ToString(object req)
        {
            return ToString(req, string.Empty);
        }

        public static string ToString(object req, string defValue)
        {
            if (req == null)
            {
                return defValue;
            }
            return req.ToString();
        }

    }
}
