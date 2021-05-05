using Unity;

namespace DaiPhatDat.Core.Kernel.Interfaces
{
    public interface IUnityDependencyResolver
    {
        void ResolveDependency(IUnityContainer unityContainer);
    }
}