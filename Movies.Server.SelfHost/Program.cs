using Movies.Server.SelfHost.Configuration;
using Microsoft.Owin.Hosting;
using System;
using Movies.Autofac;

namespace Movies.Server.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            AutofacConfigurator.Instance.ConfigureDependencyInjection();

            string baseAddress = "http://localhost:9000/";            

            // Inicia o host OWIN 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Web Api Self-Host...");
                Console.ReadLine();
            }
        }
    }
}
