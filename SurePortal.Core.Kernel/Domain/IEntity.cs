
namespace SurePortal.Core.Kernel.Domain
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
