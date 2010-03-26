using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace DynamicProg.Net
{
    /// <summary>
    /// A class used to make Ftp operations
    /// </summary>
    public static class FtpClient
    {
        #region Static Fields

        private static readonly WebClient wc = new WebClient();

        #endregion Static Fields

        #region Private Methods

        private static FtpWebRequest Connect(Uri uri, string username, string password)
        {
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(uri);
            ftp.Credentials = new NetworkCredential(username, password);
            return ftp;
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Delete a file from the server
        /// </summary>
        /// <param name="uri">The uri of the file to delete</param>
        /// <param name="username">The username to log into the ftp server</param>
        /// <param name="password">The password to log into the ftp server</param>
        /// <returns>The FtpStatusCode</returns>
        public static FtpStatusCode DeleteFile(Uri uri, string username, string password)
        {
            FtpWebRequest ftp = Connect(uri, username, password);
            ftp.Method = WebRequestMethods.Ftp.DeleteFile;

            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();

            FtpStatusCode code = response.StatusCode;
            response.Close();

            return code;
        }

        /// <summary>
        /// Downloads a file an return its content as a string
        /// </summary>
        /// <param name="uri">The URI object with the address to the file to download</param>
        /// <param name="username">The username to log into the ftp server</param>
        /// <param name="password">The password to log into the ftp server</param>
        /// <returns>String with the contents of the file encoded as UTF8</returns>
        public static string DownloadFile(Uri uri, string username, string password)
        {
            wc.Credentials = new NetworkCredential(username, password);
            byte[] newFileData = wc.DownloadData(uri.ToString());
            return System.Text.Encoding.UTF8.GetString(newFileData);
        }

        /// <summary>
        /// Downloads a file into a given local path
        /// </summary>
        /// <param name="uri">The URI object with the address to the file to download</param>
        /// <param name="username">The username to log into the ftp server</param>
        /// <param name="password">The password to log into the ftp server</param>
        /// <param name="filePath">The path where to save the file, includes file name</param>
        /// <returns>True if successfull, else and exception is thrown</returns>
        public static bool DownloadFile(Uri uri, string username, string password, string filePath)
        {
            wc.Credentials = new NetworkCredential(username, password);
            wc.DownloadFile(uri, filePath);
            return true;
        }

        /// <summary>
        /// Returns a list of folders and files from the server
        /// </summary>
        /// <param name="uri">The uri ending on / to get the list of files</param>
        /// <param name="username">The username to log into the ftp server</param>
        /// <param name="password">The password to log into the ftp server</param>
        /// <returns>A list with the names of all folders under the given uri</returns>
        public static List<string> GetFileList(Uri uri, string username, string password)
        {
            FtpWebRequest ftp = Connect(uri, username, password);
            ftp.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());

            List<string> l = new List<string>();
            while (!reader.EndOfStream)
            {
                l.Add(reader.ReadLine());
            }
            return l;
        }

        /// <summary>
        /// Upload a file to the server
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <param name="username">The username to log into the ftp server</param>
        /// <param name="password">The password to log into the ftp server</param>
        /// <param name="filePath">The path for the file to upload</param>
        /// <param name="remoteName">The name of the file in the server</param>
        /// <returns>The FtpStatusCode</returns>
        public static FtpStatusCode UploadFile(Uri uri, string username, string password, string filePath, string remoteName)
        {
            FtpWebRequest ftp = Connect(new Uri(uri.AbsoluteUri + @"/" + remoteName), username, password);
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(filePath);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            ftp.ContentLength = fileContents.Length;

            Stream requestStream = ftp.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
            FtpStatusCode code = response.StatusCode;
            response.Close();

            return code;
        }

        #endregion Public Methods
    }
}