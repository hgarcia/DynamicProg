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

namespace DynamicProg.Extensions
{
    /// <summary>
    /// Contains common regular expression used for validation
    /// </summary>
    public class RegexPattern
    {
        #region Constants

        /// <summary>
        /// Regex to find an alpha string
        /// </summary>
        public const string ALPHA = "[^a-zA-Z]";

        /// <summary>
        /// Regex to find an alpha numeric
        /// </summary>
        public const string ALPHA_NUMERIC = "[^a-zA-Z0-9]";

        /// <summary>
        /// Regex to validate the number of an AMEX card
        /// </summary>
        public const string CREDIT_CARD_AMERICAN_EXPRESS = @"^(?:(?:[3][4|7])(?:\d{13}))$";

        /// <summary>
        /// Regex to validate the number of a Carte Blanche
        /// </summary>
        public const string CREDIT_CARD_CARTE_BLANCHE = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";

        /// <summary>
        /// Regex to validate the number of a Diners Club
        /// </summary>
        public const string CREDIT_CARD_DINERS_CLUB = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";

        /// <summary>
        /// Regex to validate the number of a Discover card
        /// </summary>
        public const string CREDIT_CARD_DISCOVER = @"^(?:(?:6011)(?:\d{12}))$";

        /// <summary>
        /// Regex to validate the number of a Card En Route card
        /// </summary>
        public const string CREDIT_CARD_EN_ROUTE = @"^(?:(?:[2](?:014|149))(?:\d{11}))$";

        /// <summary>
        /// Regex to validate the number of a JCB card
        /// </summary>
        public const string CREDIT_CARD_JCB = @"^(?:(?:(?:2131|1800)(?:\d{11}))$|^(?:(?:3)(?:\d{15})))$";

        /// <summary>
        /// Regex to validate the number of a Master Card card
        /// </summary>
        public const string CREDIT_CARD_MASTER_CARD = @"^(?:(?:[5][1-5])(?:\d{14}))$";

        /// <summary>
        /// Regex to strip non numeric chars in a credit card number
        /// </summary>
        public const string CREDIT_CARD_STRIP_NON_NUMERIC = @"(\-|\s|\D)*";

        /// <summary>
        /// Regex to validate the number of a Visa card
        /// </summary>
        public const string CREDIT_CARD_VISA = @"^(?:(?:[4])(?:\d{12}|\d{15}))$";

        /// <summary>
        /// Regex to validate an email address
        /// </summary>
        public const string EMAIL = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";

        /// <summary>
        /// Regex to find a match in an embedded class name
        /// </summary>
        public const string EMBEDDED_CLASS_NAME_MATCH = "(?<=^_).*?(?=_)";

        /// <summary>
        /// Regex to replace the name of an embedded class
        /// </summary>
        public const string EMBEDDED_CLASS_NAME_REPLACE = "^_.*?_";

        /// <summary>
        /// Regex to find a match for underscore in an embedded class name
        /// </summary>
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_MATCH = "(?<=^UNDERSCORE).*?(?=UNDERSCORE)";

        /// <summary>
        /// Regex to replace the underscore in the name of an embedded class
        /// </summary>
        public const string EMBEDDED_CLASS_NAME_UNDERSCORE_REPLACE = "^UNDERSCORE.*?UNDERSCORE";

        /// <summary>
        /// Regex to validate a GUID
        /// </summary>
        public const string GUID = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";

        /// <summary>
        /// Regex to validate an ip address
        /// </summary>
        public const string IP_ADDRESS = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        /// <summary>
        /// Regex to find an integer
        /// </summary>
        public const string ISINT = "^-[0-9]+$|^[0-9]+$";

        /// <summary>
        /// Regex to validate lower case
        /// </summary>
        public const string LOWER_CASE = @"^[a-z]+$";

        /// <summary>
        /// Regex to find a natural number
        /// </summary>
        public const string NATURAL_PATTERN = "0*[1-9][0-9]*";

        /// <summary>
        /// Regex to find a non integer
        /// </summary>
        public const string NOT_INT = "[^0-9-]";

        /// <summary>
        /// Regex to find a non whole number
        /// </summary>
        public const string NOT_WHOLE_NUMBER = "[^0-9]";

        /// <summary>
        /// Regex to find non alphanumeric characters
        /// </summary>
        public const string NON_ALPHA = ".'?\\/><$!@%^*&+,;:\"{}[]|-#_=()~`";

        /// <summary>
        /// Regex to validate SSN
        /// </summary>
        public const string SOCIAL_SECURITY = @"^\d{3}[-]?\d{2}[-]?\d{4}$";

        /// <summary>
        /// Sql equal string
        /// </summary>
        public const string SQL_EQUAL = @"\=";

        /// <summary>
        /// Sql greater string
        /// </summary>
        public const string SQL_GREATER = @"\>";

        /// <summary>
        /// Sql greater or equal
        /// </summary>
        public const string SQL_GREATER_OR_EQUAL = @"\>.*\=";

        /// <summary>
        /// Sql Is
        /// </summary>
        public const string SQL_IS = @"\x20is\x20";

        /// <summary>
        /// Sql Is Not
        /// </summary>
        public const string SQL_IS_NOT = @"\x20is\x20not\x20";

        /// <summary>
        /// Sql Less
        /// </summary>
        public const string SQL_LESS = @"\<";

        /// <summary>
        /// Sql Less or Equal
        /// </summary>
        public const string SQL_LESS_OR_EQUAL = @"\<.*\=";

        /// <summary>
        /// Sql Like
        /// </summary>
        public const string SQL_LIKE = @"\x20like\x20";

        /// <summary>
        /// Sql Not Equal
        /// </summary>
        public const string SQL_NOT_EQUAL = @"\<.*\>";

        /// <summary>
        /// Sql Not Like
        /// </summary>
        public const string SQL_NOT_LIKE = @"\x20not\x20like\x20";

        /// <summary>
        /// Regex to validate if is an strong action
        /// </summary>
        public const string STRONG_PASSWORD = @"(?=^.{8,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*";

        /// <summary>
        /// Regex to locate SGML tags
        /// </summary>
        public const string SGML_TAGS = @"<(.|\n)*?>";

        /// <summary>
        /// Regex to validate upper cases
        /// </summary>
        public const string UPPER_CASE = @"^[A-Z]+$";

        /// <summary>
        /// Regex to validate urls
        /// </summary>
        public const string URL = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";

        /// <summary>
        /// Regex to validate US currency
        /// </summary>
        public const string US_CURRENCY = @"^\$(([1-9]\d*|([1-9]\d{0,2}(\,\d{3})*))(\.\d{1,2})?|(\.\d{1,2}))$|^\$[0](.00)?$";

        /// <summary>
        /// Regex to validate US telephone
        /// </summary>
        public const string US_TELEPHONE = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";

        /// <summary>
        /// Regex to validate US Zip code
        /// </summary>
        public const string US_ZIPCODE = @"^\d{5}$";

        /// <summary>
        /// Regex to validate US Zip code plus four
        /// </summary>
        public const string US_ZIPCODE_PLUS_FOUR = @"^\d{5}((-|\s)?\d{4})$";

        /// <summary>
        /// Regex to validate US Zip code plus four optionals
        /// </summary>
        public const string US_ZIPCODE_PLUS_FOUR_OPTIONAL = @"^\d{5}((-|\s)?\d{4})?$";

        #endregion Constants
    }
}