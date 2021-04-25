using Unity;

namespace SurePortal.Core.Kernel.Interfaces
{
    public interface IUnityDependencyResolver
    {
        void ResolveDependency(IUnityContainer unityContainer);
    }
}