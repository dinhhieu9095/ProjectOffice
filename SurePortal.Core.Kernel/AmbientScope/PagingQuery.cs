
namespace SurePortal.Core.Kernel.AmbientScope
{
    public class PagingQuery
    {
        public PagingQuery(int skip = 0, int take = 10)
        {
            Skip = skip;
            Take = take;
        }

        public int Skip { get; }
        public int Take { get; }
    }
}