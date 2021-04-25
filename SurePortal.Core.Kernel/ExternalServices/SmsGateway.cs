using System;
using System.Net;
using System.Threading.Tasks;

namespace SurePortal.Core.Kernel.ExternalServices
{
    public static class SmsGateway
    {
        public const string ProviderConfigKey = "SmsGateWay";
        public const string ServerUrlConfigKey = "SmsGateWay_ServerUrl";


        public async static Task<SmsSendResponse> SendQuick_Send(SmsSendRequest sendRequest)
        {
            SmsSendResponse response = new SmsSendResponse();
            try
            {
                //http://192.168.11.205/client/107254/sendsms.php 

                //                Thông số(parameter) truyền vào:
                //-tar_num: target mobile number(số điện thoại người nhận)
                //-tar_msg: target message(tin nhắn OTP)
                //Ví dụ: tar_num = 0908172938 & tar_msg =”My OTP code is 2346”
                //tar_number=0908172938&tar_msg=”My OTP code is 2346”
                using (WebClient webClient = new WebClient())
                {
                    var url = $"{sendRequest.ServerUrl}?tar_num={sendRequest.MobileNumber}" +
                        $"&tar_msg={sendRequest.Text.Trim()}" +
                        $"&tar_mode={(sendRequest.IsUnicode ? "utf8" : "text")}";
                    var content = webClient.DownloadString(new Uri(url));
                    webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
                    response.IsSuccess = true;
                    response.Message = content;
                }
            }
            catch (System.Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        private static void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }


    public class SmsSendRequest
    {
        /// <summary>
        /// Số đt phải theo mẫu +84....
        /// </summary>
        public string MobileNumber { get; set; }

        public string Text { get; set; }

        public bool IsUnicode { get; set; }

        public string ServerUrl { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ClientCode { get; set; }
    }

    public class SmsSendResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }
    }
}
