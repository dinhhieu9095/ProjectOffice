using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using DaiPhatDat.Core.Kernel.Application.Utilities;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace DaiPhatDat.Core.Kernel.Controllers
{
    [System.Web.Http.Authorize]
    public class ApiCoreController : ApiController
    {
        protected readonly ILoggerServices _loggerServices;
        protected readonly IUserServices _userService;
        protected readonly IUserDepartmentServices _userDepartmentServices;
        public ApiCoreController()
        {
        }
        public ApiCoreController(ILoggerServices loggerServices, IUserServices userService,
            IUserDepartmentServices userDepartmentServices)
        {
            _loggerServices = loggerServices;
            _userService = userService;
            _userDepartmentServices = userDepartmentServices;
        }
        private UserDto _currentUser = null;
        public UserDto CurrentUser
        {
            get
            {
                if (_currentUser != null)
                {
                    return _currentUser;
                }

                var currentUserName = WebUtils.GetUserNameFromContext(User.Identity.Name);
                _currentUser = _userService.GetByUserName(currentUserName);
                if (_currentUser == null)
                {
                    throw new System.Exception("Unauthorized");
                }

                if (_currentUser != null)
                {
                    // get jobtitles
                    var allDepts = _userDepartmentServices.GetCachedUserDepartmentsByUser(_currentUser.Id);
                    if (allDepts != null)
                    {
                        //_currentUser.JobTitles = allDepts.Where(w => !string.IsNullOrEmpty(w.JobTitleName))
                        //    .Select(s => s.JobTitleName).ToList();

                        //_currentUser.DepartmentNames = allDepts.Select(s => s.DeptName).ToList();

                        _currentUser.Departments = allDepts.Select(s => new DepartmentCompact
                        {
                            Id = s.DeptID,
                            Name = s.DeptName,
                            JobTitle = s.JobTitleName,
                            OrderNumber = s.OrderNumber
                        }).OrderBy(x => x.OrderNumber).ToList();
                    }
                }
                return _currentUser;
            }
        }
        protected object CamelCaseJson(object data)
        {
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            return JsonConvert.SerializeObject(data, jsonSerializerSettings);
        }
    }
    public class FileResult : IHttpActionResult
    {
        private readonly byte[] _content;
        private readonly string _fileName;
        public FileResult(string filePath)
        {
            _content = System.IO.File.ReadAllBytes(filePath);
            _fileName = Path.GetFileName(filePath);
        }
        public FileResult(byte[] content, string fileName)
        {
            _content = content;
            _fileName = fileName;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(_content)
                };
                response.Content.Headers.ContentLength = _content.LongLength;
                string inlineFileExtensions = ".pdf, .gif, .jpg, .jpeg, .jfif, .pjpeg, .pjp, .png, ";
                string extension = Path.GetExtension(_fileName).ToLower();
                if (inlineFileExtensions.Contains(extension))
                {
                    response.Content.Headers.Add("Content-Disposition", $"inline; filename=\"{_fileName}\"");
                }
                else
                {
                    response.Content.Headers.Add("Content-Disposition", $"attachment; filename=\"{_fileName}\"");
                }
                response.Content.Headers.ContentType =
                    new MediaTypeHeaderValue(WebUtils.GetContentType(extension));

                return response;
            }, cancellationToken);
        }
    }
    public class ImageResult : IHttpActionResult
    {
        private byte[] _content;
        private string _filePath;
        public ImageResult(string filePath)
        {
            _filePath = filePath;
        }
        public ImageResult(byte[] content)
        {
            _content = content;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(_filePath))
                {
                    _content = File.ReadAllBytes(_filePath);
                }
                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(_content)
                };
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                return response;
            }, cancellationToken);
        }
    }
}