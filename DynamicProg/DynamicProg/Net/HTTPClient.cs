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
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using LaTrompa.Validation;

namespace DynamicProg.Net
{
    /// <summary>
    /// This class is used to log with the https protocols
    /// </summary>
    public class CertificateOverride
    {
        #region Public Methods

        /// <summary>
        /// Validates a remote certificate
        /// </summary>
        /// <param name="sender">An object that sends the certificate</param>
        /// <param name="certificate">The Certificate</param>
        /// <param name="chain">An X509Chain</param>
        /// <param name="policyErrors">Errors for the policy</param>
        /// <returns>True</returns>
        public bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate,
                                                        X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }

        #endregion Public Methods
    }

    /// <summary>
    /// A class to handle HTTP operations
    /// </summary>
    public static class HttpClient
    {
        #region Private Methods

        private static string getWebPageUsingPost(string postURL, string postString)
        {
            var uri = new Uri(postURL + "?" + postString);

            byte[] bytedata = Encoding.UTF8.GetBytes(postString);

            WebRequest request = WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentLength = bytedata.Length;
            request.ContentType = "application/x-www-form-urlencoded";

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytedata, 0, bytedata.Length);
            requestStream.Close();

            WebResponse wr = request.GetResponse();

            Stream stream = wr.GetResponseStream();

            var sr = new StreamReader(stream);

            string webPage = sr.ReadToEnd();

            sr.Close();
            return webPage;
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Fetches a web page under SSL using GET
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <param name="userName">The user name</param>
        /// <param name="password">The password</param>
        /// <returns>A string with the content of the web page</returns>
        public static string GetSecureWebPage(Uri uri, string userName, string password)
        {
            var overrride = new CertificateOverride();
            ServicePointManager.ServerCertificateValidationCallback = overrride.RemoteCertificateValidationCallback;
                //new TrustAllCertificatePolicy();

            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Credentials = new NetworkCredential(userName, password);

            Stream stream = request.GetResponse().GetResponseStream();
            var sr = new StreamReader(stream);
            string webPage = sr.ReadToEnd();
            sr.Close();

            return webPage;
        }

        /// <summary>
        /// Fetches a web page using GET
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <returns>A string with the content of the web page</returns>
        public static string GetWebPage(Uri uri)
        {
            var request = WebRequest.Create(uri);
            var stream = request.GetResponse().GetResponseStream();
            var sr = new StreamReader(stream);
            var webPage = sr.ReadToEnd();
            sr.Close();

            return webPage;
        }

        /// <summary>
        /// Fetches a web page under SSL using POST
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <param name="userName">The user name</param>
        /// <param name="password">The password</param> 
        /// <returns>A string with the content of the web page</returns>
        /// <exception cref="ArgumentNullException"><c>userName</c> is null.</exception>
        public static string PostSecureWebPage(Uri uri, string userName, string password)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            if (String.IsNullOrEmpty(userName)) throw new ArgumentNullException("userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentNullException("password");
            var overrride = new CertificateOverride();
            ServicePointManager.ServerCertificateValidationCallback = overrride.RemoteCertificateValidationCallback;
            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Credentials = new NetworkCredential(userName, password);

            var stream = request.GetResponse().GetResponseStream();
            var sr = new StreamReader(stream);
            var webPage = sr.ReadToEnd();
            sr.Close();

            return webPage;
        }

        /// <summary>
        /// Fetches a web page using POST without passing any parameters
        /// </summary>
        /// <param name="postURL">The URL.</param>
        /// <returns>A string with the content of the web page</returns>
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        /// <exception cref="ArgumentException">postURL</exception>
        public static string PostWebPage(string postURL)
        {
            if (String.IsNullOrEmpty(postURL))
            {
                throw new ArgumentNullException("postURL","Please enter a non empty string for the url");
            }
            if (!Validate.IsURL(postURL))
            {
                throw new ArgumentException("Please pass the valid url");
            }

            return getWebPageUsingPost(postURL, string.Empty);
        }

        /// <summary>
        /// Fetches a web page using POST
        /// </summary>
        /// <param name="postURL">The URL.</param>
        /// <param name="postString">Post Elements</param>
        /// <returns>A string with the content of the web page</returns>
        /// <exception cref="ArgumentException">Post String contains invalid characters</exception>
        /// <exception cref="ArgumentNullException">Argument is null.</exception>
        public static string PostWebPage(string postURL, string postString)
        {
            if (String.IsNullOrEmpty(postURL))
            {
                throw new ArgumentNullException("postURL","Please enter a non empty string for the url");
            }
            if (!Validate.IsURL(postURL))
            {
                throw new ArgumentException("Please pass the valid url");
            }

            if (String.IsNullOrEmpty(postString))
            {
                throw new ArgumentNullException("postString","Please enter valid Post AddParameters");
            }
            if (postString.IndexOf("?") != -1)
            {
                throw new ArgumentException("Post String contains invalid characters");
            }

            return getWebPageUsingPost(postURL, postString);
        }

        /// <summary>
        /// Scrapes the image tags from a given URL
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <returns>string array of all images on a page</returns>
        public static string[] ScrapeImages(Uri uri)
        {
            //get the content of the url
            //ReadWebPage is another method in this useful methods collection
            string htmlPage = GetWebPage(uri);

            //set up the regex for finding images
            var imgPattern = new StringBuilder();
            imgPattern.Append("<img[^>]+"); //start 'img' tag
            imgPattern.Append("src\\s*=\\s*"); //start src property
            //three possibilities  for what src property --
            //(1) enclosed in double quotes
            //(2) enclosed in single quotes
            //(3) enclosed in spaces
            imgPattern.Append("(?:\"(?<src>[^\"]*)\"|'(?<src>[^']*)'|(?<src>[^\"'>\\s]+))");
            imgPattern.Append("[^>]*>"); //end of tag
            var imgRegex = new Regex(imgPattern.ToString(), RegexOptions.IgnoreCase);

            //look for matches 
            Match imgcheck = imgRegex.Match(htmlPage);
            var imagelist = new ArrayList {"<BASE href=\"" + uri + "\">" + uri};
            //add base href for relative urls

            while (imgcheck.Success)
            {
                string src = imgcheck.Groups["src"].Value;
                string image = "<img src=\"" + src + "\">";
                imagelist.Add(image);
                imgcheck = imgcheck.NextMatch();
            }

            var images = new string[imagelist.Count];
            imagelist.CopyTo(images);

            return images;
        }

        /// <summary>
        /// Scrapes a web page and parses out all the links.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <param name="makeLinkable">if set to <c>true</c> [make linkable].</param>
        /// <returns></returns>
        public static string[] ScrapeLinks(Uri uri, bool makeLinkable)
        {
            //get the content of the url
            //ReadWebPage is another method in this useful methods collection
            string htmlPage = GetWebPage(uri);

            //set up the regex for finding the link urls
            var hrefPattern = new StringBuilder();
            hrefPattern.Append("<a[^>]+"); //start 'a' tag and anything that comes before 'href' tag
            hrefPattern.Append("href\\s*=\\s*"); //start href property
            //three possibilities  for what href property --
            //(1) enclosed in double quotes
            //(2) enclosed in single quotes
            //(3) enclosed in spaces
            hrefPattern.Append("(?:\"(?<href>[^\"]*)\"|'(?<href>[^']*)'|(?<href>[^\"'>\\s]+))");
            hrefPattern.Append("[^>]*>.*?</a>"); //end of 'a' tag
            var hrefRegex = new Regex(hrefPattern.ToString(), RegexOptions.IgnoreCase);

            //look for matches 
            Match hrefcheck = hrefRegex.Match(htmlPage);
            var linklist = new ArrayList {"<BASE href=\"" + uri + "\">" + uri};
            //add base href for relative links
            while (hrefcheck.Success)
            {
                string href = hrefcheck.Groups["href"].Value; //link url
                string link = (makeLinkable)
                                  ? "<a href=\"" + href + "\" target=\"_blank\">" + href + "</a>"
                                  : href;
                linklist.Add(link);
                hrefcheck = hrefcheck.NextMatch();
            }
            var links = new string[linklist.Count];
            linklist.CopyTo(links);

            return links;
        }

        #endregion Public Methods
    }
}