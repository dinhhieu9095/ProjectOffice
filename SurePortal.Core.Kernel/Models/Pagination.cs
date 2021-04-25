using System.Collections.Generic;

namespace SurePortal.Core.Kernel.Models
{
    public class Pagination<T>
    {

        public Pagination(int count, IEnumerable<T> result)
        {
            Count = count;
            Result = result;
        }
        public Pagination(int count, IEnumerable<T> result, int skip, int take)
        {
            Count = count;
            Result = result;
            Skip = skip;
            Take = take;
        }
        public int Count { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<T> Result { get; set; }
    }
}
