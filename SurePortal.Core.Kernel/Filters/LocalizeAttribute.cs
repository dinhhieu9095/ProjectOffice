
namespace SurePortal.Core.Kernel.Filters
{
    public class LocalizeAttribute : OutputProcessorActionFilterAttribute
    {
        protected override string Process(string data)
        {
            // LOCALIZE RESOURCE BEFORE IT SEND TO THE CLIENT
            // SOMETHING LIKE
            // return data.Localize(); =]]

            return data;
        }
    }
}