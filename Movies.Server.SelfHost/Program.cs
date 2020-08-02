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
            // Configure the Autofac Dependeny injection
            AutofacConfigurator.Instance.ConfigureDependencyInjection();
            
            // Define the appplication addres
            string baseAddress = "http://localhost:9000/";            

            // Initiates the OWIN Server
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Hosting the application server");
                Console.WriteLine($"Use {baseAddress} to access it");
                Console.ReadLine();
            }
        }
    }
}
