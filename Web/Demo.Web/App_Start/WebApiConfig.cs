using Demo.DomainModel;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;

namespace Demo.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<Employee>("Employees");
            builder.EntitySet<Designation>("Designations");
            builder.EntitySet<EmployeeLanguages>("EmployeeLanguages");
            var isemailUnique = builder.Entity<Employee>().Action("IsEmailUnique");
            isemailUnique.Parameter<string>("email");
            isemailUnique.Returns<bool>();

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            //// Web API configuration and services

            //// Web API routes

            config.MapHttpAttributeRoutes();

            ////singleton
            //var cache = new DemoMemoryCache();
            //config.CacheOutputConfiguration().RegisterCacheOutputProvider(() => cache);
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}