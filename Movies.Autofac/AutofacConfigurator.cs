using Autofac;
using Movies.Database;
using Movies.Interfaces.Repository;
using Movies.Models;
using Movies.Repositories;
using System;

namespace Movies.Autofac
{
    /// <summary>
    /// Autofac Class responsible to configure dependcy injection and the class types container    
    /// </summary>
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

            // Defining the context as Singleton will make sure that operations with different business classes 
            // will be in the same context and operation of one of them will be correctly applied by the others
            builder.RegisterType<MoviesDBContext>().SingleInstance();

            builder.RegisterType<GenderRepository>().As<IRepository<Gender>>().SingleInstance();
            builder.RegisterType<MovieRepository>().As<IRepository<Movie>>().SingleInstance();
            builder.RegisterType<RentalRepository>().As<IRepository<Rental>>().SingleInstance();
            fContainer = builder.Build();
        }
        #endregion
    }
}
