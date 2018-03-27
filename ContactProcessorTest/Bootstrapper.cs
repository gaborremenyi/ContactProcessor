using System.Web.Mvc;
using ContactProcessor.IO;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace ContactProcessorTest
{
    public static class Bootstrapper
    {
        static Bootstrapper()
        {
            Bootstrapper.Initialise();
        }

        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IFileService, FileService>();
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}