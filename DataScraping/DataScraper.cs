using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.PhantomJS;

namespace DataScraping
{
    public class DataScraper
    {
        CookieContainer sessionCookie;

        public string getHTMLPage(string urlString)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Without this line, the request cannot process https

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(urlString, UriKind.Absolute));
            sessionCookie = new CookieContainer();
            request.CookieContainer = sessionCookie;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return html;
        }

        public string getHTMLPageUsingPhantomJS(string urlString)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // Without this line, the request cannot process https

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(urlString, UriKind.Absolute));
            sessionCookie = new CookieContainer();
            sessionCookie.Add(SetUpPhantomJS(urlString));
            request.CookieContainer = sessionCookie;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return html;
        }

        public void WriteHtmlToFile (string html)
        {
            StreamWriter sw = new StreamWriter("html.txt");
            sw.WriteLine(html);
        }

        private System.Net.Cookie SetUpPhantomJS(string link)
        {
            System.Net.Cookie cookie = new System.Net.Cookie();
            
            // PhantomJS seems to only work with .NET 4.5 or below.

            PhantomJSOptions options = new PhantomJSOptions();
            options.AddAdditionalCapability("IsJavaScriptEnabled", true);
            PhantomJSDriver driver = new PhantomJSDriver(options);

            // https://stackoverflow.com/questions/46666084/how-to-deal-with-javascript-when-fetching-http-response-in-c-sharp-using-httpweb
            // https://riptutorial.com/phantomjs
            driver.Url = link;
            driver.Navigate();

            var cookies = driver.Manage().Cookies.AllCookies;

            foreach (var c in cookies)
            {
                if (c.Name.Equals("TSPD_101"))
                {
                    cookie.Name = c.Name;
                    cookie.Domain = c.Domain;
                    cookie.Value = c.Value;
                    cookie.Path = c.Path;
                    cookie.Expires = DateTime.Now.AddHours(6);
                }
            }

            driver.Close();
            driver.Dispose();

            return cookie;
        }
    }
}
