using SurePortal.Core.Kernel.Interfaces;
using SurePortal.Core.Kernel.Ioc;
using SurePortal.Core.Kernel.Orgs.Application;
using System;
using Unity;

namespace SurePortal.WebHost
{
    /// <summary>
    ///     Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        ///     Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        ///     There is no need to register concrete types such as controllers or
        ///     API controllers (unless you want to change the defaults), as Unity
        ///     allows resolving a concrete type even if it was not previously
        ///     registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            UnityHelper.BuildUnityContainer(container);

            Core.Kernel.AmbientScope.UnityDependencyResolver.DependencyResolve(container);

            // container.RegisterType<IDbContextFactory, DefaultDbContextFactory>(new PerResolveLifetimeManager());

            foreach (var task in container.ResolveAll<IUnityDependencyResolver>()) task.ResolveDependency(container);

            //  fetch info
            IUserDepartmentServices userDeptServices = (IUserDepartmentServices)container.Resolve(typeof(IUserDepartmentServices));
            userDeptServices.GetCachedUserDepartmentDtos();
        }

        #region Unity Container

        private static readonly Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            return container;
        });

        /// <summary>
        ///     Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        #endregion
    }
}