using System;

namespace SurePortal.Core.Kernel.Models
{
    public class KtDataTableResponse
    {
        public KtDataTableResponse(Object data, KtPaging pageInfo)
        {
            Data = data;
            Meta = pageInfo;
        }

        public Object Data { get; set; }
        public KtPaging Meta { get; set; }
    }
}
