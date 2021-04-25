
namespace SurePortal.Core.Kernel.Models.Responses
{
    public class MobileResponse<T>
    {
        public MobileStatusCode StatusCode { get; set; }

        public string StatusText
        {
            get
            {
                return StatusCode.ToString();
            }
        }

        public string Message { get; set; } = string.Empty;

        public T Data { get; set; }

        public static MobileResponse<T> Create(MobileStatusCode status, string message, T data)
        {
            return new MobileResponse<T>()
            {
                Data = data,
                Message = message,
                StatusCode = status
            };
        }
    }


    public enum MobileStatusCode
    {

        Success = 1,
        Error = 2,
        UnAuthorized = 3,
        BadRequest = 4
    }
}
