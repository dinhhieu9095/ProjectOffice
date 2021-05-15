using DaiPhatDat.Core.Kernel.Controllers;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace DaiPhatDat.Module.Task.Web
{
    [Authorize]
    public class BaseTaskController : CoreController
    {
        protected string AVARTAR_URL = System.Configuration.ConfigurationManager.AppSettings["AvartarURL"];
        protected const string DefaultImageBase64 = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAD4AAAA+CAIAAAD8oz8TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAyJpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RSZWY9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZVJlZiMiIHhtcDpDcmVhdG9yVG9vbD0iQWRvYmUgUGhvdG9zaG9wIENTNiAoV2luZG93cykiIHhtcE1NOkluc3RhbmNlSUQ9InhtcC5paWQ6QTdEMjFGNzg1MUFDMTFFN0I5QzQ4OTY4NzcwMDc5RUQiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6QTdEMjFGNzk1MUFDMTFFN0I5QzQ4OTY4NzcwMDc5RUQiPiA8eG1wTU06RGVyaXZlZEZyb20gc3RSZWY6aW5zdGFuY2VJRD0ieG1wLmlpZDpBN0QyMUY3NjUxQUMxMUU3QjlDNDg5Njg3NzAwNzlFRCIgc3RSZWY6ZG9jdW1lbnRJRD0ieG1wLmRpZDpBN0QyMUY3NzUxQUMxMUU3QjlDNDg5Njg3NzAwNzlFRCIvPiA8L3JkZjpEZXNjcmlwdGlvbj4gPC9yZGY6UkRGPiA8L3g6eG1wbWV0YT4gPD94cGFja2V0IGVuZD0iciI/PuPygQIAAATjSURBVHjazFrbVloxEIUxVSuIooD//2c+uxBdauVSK90lbUgnyWSSE9F5cFE4SXZm9lxP+7e3t72YbLfbfr/vf6iW1A7yEVkAlDrPPe3WQypA+1uljtAAiEBXAurvxKHxPzCg/geluVI2iZ7ifqVSMgjKYHcT7KA/SLAY9VqLs08UruZ6SiFBK00OEDSn3D/1GLmfnUtpmCoTsSMZlDcnhzjr0Wy7jkGzLmT5+qLq4/2FfmxhgFIMjLqEZq3jBQnBLsvIkFohIFk18mVkrRl3CbdMYAL7KZrttv+EPRlFJhOP7cBwmlAxbDv/n3K6/pMmiL7txBiDz/j+/f39507e3t7swymzpDJ/6nSj8TZBN/YnCLAOh8Ozs7Pj4+PwSVxgvV7/2Ak+swvIdk4Z36QU6W8XfsNKlNFodHFxcXR0lEwfRN93gicfHx9fX1+ZYWWTRm1i5LSX3RrcmEwmp6enyrgEm8xms+fn58VikdKFgIFzXV/Q+Y4Cu5+cnAAHqFIaWM/Pz7Hq7u4u6vdlhUCYmFJB0FkN+q7DbQXkwfJsCBZiNwn5RW4dwJNq3A79eDyG9VJsjLYN+6JX0xn4vmL/wuJ6fgsC58Y+0YooFdD2RW9RIWHjDJSNI1sVydGtNBUOlXoJdhwMBkIcrKANwo4N9nJRwC5DRbWyZTnyTtvWBLrQ21wLPbw62IKY2LaxAt0ZLA0eErrjaIRCTOw42wgFe1YwkIR4Ev2mY0BM9shEpe2fqq1OFQhtobtqOXpQMiVVNEQNxc/lmtr9b5fkB9FUhVk3SCkSm1OVU4l95Sg31CzcomlojvvXToSCMUzzvBDQVPqA7h/TRDabDfaUB09hb0VypRYuwBnL5bIt9OyGUdpQ6XwV36NJa8sWNE2hF2ZDGZXO07AjlLRarVpBR8eEjlueR7DSNQk9e12sf3h4aBJq4DlPT09+PpLnqX69QKVDSjuxgNbRHXcP5/f391Gn16iPNMMtP+rbewM9oMPWXaADN1TgqzyM6CE81+OT4I6pWY9bjLPr0APQfD7HWkZizRhiX011KTysxdfr9eXlpb4sw/OLxQL6ludq2omATHTBMnjy5eUF+lN6LRI+ngf6imKOHWGUHUY4i7KMR2eJFhsFt1ZVRNfX16PRyF7Y7+uyI0QW+40wyJVXos0bj8doK+t6C6wdDofwdZvg9BNqB8bIyg4vAz2ho5lMJlB29+ZoOp2irQb1bQ1ThN4I09DwMsANNQN3w/YUuseeCDhwgHD8KxCB9GNi4Eb/e3Nz07ytBnRsC/X7VXs28lCWZ92HoxoBCbE5Q58PjtmhgOUJtm44OYoGH1AfCrLoVW21TBUbBLsPR5W6B3r81bxrITbg899guZctV1dXzfkthB0Efk164lwP38UNBoPucbBI7ImgjdDm7xs81gg6O8B2yB29g4umKKJo/Hb1PvK8Psm3JT3Qy3N3kml3YKqwVAUHy6ekKKVQJIWjwIOJfacphMhk0QuqKcfeH+qvyCf5/w/DoKMw/NAEpFQ8YCShhw2opdenq9wpPkVactWZX+7ATAfLQdnKLMqZfUpi3wL3R8zR6wQ1WTRKRgYJAN3knWgrsW+aGKv/G5f6/+/hi7DFpRdXkPmsjkQYhMVPjy0sswJSSHfD/NeW5l+H6M5ZN5sNg/pbgAEAqY268+gGhSIAAAAASUVORK5CYII=";
        public BaseTaskController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices) : base(loggerServices, userService, userDepartmentServices)
        {

        }

        private static string GetIpAddress(string userHostAddress)
        {
            var iPAddresses = Dns.GetHostAddresses(userHostAddress);

            foreach (var iPAddress in iPAddresses)
                if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                    return iPAddress.ToString();

            foreach (var iPAddress in Dns.GetHostAddresses(Dns.GetHostName()))
                if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                    return iPAddress.ToString();

            return IPAddress.None.ToString();
        }
    }
}