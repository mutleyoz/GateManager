using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GateManager.API
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
//            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors();
 
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });
        }
    }
}