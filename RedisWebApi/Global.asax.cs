using Autofac;
using Autofac.Integration.WebApi;
using RedisWebApi.Data;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RedisWebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        public IRedisClientsManager ClientManager;
        private const string RedisUri = "localhost";

        protected void Application_Start()
        {
            ClientManager = new PooledRedisClientManager(RedisUri);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            ConfigureDependencyResolver(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void ConfigureDependencyResolver(HttpConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterType<CustomerRepository>()
                .As<ICustomerRepository>()
                .PropertiesAutowired()
                .InstancePerApiRequest();

            builder.RegisterType<OrderRepository>()
                .As<IOrderRepository>()
                .PropertiesAutowired()
                .InstancePerApiRequest();

            builder.Register<IRedisClient>(c => ClientManager.GetClient())
                .InstancePerApiRequest();

            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }

        protected void Application_End()
        {
            ClientManager.Dispose();
        }
    }


}