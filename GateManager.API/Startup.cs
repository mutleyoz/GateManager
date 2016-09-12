using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Swashbuckle.Application;
using System.Net.Http.Formatting;

[assembly: OwinStartup(typeof(GateManager.API.Startup))]

namespace GateManager.API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

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
            
            app.UseWebApi(config);
        }
    }
}
