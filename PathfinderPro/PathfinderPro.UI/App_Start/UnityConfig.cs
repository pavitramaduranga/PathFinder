using PathfinderPro.Bussiness;
using PathfinderPro.Bussiness.Interfaces;
using System;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace PathfinderPro.UI
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register your types here
            container.RegisterType<IGraphService, GraphService>();
            container.RegisterType<IPathfinderService, PathfinderService>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}