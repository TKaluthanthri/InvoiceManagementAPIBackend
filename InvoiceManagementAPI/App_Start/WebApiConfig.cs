using InvoiceManagementAPI.Repositories;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Unity;
using Unity.WebApi;
using System.Web.Http.Cors;

namespace InvoiceManagementAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

           
            var container = new UnityContainer();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IInvoiceRepository, InvoiceRepository>();
            container.RegisterType<IEmailRepository, EmailRepository>();

            config.DependencyResolver = new UnityDependencyResolver(container);

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            EnableCorsAttribute cores = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cores);

        }
    }
}
