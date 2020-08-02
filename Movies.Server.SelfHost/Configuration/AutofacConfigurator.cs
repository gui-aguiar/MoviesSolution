﻿using Autofac;
using Movies.Models;
using Movies.Interfaces.Repository;
using System;
using Movies.Database;
using Movies.Repositories;

namespace Movies.Server.SelfHost.Configuration
{
    public class AutofacConfigurator
    {
        #region Fields
        private static readonly Lazy<AutofacConfigurator> fInstance = new Lazy<AutofacConfigurator>(() => new AutofacConfigurator());
        IContainer fContainer;
        #endregion

        #region Properties
        public IContainer Container
        {
            get
            {
                return fContainer;
            }
        }

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

            builder.RegisterType<MoviesDBContext>().SingleInstance();
            builder.RegisterType<GenderRepository>().As<IRepository<Gender>>().SingleInstance();
            builder.RegisterType<MovieRepository>().As<IRepository<Movie>>().SingleInstance();
            builder.RegisterType<RentalRepository>().As<IRepository<Rental>>().SingleInstance();
            fContainer = builder.Build();
        }
        #endregion
    }
}
