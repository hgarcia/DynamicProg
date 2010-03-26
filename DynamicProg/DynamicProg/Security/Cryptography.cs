using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DynamicProg.Security
{
    /// <summary>
    /// Contains encryption and decryption methods
    /// </summary>
    public static class Cryptography
    {
        #region Public Methods

        /// <summary>
        /// MD5 encode a given string
        /// </summary>
        /// <param name="valueToEncrypt">The string to encrypt</param>
        /// <returns>The encrypted string</returns>
        public static string MD5encrypt(string valueToEncrypt)
        {
            var crypto = new MD5CryptoServiceProvider();
            var encoder = new UTF8Encoding();

            byte[] bs = crypto.ComputeHash(encoder.GetBytes(valueToEncrypt));
            return BitConverter.ToString(bs).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// Encrypts a string using the Rijandel method of encryption
        /// </summary>
        /// <param name="strEncryptionKey">The string used to salt the encryption</param>
        /// <param name="strToken">The string to encrypt</param>
        /// <returns>The encrypted string</returns>
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        public static string RijandelEncrypt(string strEncryptionKey, string strToken)
        {
            if (string.IsNullOrEmpty(strToken))
            {
                throw new ArgumentNullException("strToken", " value of 'strEncryptionKey' is:" + strEncryptionKey);
            }

            if (string.IsNullOrEmpty(strEncryptionKey))
            {
                throw new ArgumentNullException("strEncryptionKey", " value of 'strToken' is:" + strToken);
            }

            string strPlainToken = strToken.Replace("|", "\x1F");
            var aMethod = new RijndaelManaged {KeySize = 256, Padding = PaddingMode.PKCS7, Mode = CipherMode.ECB};

            var aEncryptStream = new CryptoStream(new MemoryStream(Encoding.ASCII.GetBytes(strPlainToken)),
                                                  aMethod.CreateEncryptor(Encoding.ASCII.GetBytes(strEncryptionKey),
                                                                          null), CryptoStreamMode.Read);
            var aEncodeStream = new CryptoStream(aEncryptStream, new ToBase64Transform(), CryptoStreamMode.Read);
            var aReader = new StreamReader(aEncodeStream);

            string strEncodedToken = aReader.ReadToEnd();
            strEncodedToken = strEncodedToken.Replace("+", "!");
            strEncodedToken = strEncodedToken.Replace("/", "*");
            // Return result
            return strEncodedToken;
        }

        #endregion Public Methods
    }
}