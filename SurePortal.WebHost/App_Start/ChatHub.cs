//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Hubs;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Web;

//namespace SurePortal.WebHost
//{
//    [HubName("chat")]
//    public class ChatHub : Hub
//    {
//        static List<UserHub> UserHubs = new List<UserHub>();
//        public override Task OnConnected()
//        {
//            if (UserHubs.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId) == null)
//            {
//                UserHubs.Add(new UserHub { ConnectionId = Context.ConnectionId });
//            }
//            return base.OnConnected();
//        }
//        public override Task OnDisconnected(bool stopCalled)
//        {
//            var currentConnection = UserHubs.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
//            if (currentConnection != null)
//                UserHubs.Remove(currentConnection);
//            var userDistinct = UserHubs.Select(x => new { x.UserName, x.FullName }).Distinct().ToList();
//            Clients.All.listConnected(ObjectToJson(userDistinct));

//            return base.OnDisconnected(stopCalled);
//        }
//        public void AfterConnected(string user, string fullName = "")
//        {
//            var currentConnection = UserHubs.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
//            if (currentConnection != null)
//            {
//                currentConnection.FullName = fullName;
//                currentConnection.UserName = user;
//            }
//            else
//            {
//                UserHubs.Add(new UserHub
//                {
//                    ConnectionId = Context.ConnectionId,
//                    FullName = fullName,
//                    UserName = user
//                });
//            }
//            var userDistinct = UserHubs.Select(x => new { x.UserName, x.FullName }).Distinct().ToList();
//            Clients.All.listConnected(ObjectToJson(userDistinct));
//        }
//        public void JoinRooms(List<string> rooms)
//        {
//            foreach (var room in rooms)
//            {
//                try
//                {
//                    Groups.Add(Context.ConnectionId, room);
//                }
//                catch (Exception)
//                {
//                }
//            }
//        }
//        public void JoinRoom(string roomName, string connectionId)
//        {

//            try
//            {
//                Groups.Add(connectionId, roomName);
//            }
//            catch (Exception)
//            {
//            }
//        }
//        public void LeaveRoom(string connectionId, string roomName)
//        {
//            try
//            {
//                Groups.Remove(connectionId, roomName);
//            }
//            catch (Exception)
//            {
//            }
//        }
//        public Task AddChat(string roomName, object data)
//        {
//            return Clients.Group(roomName).addChat(roomName, data);
//        }
//        public Task EditedChat(string roomName, object data)
//        {
//            return Clients.Group(roomName).editedChat(roomName, data);
//        }
//        public Task DeletedChat(string roomName, string chatId)
//        {
//            return Clients.Group(roomName).deletedChat(roomName, chatId);
//        }
//        public async Task AddRoom(string roomName, object data, List<string> members)
//        {
//            JoinRoom(roomName, Context.ConnectionId);
//            if (members.Any())
//            {
//                var menberOnline = UserHubs.Where(x => members.Any(y => y == x.UserName)).ToList();
//                if (menberOnline != null && menberOnline.Any())
//                {
//                    foreach (var menber in menberOnline)
//                    {
//                        JoinRoom(roomName, menber.ConnectionId);
//                    }
//                }
//            }
//            System.Threading.Thread.Sleep(100);
//            await Clients.Group(roomName).addRoom(roomName, data);
//        }
//        public Task EditedRoom(string roomName, object data)
//        {
//            return Clients.Group(roomName).editedRoom(roomName, data);
//        }
//        public Task DeletedRoom(string roomName)
//        {
//            return Clients.Group(roomName).deletedRoom(roomName, roomName);
//        }
//        public void ChangUserInRoom(string roomName, List<string> userNews, List<string> userRemoves, object data)
//        {
//            if (userNews != null && userNews.Any())
//            {
//                foreach (var user in userNews)
//                {
//                    var userHubs = UserHubs.Where(x => x.UserName == user).ToList();
//                    if (userHubs != null)
//                    {
//                        foreach (var userHub in userHubs)
//                        {
//                            JoinRoom(userHub.ConnectionId, roomName);
//                            Clients.User(userHub.ConnectionId).addRoom(roomName, data);
//                        }
//                    }
//                }
//            }
//            if (userRemoves != null && userRemoves.Any())
//            {
//                foreach (var user in userRemoves)
//                {
//                    var userHubs = UserHubs.Where(x => x.UserName == user).ToList();
//                    if (userHubs != null)
//                    {
//                        foreach (var userHub in userHubs)
//                        {
//                            LeaveRoom(userHub.ConnectionId, roomName);
//                            Clients.User(userHub.ConnectionId).deletedRoom(roomName);
//                        }
//                    }
//                }
//            }
//        }
//        public Task Interactive(string roomName, object interactive)
//        {
//            return Clients.Group(roomName).interactive(roomName, interactive);
//        }
//        public Task SendNotification(string userId, Guid notificationID, Guid senderID, string senderFullName, string action, string summary)
//        {
//            return Clients.User(userId).addNotification(notificationID, senderID, senderFullName, action, summary, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
//        }
//        private string ObjectToJson(object input)
//        {
//            return JsonConvert.SerializeObject(input);
//        }
//    }
//    public class UserHub
//    {
//        public string ConnectionId { get; set; }
//        public string UserName { get; set; }
//        public string FullName { get; set; }
//        //public bool IsAdmin { get; set; }
//    }
//}