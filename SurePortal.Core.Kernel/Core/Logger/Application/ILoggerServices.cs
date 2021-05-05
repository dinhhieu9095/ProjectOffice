
namespace DaiPhatDat.Core.Kernel.Logger.Application
{
    /// <summary>
    ///
    /// </summary>
    public interface ILoggerServices
    {
        void WriteDebug(string message);

        void WriteError(string message);

        void WriteInfo(string message);

        void WriteWarn(string message);
    }
}