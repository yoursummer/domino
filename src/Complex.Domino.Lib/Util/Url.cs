﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Complex.Domino.Util
{
    public static class Url
    {
        public static void RedirectTo(Uri url)
        {
            if (url != null)
            {
                HttpContext.Current.Response.Redirect(url.ToString());
            }
            else
            {
                HttpContext.Current.Response.Redirect("~");
            }
        }

        public static void RedirectTo(string url)
        {
            if (!String.IsNullOrWhiteSpace(url))
            {
                HttpContext.Current.Response.Redirect(url);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~");
            }
        }

        public static string ToBaseUrl(String url, string applicationPath)
        {
            var uri = new Uri(url);
            return String.Format("{0}://{1}{2}/", uri.Scheme, uri.Authority, applicationPath.TrimEnd('/'));
        }

        public static string GetAppUrl()
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                return string.Format("{0}://{1}",
                  context.Request.Url.Scheme,
                  context.Request.Url.Authority);
            }
            else
            {
                return null;
            }
        }

        public static string GetClientRedirect(string url)
        {
            return String.Format("javascript:location='{0}';", MakeRelativePath(url));
        }

        public static string GetClientPopUp(string url)
        {
            return String.Format("javascript:window.open('{0}');", MakeRelativePath(url));
        }

        private static string MakeRelativePath(string url)
        {
            if (HttpContext.Current != null && !String.IsNullOrEmpty(url) && VirtualPathUtility.IsAppRelative(url))
            {
                url = VirtualPathUtility.MakeRelative(
                    HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath,
                    url);
            }

            return url;
        }

        /// <summary>
        /// Converts the provided app-relative path into an absolute Url containing the 
        /// full host name
        /// </summary>
        /// <param name="relativeUrl">App-Relative path</param>
        /// <returns>Provided relativeUrl parameter as fully qualified Url</returns>
        /// <example>~/path/to/foo to http://www.web.com/path/to/foo</example>
        public static string ToAbsoluteUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return relativeUrl;

            if (HttpContext.Current == null)
                return relativeUrl;

            var url = HttpContext.Current.Request.Url;
            var port = url.Port != 80 ? (":" + url.Port) : String.Empty;

            if (relativeUrl.StartsWith("/"))
            {
                return String.Format("{0}://{1}{2}{3}",
                    url.Scheme, url.Host, port, relativeUrl);
            }
            else
            {
                string dir;

                var i = url.AbsolutePath.LastIndexOf('/');
                if (i > 0)
                {
                    dir = url.AbsolutePath.Substring(0, i);
                }
                else
                {
                    dir = "";
                }

                return String.Format("{0}://{1}{2}{3}/{4}",
                    url.Scheme, url.Host, port, dir, relativeUrl);
            }
        }

        public static string ArrayToUrlList(string[] array)
        {
            string res = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                {
                    res += ",";
                }

                res += array[i];
            }

            return res;
        }

        public static string ArrayToUrlList(Guid[] array)
        {
            string res = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 0)
                {
                    res += ",";
                }

                res += array[i];
            }

            return res;
        }

        public static int ParseInt(string parameter)
        {
            return int.Parse(parameter);
        }

        public static int ParseInt(string parameter, int defaultValue)
        {
            if (parameter == null)
            {
                return defaultValue;
            }
            else
            {
                return int.Parse(parameter);
            }
        }
    }
}
