using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using DaiPhatDat.Core.Kernel.Firebase.Models;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DaiPhatDat.Core.Kernel.Firebase.Application
{
    public class FirebaseServices : IFirebaseService
    {
        private static string _serviceAccountKeyFile = AppDomain.CurrentDomain.BaseDirectory + "\\sureportal-office-firebase-key.json";
        private readonly IUserDeviceServices _userDeviceServices;
        private readonly ILoggerServices _loggerServices;
        private static FirebaseApp _firebaseDefaultApp;


        public FirebaseServices(IUserDeviceServices userDeviceServices,
            ILoggerServices loggerServices)
        {
            _userDeviceServices = userDeviceServices;
            _loggerServices = loggerServices;
            CreateFirebaseApp();
        }

        private static void CreateFirebaseApp()
        {
            if (_firebaseDefaultApp == null)
            {
                _firebaseDefaultApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(_serviceAccountKeyFile),
                });
            }
        }

        public async Task<SendMessageResponse> SendMessageAsync(SendMessageData messageData,
            List<Guid> userIds)
        {
            if (messageData == null)
            {
                return SendMessageResponse.CreateFailedResponse("Message empty");
            }

            var devices = await _userDeviceServices.GetUserDeviceInfosAsync(userIds);
            if (devices == null)
            {
                return SendMessageResponse.CreateFailedResponse("No devices");
            }
            var iOSTokens = devices
                 .Where(device => device.OSPlatform.Contains("iOS"))
                 .Select(device => device.FireBaseToken)
                 .Distinct()
                 .ToList();
            var otherTokens = devices
                    .Where(device => !device.OSPlatform.Contains("iOS"))
                    .Select(device => device.FireBaseToken)
                    .Distinct()
                    .ToList();
            await Task.WhenAll(
                SendMulticastAsync(messageData, iOSTokens),
                SendAndroidMulticastAsync(messageData, otherTokens));
            return SendMessageResponse.CreateSuccessResponse("Done!");

        }
        private async Task SendMulticastAsync(SendMessageData messageData, IReadOnlyList<string> tokens)
        {
            try
            {
                if (tokens == null || !tokens.Any())
                {
                    return;
                }
                var message = CreateMessage(messageData, tokens);
                FirebaseMessaging.DefaultInstance.SendMulticastAsync(message, CancellationToken.None);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
        }
        private async Task SendAndroidMulticastAsync(SendMessageData messageData, IReadOnlyList<string> tokens)
        {
            try
            {
                if (tokens == null || !tokens.Any())
                {
                    return;
                }
                var message = CreateAndroidMessage(messageData, tokens);
                FirebaseMessaging.DefaultInstance.SendMulticastAsync(message, CancellationToken.None);
            }
            catch (Exception ex)
            {
                _loggerServices.WriteError(ex.ToString());
            }
        }

        private MulticastMessage CreateMessage(SendMessageData messageData, IReadOnlyList<string> tokens)
        {
            var message = new MulticastMessage
            {
                Tokens = tokens,
                Notification = new Notification
                {
                    Title = messageData.Title,
                    Body = messageData.Body,
                },
                Data = messageData.CustomData,
            };
            return message;
        }
        private MulticastMessage CreateAndroidMessage(SendMessageData messageData, IReadOnlyList<string> tokens)
        {
            messageData.CustomData.Add("Title", messageData.Title);
            messageData.CustomData.Add("Body", messageData.Body);
            var message = new MulticastMessage
            {
                Tokens = tokens,
                Android = new AndroidConfig()
                {
                    TimeToLive = TimeSpan.FromHours(1),
                    Priority = Priority.Normal,
                    Notification = new AndroidNotification()
                    {
                        Title = messageData.Title,
                        Body = messageData.Body,
                        // Icon = "stock_ticker_update",
                        //   Color = "#f45342",
                    },
                },
                Data = messageData.CustomData,
            };
            return message;
        }
    }
}
