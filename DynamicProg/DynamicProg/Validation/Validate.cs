#region Header

/*
 * Part of the code have been taken from SubSonic - http://subsonicproject.com
 * 
 * The contents of this file are subject to the Mozilla Public
 * License Version 1.1 (the "License"); you may not use this file
 * except in compliance with the License. You may obtain a copy of
 * the License at http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an 
 * "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
 * implied. See the License for the specific language governing
 * rights and limitations under the License.
*/

#endregion Header

using System;
using System.Text.RegularExpressions;
using DynamicProg.Extensions;
using LaTrompa.Extensions;

namespace LaTrompa.Validation
{

    public static class Validate
    {

        #region Public Methods

        public static bool IsAlpha(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            return !Regex.IsMatch(value, RegexPattern.ALPHA);
        }

        public static bool IsAlphaNumeric(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            return !Regex.IsMatch(value, RegexPattern.ALPHA_NUMERIC);
        }

        public static bool IsEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }
            return Regex.IsMatch(email, RegexPattern.EMAIL);
        }

        public static bool IsGuid(string guid)
        {
            if (String.IsNullOrEmpty(guid))
            {
                return false;
            }
            return Regex.IsMatch(guid, RegexPattern.GUID);
        }

        public static bool IsIPAddress(string ipAddress)
        {
            if (String.IsNullOrEmpty(ipAddress))
            {
                return false;
            }
            return Regex.IsMatch(ipAddress, RegexPattern.IP_ADDRESS);
        }

        public static bool IsLowerCase(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            return Regex.IsMatch(value.StripWhitespace(), RegexPattern.LOWER_CASE);
        }

        public static bool IsStrongPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return false;
            }
            return Regex.IsMatch(password, RegexPattern.STRONG_PASSWORD);
        }

        public static bool IsURL(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return false;
            }
            return Regex.IsMatch(url, RegexPattern.URL);
        }

        public static bool IsUSSocialSecurityNumber(string socialSecurityNumber)
        {
            if (String.IsNullOrEmpty(socialSecurityNumber))
            {
                return false;
            }
            return Regex.IsMatch(socialSecurityNumber, RegexPattern.SOCIAL_SECURITY);
        }

        public static bool IsUsZipCodeAny(string zipCode)
        {
            if (String.IsNullOrEmpty(zipCode))
            {
                return false;
            }
            return Regex.IsMatch(zipCode, RegexPattern.US_ZIPCODE_PLUS_FOUR_OPTIONAL);
        }

        public static bool IsUsZipCodeFive(string zipCode)
        {
            if (String.IsNullOrEmpty(zipCode))
            {
                return false;
            }
            return Regex.IsMatch(zipCode, RegexPattern.US_ZIPCODE);
        }

        public static bool IsUsZipCodeFivePlusFour(string zipCode)
        {
            if (String.IsNullOrEmpty(zipCode))
            {
                return false;
            }
            return Regex.IsMatch(zipCode, RegexPattern.US_ZIPCODE_PLUS_FOUR);
        }

        public static bool IsUpperCase(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }
            return Regex.IsMatch(value.StripWhitespace(), RegexPattern.UPPER_CASE);
        }

        #endregion Public Methods
    }
}