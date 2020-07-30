using Autofac;
using Movies.Business;
using Movies.Models;
using Movies.Interfaces.Repository;
using System;

namespace Movies.Server.SelfHost.Configuration
{
    public class AutofacConfigurator
    {
        #region Fields
        private static readonly Lazy<AutofacConfigurator> fInstance = new Lazy<AutofacConfigurator>(() => new AutofacConfigurator());
        #endregion

        #region Properties
        public IContainer Container { get; }

        public static AutofacConfigurator Instance
        {
            get
            {
                return fInstance.Value;
            }
        }
        #endregion

        #region Public methods
        public void ConfigureDependencyInjection()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<GenderBusiness>().As<IRepository<Gender>>().SingleInstance();
     /*       builder.RegisterType<OpenWeatherClient>().As<IOpenWeatherClient>().SingleInstance();
            builder.RegisterType<Clock>().As<IClock>();
            fContainer = builder.Build();*/
        }
        #endregion
    }
}
