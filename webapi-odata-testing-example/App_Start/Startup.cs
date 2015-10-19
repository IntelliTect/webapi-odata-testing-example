﻿using System.Web.Http;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Example;
using Example.Data.Models;
using Example.Data.Services;
using Microsoft.OData.Edm;
using Microsoft.Owin;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly : OwinStartup( typeof (Startup) )]

namespace Example
{
    public class Startup
    {
        public void Configuration( IAppBuilder app )
        {
            var config = new HttpConfiguration();
            config.MapODataServiceRoute( "odataRoute",
                    null,
                    GetEdmModel() );


            config.EnsureInitialized();

            app.UseNinjectMiddleware( CreateKernel ).UseNinjectWebApi( config );
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder
                          {
                                  Namespace = "Example",
                                  ContainerName = "ExampleContainer"
                          };
            builder.EntitySet<Race>( "Races" );
            builder.EntitySet<Driver>( "Drivers" );

            return builder.GetEdmModel();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IRaceService>().To<RaceService>();
            kernel.Bind<IDriverService>().To<DriverService>();
            return kernel;
        }
    }
}