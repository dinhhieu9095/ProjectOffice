using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SurePortal.Core.Kernel.ExternalServices
{
    /// <summary>
    /// Cổng kết nối của VNPT Directs
    /// </summary>
    public static class SmsVNPTBranchGateway
    {
        public const string ConfigServerAddress = "VNPTBRanch_ServerAddress";
        public const string ConfigLABELID = "VNPTBRanch_LABELID";
        public const string ConfigCONTRACTID = "VNPTBRanch_CONTRACTID";
        public const string ConfigCONTRACTTYPEID = "VNPTBRanch_CONTRACTTYPEID";
        public const string ConfigTEMPLATEID = "VNPTBRanch_TEMPLATEID";
        public const string ConfigAGENTID = "VNPTBRanch_AGENTID";
        public const string ConfigAPIUSER = "VNPTBRanch_APIUSER";
        public const string ConfigAPIPASS = "VNPTBRanch_APIPASS";
        public const string ConfigUSERNAME = "VNPTBRanch_USERNAME";



        private static bool ValidateServerCertificate(object sender,
            X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        /// <summary>
        /// Gửi tin nhắn
        /// </summary>
        /// <param name="smsMessageRequests"></param>
        /// <returns></returns>
        public static List<SmsSendResponse> SendMessage(VNPTBranchSetting branchSetting,
            List<VnptDirectParam> vnptDirectParams, string phoneNumber)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(ValidateServerCertificate);
            var smsReponses = new List<SmsSendResponse>();

            var request = HttpWebRequest.Create(branchSetting.ServerAddress) as HttpWebRequest;

            var param = new
            {
                RQST = new VnptDirectRequest()
                {
                    AGENTID = branchSetting.AGENTID,
                    APIPASS = branchSetting.APIPASS,
                    APIUSER = branchSetting.APIUSER,
                    CONTRACTID = branchSetting.CONTRACTID,
                    CONTRACTTYPEID = branchSetting.CONTRACTTYPEID,
                    DATACODING = "0",
                    ISTELCOSUB = "0",
                    LABELID = branchSetting.LABELID,
                    MOBILELIST = ConvertVNPhone(phoneNumber),
                    name = "send_sms_list",
                    PARAMS = vnptDirectParams,
                    REQID = DateTime.Now.Ticks.ToString(),
                    SCHEDULETIME = "",
                    TEMPLATEID = branchSetting.TEMPLATEID,
                    USERNAME = branchSetting.USERNAME

                }
            };
            request.Method = "POST";
            request.ContentType = "application/json";
            SetRequestData(request, param);
            string result = GetRequestData(request);
            smsReponses.Add(new SmsSendResponse() { IsSuccess = true, Message = result });


            return smsReponses;
        }
        public static List<SmsSendResponse> TestSendMessage(string serverAddress, string data)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(ValidateServerCertificate);
            var smsReponses = new List<SmsSendResponse>();
            var request = HttpWebRequest.Create(serverAddress) as HttpWebRequest;
            var param = new
            {
                RQST = JsonConvert.DeserializeObject<VnptDirectRequest>(data)
            };
            request.Method = "POST";
            request.ContentType = "application/json";
            SetRequestData(request, param);
            string result = GetRequestData(request);
            smsReponses.Add(new SmsSendResponse() { IsSuccess = true, Message = result });
            return smsReponses;
        }
        private static string ConvertVNPhone(string phone)
        {
            return "84" + phone.TrimStart('0');
        }
        private static void SetRequestData(HttpWebRequest request, object data)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new DefaultContractResolver()
            };
            var jsonstring = JsonConvert.SerializeObject(data, settings);
            byte[] dataParm = System.Text.Encoding.UTF8.GetBytes(jsonstring);
            request.ContentLength = dataParm.Length;
            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(dataParm, 0, dataParm.Length);
            }
        }
        private static string GetRequestData(HttpWebRequest request)
        {
            string jsonData = string.Empty;
            using (var httpWebResponse = request.GetResponse() as HttpWebResponse)
            {
                if (httpWebResponse != null && httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        jsonData = streamReader.ReadToEnd();
                    }
                }
            }
            return jsonData;

        }

    }

    public struct VNPTBranchSetting
    {
        public string ServerAddress { get; set; }
        public string LABELID { get; set; }
        public string CONTRACTID { get; set; }
        public string CONTRACTTYPEID { get; set; }
        public string TEMPLATEID { get; set; }
        public string AGENTID { get; set; }
        public string APIUSER { get; set; }
        public string APIPASS { get; set; }
        public string USERNAME { get; set; }
    }

    public class VnptDirectSms
    {
        /// <summary>
        /// send_sms_list,
        /// </summary>
        public string name { get; set; }
    }

    public class VnptDirectRequest : VnptDirectSms
    {
        /// <summary>
        /// "[request_id]"
        /// </summary>
        public string REQID { get; set; }

        /// <summary>
        /// [label_id]
        /// </summary>
        public string LABELID { get; set; }

        /// <summary>
        /// [contract_type_id]
        /// </summary>
        public string CONTRACTTYPEID { get; set; }

        /// <summary>
        /// [contract_id]
        /// </summary>
        public string CONTRACTID { get; set; }

        /// <summary>
        /// [template_id]
        /// </summary>
        public string TEMPLATEID { get; set; }

        /// <summary>
        //         {
        //             "NUM": "1",
        //             "CONTENT": "[param_1]"
        //         },
        //...
        //         {
        //             "NUM": "n",
        //             "CONTENT": "[param_n]"
        //         }
        /// </summary>
        public List<VnptDirectParam> PARAMS { get; set; }

        /// <summary>
        /// [schedule_time]
        /// </summary>
        public string SCHEDULETIME { get; set; }

        /// <summary>
        /// [mobile_list]
        /// </summary>
        public string MOBILELIST { get; set; }

        /// <summary>
        /// [is_telco_sub]
        /// </summary>
        public string ISTELCOSUB { get; set; }

        /// <summary>
        /// [agent_id]
        /// </summary>
        public string AGENTID { get; set; }

        /// <summary>
        /// [api_user]
        /// </summary>
        public string APIUSER { get; set; }

        /// <summary>
        /// [api_pass]
        /// </summary>
        public string APIPASS { get; set; }

        /// <summary>
        /// [user_name]
        /// </summary>
        public string USERNAME { get; set; }

        /// <summary>
        /// [data_coding]
        /// </summary>
        public string DATACODING { get; set; }

    }

    public class VnptDirectResponse : VnptDirectSms
    {
        public string ERROR { get; set; }

        public string ERROR_DESC { get; set; }
    }

    public class VnptDirectParam
    {
        public int NUM { get; set; }

        public string CONTENT { get; set; }
    }
}
