using System;
using DynamicProg.Extensions;
using LaTrompa;
using LaTrompa.Extensions;

namespace DynamicProg.Security
{
    /// <summary>
    /// This class holds old the authentication methods used in our applications.
    /// </summary>
    public class Authentication
    {
        private static readonly int _tolerance;

        static Authentication()
        {
            if (String.IsNullOrEmpty(Utils.GetAppSetting("AuthenticationTimeTolerance")))
            {
                _tolerance = 3;
            }else
            {
                _tolerance = Convert.ToInt32(Utils.GetAppSetting("AuthenticationTimeTolerance"));
            }
        }

        #region Constants

        private const string DATEFORMAT = "yyyy-MM-dd";

        #endregion Constants

        #region Public Properties

        ///<summary>
        /// Returns the format of the date to use in a timed token.
        ///</summary>
        public static String DateFormat
        {
            get
            {
                return DATEFORMAT;
            }
        }

        #endregion Public Properties

        #region Private Methods

        private static string getUtcDate(int daysDiff)
        {
            return daysDiff.DaysAgo().ToUniversalTime().ToString(DateFormat);
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Verify that a temporary token is valid, order of parameters indicates the order
        /// that the strings are encrypted
        /// </summary>
        /// <param name="publicKey">The public key, this is usually the member id</param>
        /// <param name="semiPublicKey">The semi public key, this is usually the IP address</param>
        /// <param name="privateKey">The private key, this is usually the password</param>
        /// <param name="token">The token</param>
        /// <returns>True or False</returns>
        /// <remarks>The token is only valid for 3 days (today, yesterday and day before yesterday)</remarks>
        public static bool IsValidTimedToken(string publicKey, string semiPublicKey, string privateKey, string token)
        {
            bool valid = false;
            int diff = 0;

            while (!valid && diff < _tolerance)
            {
                valid = (token == Cryptography.MD5encrypt(publicKey + semiPublicKey + privateKey + getUtcDate(diff)));
                diff++;
            }
            return valid;
        }

        /// <summary>
        /// Verify that a permanent token is valid, order of parameters indicates the order
        /// that the strings are encrypted
        /// </summary>
        /// <param name="publicKey">The public key, this is usually the member id</param>
        /// <param name="semiPublicKey">The semi public key, this is usually the IP address</param>
        /// <param name="privateKey">The private key, this is usually the password</param>
        /// <param name="token">The token</param>
        /// <returns>True or False</returns>
        public static bool IsValidToken(string publicKey, string semiPublicKey, string privateKey, string token)
        {
            return (token == Cryptography.MD5encrypt(publicKey + semiPublicKey + privateKey));
        }

        #endregion Public Methods
    }
}