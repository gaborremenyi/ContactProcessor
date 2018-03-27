using System.Web.Mvc;
using ContactProcessor.Email;
using ContactProcessor.IO;
using Microsoft.Practices.Unity;
using Unity.Mvc4;

namespace ContactProcessor
{
    public static class Bootstrapper
    {
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

            container.RegisterType<IStreamService, StreamService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<IEmailService, EmailService>();

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}