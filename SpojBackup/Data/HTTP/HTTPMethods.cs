using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Data.HTTP
{
    class HTTPMethods
    {
        public static string GET(string url, string referer, CookieContainer cookies)
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = "GET";
            webReq.CookieContainer = cookies;
            webReq.UserAgent = "";
            webReq.Referer = referer;

            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();

            string pageSrc;
            StreamReader sr = new StreamReader(webResp.GetResponseStream());
            pageSrc = sr.ReadToEnd();

            return pageSrc;


        }

        

        public static bool POST(string url, string postData, string referer, ref CookieContainer cookies)
        {
            string words = "Authentication failed!";
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
            webReq.Method = "POST";
            webReq.CookieContainer = cookies;
            webReq.UserAgent = "";
            webReq.Referer = referer;

            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";
            Stream postStream = webReq.GetRequestStream();
            byte[] postByte = Encoding.ASCII.GetBytes(postData);
            postStream.Write(postByte, 0, postByte.Length);
            postStream.Dispose();

            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
            cookies.Add(webResp.Cookies);

            string pageSrc;
            StreamReader sr = new StreamReader(webResp.GetResponseStream());
            pageSrc = sr.ReadToEnd();

            sr.Dispose();

            return (!pageSrc.Contains(words));
        }

        public static bool Login(ref CookieContainer cookie)
        {

            CookieContainer myCookies = cookie;

            //string mySrc = HTTPMethods.GET("https://www.spoj.com/login", "https://www.spoj.com/login", myCookies);
            Console.Write("Username: ");
            String user = Console.ReadLine();
            Console.Write("Password: ");
            String pass = Console.ReadLine();
            Console.Clear();
            String postData = "login_user=" + user + "&password=" + pass + "&next_raw=%2F";
            bool mySrc = HTTPMethods.POST("https://www.spoj.com/login", postData, "https://www.spoj.com/login", ref myCookies);

            if (mySrc)
            {

                Console.WriteLine("Successful Login!");
                return true;
            }
            else
            {

                Console.WriteLine("Unsuccessful Login!");
                return false;

            }

        }

        public static List<String> ListOfSolvedProblems(string username, CookieContainer cookie)
        {
            List<String> listOfProblems = new List<string>();
            
            string mySrc = GET("https://www.spoj.com/EIUDISC2/users/" + username, "https://www.spoj.com/EIUDISC2/users/" + username, cookie);

            
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(mySrc);
            var htmlNodes = htmlDoc.DocumentNode.SelectNodes("//table[@class='table table-condensed']/tr/td/a");
            foreach (var node in htmlNodes)
            {
                Console.WriteLine(node.InnerText);
            }
            foreach (string str in listOfProblems)
            {
                Console.WriteLine(str);
            }

            return listOfProblems;
        }

    }
}
