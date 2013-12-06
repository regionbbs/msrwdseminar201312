using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;

namespace Demo04_HostingSignalROnConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8080";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Console-hosting SignalR application is running.");
                Console.WriteLine("URL: {0}", url);
                Console.WriteLine("Press ENTER to close.");
                Console.ReadLine();
            }
        }
    }
}
