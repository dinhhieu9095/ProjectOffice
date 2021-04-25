using Unity;
using Unity.Lifetime;

namespace SurePortal.Core.Kernel.AmbientScope
{
    public static class UnityDependencyResolver
    {
        /// <summary>
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IUnityContainer DependencyResolve(IUnityContainer container)
        {
            container.RegisterType<IDbContextFactory, DefaultDbContextFactory>(new PerResolveLifetimeManager());
            return container;
        }
    }
}