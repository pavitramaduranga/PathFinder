using PathfinderPro.Business;
using PathfinderPro.Business.Interfaces;
using System;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace PathfinderPro.UI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IGraphService, GraphService>(new ContainerControlledLifetimeManager());
            container.RegisterType<IPathfinderService, PathfinderService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}