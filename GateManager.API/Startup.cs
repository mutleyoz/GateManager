using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Swashbuckle.Application;
using System.Net.Http.Formatting;
using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using GateManager.Repository;

[assembly: OwinStartup(typeof(GateManager.API.Startup))]

namespace GateManager.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ContainerBuilder builder = new ContainerBuilder();

            // Autofac

            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<GateRepository>().As<IGateRepository>();

            config.EnableSwagger(c => c.SingleApiVersion("v1", "GateManager API")).EnableSwaggerUi();


            // Formatting
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes(); 
            config.Routes.IgnoreRoute("IgnoreAxd", "{resource}.axd/{*pathInfo}");

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseWebApi(config);
        }
    }
}
