using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ws_fuel_control
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApiWithId", "Api/{controller}/{id}", (object)new
            {
                id = RouteParameter.Optional
            }, (object)new { id = "\\d+" });

            config.Routes.MapHttpRoute("DefaultApiWithAction", "Api/{controller}/{action}");

            config.Routes.MapHttpRoute("DefaultApiGet", "Api/{controller}", (object)new
            {
                action = "Get"
            }, (object)new
            {
                httpMethod = new HttpMethodConstraint(new HttpMethod[1]
                {
                    HttpMethod.Get
                })
            });

            config.Routes.MapHttpRoute("DefaultApiPost", "Api/{controller}", (object)new
            {
                action = "Post"
            }, (object)new
            {
                httpMethod = new HttpMethodConstraint(new HttpMethod[1]
                {
                    HttpMethod.Post
                })
            });

        }

        //public static void Register(HttpConfiguration config)
        //{
        //    // Configuración y servicios de API web

        //    // Rutas de API web
        //    config.MapHttpAttributeRoutes();

        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}
    }
}
