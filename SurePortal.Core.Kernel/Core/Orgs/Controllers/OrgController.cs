using SurePortal.Core.Kernel.Controllers;
using SurePortal.Core.Kernel.Logger.Application;
using SurePortal.Core.Kernel.Models;
using SurePortal.Core.Kernel.Models.Responses;
using SurePortal.Core.Kernel.Orgs.Application;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.Core.Kernel.Orgs.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AppSettings = SurePortal.Core.Kernel.Application.AppSettings;

namespace SurePortal.Core.Kernel.Orgs.Controllers
{
    [RoutePrefix("_apis/org")]
    public class OrgController : ApiCoreController
    {
        private readonly IOrgService _orgService;
        private readonly IDepartmentServices _departmentServices;

        public OrgController(ILoggerServices loggerServices,
            IUserServices userService,
            IUserDepartmentServices userDepartmentServices,
            IDepartmentServices departmentServices,
            IOrgService orgService) :
            base(loggerServices, userService, userDepartmentServices)
        {
            _orgService = orgService;
            _departmentServices = departmentServices;
        }

        #region Users
        [Route("get-current-user")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            return Ok(await _orgService.GetUserInfoAsync(CurrentUser.Id, true).ConfigureAwait(false));
        }
        [HttpGet]
        [Route("get-users")]
        public async Task<IHttpActionResult> GetUsers()
        {
            return Ok(await _orgService.GetAllUserInfoAsync().ConfigureAwait(false));
        }
        [HttpGet]
        [Route("search-users")]
        public async Task<IHttpActionResult> SearchUsers(int page,
            int pageSize, string key = "")
        {
            var output = await _orgService.SearchUsersAsync(new UserFilterInput()
            {
                Page = page,
                PageSize = pageSize,
                Key = key
            }).ConfigureAwait(false);
            return Ok(output);
        }
        #endregion

        #region Departments
        [Route("get-departments")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDepartments()
        {
            var output = await _orgService.GetActiveDepartmentsAsync().ConfigureAwait(false);
            return Ok(output);
        }
        [Route("get-org-tree")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrgTree()
        {
            var output = await _orgService.GetOrgTreeAsync().ConfigureAwait(false);
            return Ok(output);
        }
        [Route("get-edit-org-tree")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEditOrgTree()
        {
            var output = await _orgService.GetOrgTreeNoCachedAsync().ConfigureAwait(false);
            return Ok(output);
        }
        [Route("get-catalog-departments")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCatalogDepartments()
        {
            var results = new List<SelectItemResponse>();
            var models = await _departmentServices.GetDislayNameHierarchyDepartmentsAsync(prefix: '-');
            if (models != null)
            {
                results = models.Where(x => x.IsActive == true).Select(s => new SelectItemResponse()
                {
                    Id = s.Id,
                    Name = s.HierarchyName
                }).ToList();
            }
            return Ok(results);
        }
        #endregion

        #region Department Types
        [Route("get-department-types")]
        [HttpGet]
        public async Task<IHttpActionResult> GetDepartmentTypes()
        {
            var output = await _orgService.GetDepartmentTypesAsync();
            return Ok(output);
        }
        #endregion

        #region CRUD Departments
        [Route("create-department")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateDepartment(CreateDepartmentDto item)
        {
            //TODO: validate
            item.CreatedBy = CurrentUser.Id;
            var result = await _departmentServices.CreateAsync(item);
            return Ok(result);
        }

        [Route("update-department")]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateDepartment(UpdateDepartmentDto item)
        {
            //TODO: validate
            item.ModifiedBy = CurrentUser.Id;
            var result = await _departmentServices.UpdateAsync(item);
            return Ok(result);
        }

        [Route("delete-department")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteDepartment(DeleteDepartmentDto input)
        {
            //TODO: validate
            var result = await _departmentServices.DeleteAsync(input.Id);
            return Ok(result);
        }

        [Route("get-update-department")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUpdateDepartment(Guid id)
        {
            var output = await _departmentServices.GetUpdateDepartmentAsync(id);
            return Ok(output);
        }

        #endregion

        #region CRUD Users / UserDepartments

        [Route("get-user-department")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserDepartment(Guid userId, Guid deptId)
        {
            var userDto = _userService.GetById(userId);
            var userDept = _userDepartmentServices.GetUserDepartmentsByUser(userId)
                .FirstOrDefault(f => f.DeptID == deptId);
            var result = new CreateUserDto()
            {
                Address = userDto.Address,
                BirthDate = userDto.BirthDate,
                Email = userDto.Email,
                Ext = userDto.Ext,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                HomePhone = userDto.HomePhone,
                IsManager = userDept.IsManager,
                JobTitleId = userDept.JobTitleID ?? Guid.Empty,
                JobDescription = userDept.JobDescription,
                Language = userDto.LanguageCulture,
                Mobile = userDto.Mobile,
                OrderNumber = userDept.OrderNumber ?? 10,
                UserCode = userDto.UserCode,
                UserIndex = userDto.UserIndex,
                UserName = userDto.UserName
            };

            return Ok(result);
        }


        [Route("check-user")]
        [HttpGet]
        public async Task<IHttpActionResult> CheckUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return Ok();
            }
            if (!userName.Contains("\\"))
            {
                userName = AppSettings.DomainName + "\\" + userName;
            }
            var user = _userService.GetByUserName(userName.ToLower());
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return Ok(new UserDto());
            }
        }

        //[Route("create-user")]
        //[HttpPost]
        //public async Task<IHttpActionResult> CreateUser(CreateUserDto item)
        //{
        //    if (!await ValidateUserSignature(item.UserSignatures))
        //    {
        //        return BadRequest("User signatures not valid");
        //    }

        //    var result = await _orgService.CreateUserAsync(item);
        //    return Ok(result);
        //}

        //[Route("update-user")]
        //[HttpPost]
        //public async Task<IHttpActionResult> UpdateUser(UpdateUserDto item)
        //{
        //    if (!await ValidateUserSignature(item.AddedUserSignatures))
        //    {
        //        return BadRequest("User signatures not valid");
        //    }

        //    var result = await _orgService.UpdateUserAsync(item);
        //    return Ok(result);
        //}

        //private async Task<bool> ValidateUserSignature(List<CreateUserSignatureDto> userSignatureDtos)
        //{
        //    if (userSignatureDtos != null && userSignatureDtos.Count > 0)
        //    {
        //        var signServers = await _signatureServices.GetSignatureServers();
        //        foreach (var signatureDto in userSignatureDtos)
        //        {
        //            try
        //            {
        //                var signServer = signServers.FirstOrDefault(f => f.Id == signatureDto.SignServerId);
        //                var validateResult = await _signingServices.ValidateSignatureAsync(new ValidateSignatureParam()
        //                {
        //                    CertificatePfx = signatureDto.CertificateBin,
        //                    ClientId = signServer.ClientId,
        //                    ClientSecret = signServer.ClientSecret,
        //                    User = signatureDto.Account,
        //                    Password = signatureDto.Password,
        //                    IssuerBrand = string.IsNullOrEmpty(signServer.ClientId) ?
        //                    DigitalSignatureIssuer.LOCAL : DigitalSignatureIssuer.VNPT
        //                });

        //                if (validateResult == null || validateResult.Count == 0)
        //                {
        //                    return false;
        //                }

        //            }
        //            catch (Exception ex)
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        [Route("delete-user-department")]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteUser(DeleteUserDepartmentInput input)
        {
            var result = await _orgService.DeleteUserDepartmentAsync(input.UserId, input.DeptId);
            return Ok(result);
        }

        [Route("reset-password-user")]
        [HttpPost]
        public async Task<IHttpActionResult> ResetPasswordUser(Guid id)
        {
            return Ok();
        }

        [Route("get-user-departments")]
        [HttpPost]
        public async Task<IHttpActionResult> GetUserDepartments(GetUserDepartment input)
        {
            if (input.Pagination.Perpage == 0)
            {
                input.Pagination.Perpage = 10;
            }
            var models = await _userDepartmentServices.GetUserDepartmentDtos(input.DeptId,
               input.Pagination.Page,
               input.Pagination.Perpage, input.Keyword);
            var totalUser = 0;
            if (models.Count > 0)
            {
                totalUser = models.First().TotalRows;
            }
            var response = new KtDataTableResponse(models, new KtPaging()
            {
                Field = "fullName",
                Page = input.Pagination.Page,
                Pages = (totalUser / input.Pagination.Perpage),
                Perpage = input.Pagination.Perpage,
                Sort = "asc",
                Total = totalUser
            });
            return Ok(response);
        }

        [Route("get-catalog-jobtitles")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCatalogJobTitles()
        {
            var results = new List<SelectItemResponse>();
            var models = await _orgService.GetJobtitlesAsync();
            if (models != null)
            {
                results = models.Where(x => x.IsActive)
                    .OrderBy(o => o.Name)
                    .Select(s => new SelectItemResponse()
                    {
                        Id = s.Id,
                        Name = s.Name + (string.IsNullOrEmpty(s.Code) ? "" : " - " + s.Code)
                    }).ToList();
            }
            return Ok(results);
        }


        //[Route("get-catalog-signservers")]
        //[HttpGet]
        //public async Task<IHttpActionResult> GetCatalogSignServers()
        //{
        //    var results = new List<SelectItemResponse>();
        //    var models = await _signatureServices.GetSignatureServers();
        //    if (models != null)
        //    {
        //        results = models.OrderBy(o => o.Title)
        //            .Select(s => new SelectItemResponse()
        //            {
        //                Id = s.Id,
        //                Name = s.Title,
        //                Type = s.Type.ToString()
        //            }).ToList();


        //    }
        //    return Ok(results);
        //}
        #endregion

        [Route("upload-file")]
        [HttpPost]
        public async Task<string> Upload()
        {
            try
            {
                var fileuploadPath = AppSettings.FileFolderPath;

                var provider = new MultipartFormDataStreamProvider(fileuploadPath);
                var content = new StreamContent(HttpContext.Current.Request.GetBufferlessInputStream(true));
                foreach (var header in Request.Content.Headers)
                {
                    content.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                await content.ReadAsMultipartAsync(provider);

                //Code for renaming the random file to Original file name  
                string uploadingFileName = provider.FileData.Select(x => x.Headers.ContentDisposition.FileName).FirstOrDefault();
                uploadingFileName = Path.Combine(fileuploadPath, uploadingFileName.Replace("\"", ""));
                string originalFileName = Path.Combine(fileuploadPath, Guid.NewGuid() + Path.GetExtension(uploadingFileName));

                if (File.Exists(originalFileName))
                {
                    File.Delete(originalFileName);
                }

                File.Move(provider.FileData.FirstOrDefault().LocalFileName, originalFileName);
                //Code renaming ends...  

                return Path.GetFileName(originalFileName);
            }
            catch (Exception)
            {
                return "";
            }

        }

        [Route("sync-active-directory-info")]
        public async Task<string> SyncActiveDirectoryInfo()
        {
            try
            {
                var sb = new StringBuilder();
                var adConnectionString = ConfigurationManager.AppSettings["SyncADConnection"];
                var adUserAccess = ConfigurationManager.AppSettings["SyncADUserAccess"];
                var adPassword = ConfigurationManager.AppSettings["SyncADPassword"];
                var adDomainName = ConfigurationManager.AppSettings["DomainName"];
                using (var de = new DirectoryEntry(adConnectionString, adUserAccess, adPassword, AuthenticationTypes.Secure))
                {
                    using (var deSearch = new DirectorySearcher())
                    {
                        deSearch.SearchRoot = de;
                        deSearch.PageSize = 6000;
                        deSearch.Filter = "(objectClass=user)";
                        deSearch.SearchScope = SearchScope.Subtree;
                        SearchResultCollection results = deSearch.FindAll();
                        if (results != null)
                        {
                            foreach (SearchResult result in results)
                            {
                                var userEntry = new DirectoryEntry(result.Path, adUserAccess, adPassword);
                                if (userEntry.Properties.Contains("sAMAccountName"))
                                {
                                    string username = userEntry.Properties["sAMAccountName"].Value.ToString();
                                    if (username.Contains("$"))
                                    {
                                        continue;
                                    }
                                    try
                                    {
                                        username = adDomainName + "\\" + username;
                                        var mail = GetUserProperty(userEntry, "mail");
                                        var employeeID = GetUserProperty(userEntry, "employeeID");
                                        var ext = GetUserProperty(userEntry, "telephoneNumber");
                                        var mobile = GetUserProperty(userEntry, "mobile");
                                        var avarStr = GetUserAvatarProperty(userEntry, "thumbnailPhoto");

                                        await _orgService.UpdateAdInfo(username, employeeID, mail, ext, mobile.Replace("+84", "0").Trim(),
                                            avarStr);
                                    }
                                    catch (Exception ex)
                                    {
                                        sb.AppendLine(username + " error:" + ex.Message);
                                    }
                                }
                            }
                        }
                    }
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


        private string GetUserProperty(DirectoryEntry userEntry, string property)
        {
            return userEntry.Properties[property] != null && userEntry.Properties[property].Value != null
                ? userEntry.Properties[property].Value.ToString()
                : string.Empty;
        }

        private byte[] GetUserAvatarProperty(DirectoryEntry userEntry, string property)
        {
            return userEntry.Properties[property] != null && userEntry.Properties[property].Value != null
                ? (byte[])userEntry.Properties[property].Value
                : null;
        }
        [Route("get-org-user-chart")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrgUserChart()
        {
            var output = await _orgService.GetOrgUserTree();
            return Ok(output);
        }
    }
}
