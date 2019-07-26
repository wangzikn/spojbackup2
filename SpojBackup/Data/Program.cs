using Data.HTTP;
using System;
using System.Net;

namespace Data
{
    class Program
    {
        static void Main(string[] args)
        {
            CookieContainer myCookies = new CookieContainer();
            HTTPMethods.Login(ref myCookies);
            string username = "eis19vinhng";
            HTTPMethods.ListOfSolvedProblems(username, myCookies);
            Console.ReadKey();
        }
    }
}
