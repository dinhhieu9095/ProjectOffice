
namespace DaiPhatDat.Core.Kernel.Firebase.Models
{
    public class SendMessageResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public bool IsFailed => !IsSuccess;

        public static SendMessageResponse CreateSuccessResponse(string message)
        {
            return new SendMessageResponse()
            {
                IsSuccess = true,
                Message = message
            };
        }

        public static SendMessageResponse CreateFailedResponse(string message)
        {
            return new SendMessageResponse()
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}
