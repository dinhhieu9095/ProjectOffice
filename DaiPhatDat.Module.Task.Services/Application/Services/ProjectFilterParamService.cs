using AutoMapper;
using DaiPhatDat.Core.Kernel.AmbientScope;
using DaiPhatDat.Core.Kernel.Logger.Application;
using DaiPhatDat.Core.Kernel.Orgs.Models;
using DaiPhatDat.Module.Task.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DaiPhatDat.Core.Kernel.Orgs.Application;
using System.Globalization;
using SystemTask = System.Threading.Tasks.Task;
using DaiPhatDat.Core.Kernel.Orgs.Domain.Entities;
using DaiPhatDat.Core.Kernel.Orgs.Application.Dto;

namespace DaiPhatDat.Module.Task.Services
{
    public class ProjectFilterParamService : IProjectFilterParamService
    {
        private readonly ILoggerServices _loggerServices;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IProjectFilterParamRepository _objectRepository;
        private readonly IProjectCategoryRepository _projectCategoryRepository;
        private readonly IProjectFolderService _projectFolderService;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        public ProjectFilterParamService(ILoggerServices loggerServices, IDbContextScopeFactory dbContextScopeFactory, IMapper mapper, IProjectFilterParamRepository objectRepository, IProjectFolderService projectFolderService, IUserServices userServices, IProjectCategoryRepository projectCategoryRepository)
        {
            _loggerServices = loggerServices;
            _dbContextScopeFactory = dbContextScopeFactory;
            _objectRepository = objectRepository;
            _projectFolderService = projectFolderService;
            _mapper = mapper;
            _userServices = userServices;
            _projectCategoryRepository = projectCategoryRepository;
        }
        private JsTreeViewModel ConvertToJsTreeObject(ProjectFilterParam treeDoc, bool isLable = false, bool isEdit = false)
        {
            JsTreeViewModel jsTree = new JsTreeViewModel();
            jsTree.text = treeDoc.Name;
            jsTree.id = treeDoc.Id.ToString();
            jsTree.left_iscount = treeDoc.IsCount.HasValue ? treeDoc.IsCount.Value : false;
            jsTree.left_paramnames = "";
            jsTree.left_paramvalues = treeDoc.ParamValue;
            jsTree.icon = "far fa-folder";
            jsTree.parent = treeDoc.ParentID.Value == Guid.Empty ? "#" : treeDoc.ParentID.Value.ToString();
            jsTree.state = new JsTreeStateObject() { opened = true };
            var m_datacontext =
                   "{ \"ParamValues\" : \"" + treeDoc.ParamValue + "\","
                   + " \"ParentDocument\" : \"" + treeDoc.ParentID + "\" }";
            jsTree.li_attr = new
            {
                datacontext = m_datacontext,
                islable = isLable,
                isedit = isEdit
            };
            return jsTree;
        }
        private string ToNoSign(string value)
        {
            var stringNoSign = value.Normalize(NormalizationForm.FormD);
            var chars = stringNoSign.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            stringNoSign = new string(chars).Normalize(NormalizationForm.FormC).ToLower();
            stringNoSign = stringNoSign.Replace("Đ", "D");
            stringNoSign = stringNoSign.Replace("đ", "d");
            return stringNoSign;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="userRoles"></param>
        /// <param name="treeRoles"></param>
        /// <param name="listUserTree"></param>
        /// <param name="userDelegate"></param>
        private void GetChildTreeFilterByTask(Guid currentUserId, Guid parentID, List<JsTreeViewModel> listUserTree,
            string userParams = "", Guid postUserId = default(Guid), string keySearch = null, bool isMobile = false)
        {
            var allFilter = _objectRepository.Get(w => w.IsActive == true);
            var lstParamsCount = new List<string>();
            // task & document
            // level 1
            IReadOnlyList<ProjectFilterParam> childLv1 = allFilter.Where(w => w.ParentID == parentID && ((w.IsPrivate == true && w.CreatedBy == currentUserId) || w.IsPrivate == false)).OrderBy(e => e.NoOrder).ThenBy(e => e.Name).ToList();

            //AsyncHelper.RunSync(() => _projectFilterParamRepository.GetChild(parentID));

            var isAdmin = false;
            var currentUser = _userServices.GetById(currentUserId);
            var permissions = _userServices.GetUserPermission(currentUser.UserName);
            if (currentUser.UserName.Contains("spadmin") || permissions.Contains("FullControl"))
            {
                isAdmin = true;
            }

            if (childLv1 != null && childLv1.Count > 0)
            {
                if (!string.IsNullOrEmpty(keySearch))
                {
                    keySearch = ToNoSign(keySearch);
                }
                foreach (var itemLevel1 in childLv1)
                {
                    if (!string.IsNullOrEmpty(keySearch))
                    {
                        List<string> namesLv2 = allFilter.Where(w => w.ParentID == itemLevel1.Id && ((w.IsPrivate == true && w.CreatedBy == currentUserId) || w.IsPrivate == false)).Select(e => e.Name).ToList();
                        bool isExistLV2 = false;
                        foreach (string itemNoSignLV2 in namesLv2)
                        {
                            if (ToNoSign(itemNoSignLV2).Contains(keySearch))
                            {
                                isExistLV2 = true;
                            }
                        }
                        var nameLV1NoSign = ToNoSign(itemLevel1.Name);
                        if (!nameLV1NoSign.Contains(keySearch) && !isExistLV2)
                        {
                            continue;
                        }
                    }
                    bool isPermission = false;
                    if (itemLevel1.IsPrivate == true || itemLevel1.CreatedBy.Value == currentUserId)
                    {
                        isPermission = true;
                    }

                    if (!isPermission && itemLevel1.IsPrivate == false && !string.IsNullOrEmpty(itemLevel1.Roles))
                    {
                        var RoleStr = itemLevel1.Roles.Split(new char[] { ',' }).ToList();
                        List<Guid> Guids = new List<Guid>();
                        foreach (var item in RoleStr)
                        {
                            Guids.Add(new Guid(item));
                        }
                        var users = _userServices.GetUserByRoles(Guids);
                        if (users.Any(e => e.Id == currentUserId))
                        {
                            isPermission = true;
                        }
                    }

                    if (!isPermission && itemLevel1.IsPrivate == false && !string.IsNullOrEmpty(itemLevel1.Users))
                    {
                        if (itemLevel1.Users.Contains(currentUserId.ToString()))
                        {
                            isPermission = true;
                        }
                    }

                    if (CheckPermission(itemLevel1, currentUserId))
                    {
                        // translate
                        //itemLevel1.Name = TranslateTreeFilterResource(itemLevel1);
                        if (!string.IsNullOrEmpty(userParams))
                        {
                            itemLevel1.ParamValue =
                                 (!string.IsNullOrEmpty(itemLevel1.ParamValue) ?
                                (itemLevel1.ParamValue + ';') : string.Empty) + userParams;
                        }

                        bool isEditLV1 = false;
                        if ((isAdmin && itemLevel1.IsPrivate == false) || itemLevel1.CreatedBy == currentUserId)
                        {
                            isEditLV1 = true;
                        }
                        var jstree = ConvertToJsTreeObject(itemLevel1, itemLevel1.IsLable ?? false, isEditLV1);

                        //if (isAdmin || (itemLevel1.IsPrivate == true && itemLevel1.CreatedBy == currentUserId))
                        if (isMobile)
                        {
                            jstree.text = itemLevel1.Name;
                        }
                        else
                        {
                            if (isEditLV1)
                            {
                                jstree.text = string.Format("<span class='menu-icon'><i class='far fa-folder'></i></span><span class='text-content'>" + itemLevel1.Name + "</span>");

                            }
                            else
                            {
                                jstree.text = string.Format("<span class='menu-icon'><i class='far fa-folder'></i></span><span class='text-content'>" + itemLevel1.Name + "</span>");
                            }
                        }

                        if (postUserId != Guid.Empty)
                        {
                            jstree.id += "_" + postUserId.ToString();
                        }
                        // jstree.parent = parentID.ToString();
                        // for level 2
                        IReadOnlyList<ProjectFilterParam> childLv2 = allFilter.Where(w => w.ParentID == itemLevel1.Id && ((w.IsPrivate == true && w.CreatedBy == currentUserId) || w.IsPrivate == false)).OrderBy(e => e.NoOrder).ThenBy(o => o.CreatedDate).ToList();

                        // AsyncHelper.RunSync(() => _projectFilterParamRepository.GetChild(itemLevel1.Id));
                        if (childLv2 != null && childLv2.Count > 0)
                        {
                            var lstChild = new List<JsTreeViewModel>();
                            foreach (var itemLevel2 in childLv2)
                            {
                                if (!string.IsNullOrEmpty(keySearch))
                                {
                                    var nameLV2NoSign = ToNoSign(itemLevel2.Name);
                                    if (!nameLV2NoSign.Contains(keySearch))
                                    {
                                        continue;
                                    }
                                }

                                if (CheckPermission(itemLevel2, currentUserId))
                                {
                                    //itemLevel2.Name = TranslateTreeFilterResource(itemLevel2);

                                    if (!string.IsNullOrEmpty(userParams))
                                    {
                                        itemLevel2.ParamValue =
                                             (!string.IsNullOrEmpty(itemLevel2.ParamValue) ?
                                            (itemLevel2.ParamValue + ';') : string.Empty) + userParams;
                                    }

                                    bool isEditLV2 = false;
                                    if ((isAdmin && itemLevel1.IsPrivate == false) || itemLevel2.CreatedBy == currentUserId)
                                    {
                                        isEditLV2 = true;
                                    }

                                    var jstreeV2 = ConvertToJsTreeObject(itemLevel2, itemLevel2.IsLable ?? false, isEditLV2);

                                    //if (isAdmin || (itemLevel2.IsPrivate == true && itemLevel2.CreatedBy == currentUserId))

                                    if (isMobile)
                                    {
                                        jstreeV2.text = itemLevel2.Name;
                                    }
                                    else
                                    {
                                        if (isEditLV2)
                                        {
                                            jstreeV2.text = string.Format("<span class='menu-icon'><i class='far fa-none'></i></span><span class='text-content'>" + itemLevel2.Name + " </span>");

                                        }
                                        else
                                        {
                                            jstreeV2.text = string.Format("<span class='menu-icon'><i class='far fa-none'></i></span><span class='text-content'>" + itemLevel2.Name + "</span>");
                                        }
                                    }

                                    if (postUserId != Guid.Empty)
                                    {
                                        jstreeV2.id += "_" + postUserId.ToString();
                                    }
                                    //  jstreeV2.parent = jstree.id;
                                    lstChild.Add(jstreeV2);
                                    if (jstreeV2.left_iscount && itemLevel2.IsLable != true)
                                        lstParamsCount.Add(jstreeV2.left_paramvalues);
                                }
                            }
                            //jstree.children = lstChild;
                            listUserTree.AddRange(lstChild);
                        }
                        else
                        {
                            jstree.children = null;
                        }
                        jstree.icon = null;
                        jstree.parent = "#";
                        if (jstree.left_iscount && itemLevel1.IsLable != true)
                            lstParamsCount.Add(jstree.left_paramvalues);
                        listUserTree.Add(jstree);
                    }
                }
            }

            //Do count
            foreach (var item in listUserTree)
            {
                if (item.left_iscount && item.left_iscount)
                {
                    lstParamsCount.Add(item.left_paramvalues);
                }
                var childsLv2 = item.children as List<JsTreeViewModel>;
                if (childsLv2 != null)
                {
                    foreach (var itemSub in childsLv2)
                    {
                        if (item.left_iscount)
                        {
                            lstParamsCount.Add(item.left_paramvalues);
                        }
                    }
                }

            }

            var dataCounts = CountByListFilter(lstParamsCount, currentUser, DateTime.Now);

            foreach (var menuItem in listUserTree)
            {
                System.Reflection.PropertyInfo pi = menuItem.li_attr.GetType().GetProperty("islable");
                bool isLable = (bool)(pi.GetValue(menuItem.li_attr, null));
                if (menuItem.left_iscount && !isLable && dataCounts != null)
                {
                    var Count = dataCounts.Where(w => w.ParamName == menuItem.left_paramvalues)
                                  .Select(s => s.NoDoc).FirstOrDefault();

                    if (Count > 0 && !isMobile)
                    {
                        menuItem.text += " <span class=\"doc-left-span-no\">" + Count + "</span>";
                    }
                }
                if (menuItem.children != null)
                {
                    var childs = menuItem.children as List<JsTreeViewModel>;
                    if (childs != null && childs.Count > 0)
                    {
                        for (int idx = 0; idx < childs.Count; idx++)
                        {
                            System.Reflection.PropertyInfo piLv2 = childs[idx].li_attr.GetType().GetProperty("islable");
                            bool isLableLv2 = (bool)(piLv2.GetValue(childs[idx].li_attr, null));
                            if (childs[idx].left_iscount && !isLableLv2 && dataCounts != null)
                            {
                                var Count = dataCounts.Where(w => w.ParamName == childs[idx].left_paramvalues)
                                                  .Select(s => s.NoDoc).FirstOrDefault();

                                if (Count > 0 && !isMobile)
                                {
                                    childs[idx].text += " <span class=\"doc-left-span-no\">" + Count + "</span>";
                                }
                                var childsLv2 = childs[idx].children as List<JsTreeViewModel>;
                                if (childsLv2 != null && childsLv2.Count > 0)
                                {
                                    for (int mdx = 0; mdx < childsLv2.Count; mdx++)
                                    {
                                        if (childsLv2[mdx].left_iscount && dataCounts != null)
                                        {
                                            var Countv2 = dataCounts.Where(w => w.ParamName == childsLv2[idx].left_paramvalues)
                                            .Select(s => s.NoDoc).FirstOrDefault();
                                            if (Countv2 > 0 && !isMobile)
                                            {
                                                childsLv2[mdx].text += "<span class=\"doc-left-span-no\">" + Countv2 + "</span>";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<ProjectFilterCounter> CountByListFilter(List<string> paramValues, UserDto currentUser,
            DateTime currentDate)
        {
            if (paramValues != null)
            {
                for (int i = 0; i < paramValues.Count; i++)
                {
                    if (paramValues[i].Contains("FromDate") || paramValues[i].Contains("ToDate"))
                    {
                        //var listParam = paramValues[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        //fore
                        //paramValues[i] = paramValues[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    }
                }
            }

            using (var dbCtxScope = _dbContextScopeFactory.CreateReadOnly())
            {
                var dbContext = dbCtxScope.DbContexts.Get<TaskContext>();
                return dbContext.Database.SqlQuery<ProjectFilterCounter>(
                 string.Format(
                     @" EXEC dbo.SP_Count_Select_Projects_MultiFilters @CurrentDate = '{0}', @CurrentUserID = '{1}', @ListParamsName = '{2}', @IsFullControl= '{3}'",
                     currentDate.ToString("yyyy-MM-dd HH:mm"), currentUser.Id, string.Join("|", paramValues), currentUser.AccountName.Contains("spadmin") ? "1": "0")).ToList();
            }

        }

        public bool CheckPermission(ProjectFilterParam projectFilterParam, Guid currentUserId)
        {
            bool isPermission = false;
            if (projectFilterParam.IsPrivate == true || projectFilterParam.CreatedBy.Value == currentUserId)
            {
                isPermission = true;
            }

            if (!isPermission && projectFilterParam.IsPrivate == false && !string.IsNullOrEmpty(projectFilterParam.Roles))
            {
                var RoleStr = projectFilterParam.Roles.Split(new char[] { ',' }).ToList();
                List<Guid> Guids = new List<Guid>();
                foreach (var item in RoleStr)
                {
                    Guids.Add(new Guid(item));
                }
                var users = _userServices.GetUserByRoles(Guids);
                if (users.Any(e => e.Id == currentUserId))
                {
                    isPermission = true;
                }
            }

            if (!isPermission && projectFilterParam.IsPrivate == false && !string.IsNullOrEmpty(projectFilterParam.Users))
            {
                if (projectFilterParam.Users.Contains(currentUserId.ToString()))
                {
                    isPermission = true;
                }
            }

            if (!isPermission && projectFilterParam.IsPrivate == false && string.IsNullOrEmpty(projectFilterParam.Users) && string.IsNullOrEmpty(projectFilterParam.Roles))
            {
                isPermission = true;
            }
            return isPermission;
        }

        public async Task<List<JsTreeViewModel>> GetTreeFilterByParentId(Guid userID, Guid parentID = default(Guid), string keySearch = null, bool isMobile = false)
        {
            // Get Role of user
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                List<JsTreeViewModel> listUserTree = new List<JsTreeViewModel>();
                var parentObj = _objectRepository
                .Get(x => x.Id == parentID && x.IsActive == true).FirstOrDefault();
                if (parentID == Guid.Empty)
                {
                    var projectFilterRoot = _objectRepository
                    .Get(x => x.ParentID == parentID && x.IsActive == true);
                    listUserTree = projectFilterRoot.Select(x => ConvertToJsTreeObject(x)).ToList();
                    return listUserTree;
                }
                if (parentObj != null)
                {
                    switch (parentObj.Code)
                    {
                        //case "HSCT":
                        //    GetTreeFilterByCompanyFolder(parentID, listUserTree);
                        //    break;
                        //case "HSCN":
                        //    {
                        //        await GetTreeFilterByPersonalFolder(userID, parentID, listUserTree);
                        //    }
                        //    break;
                        //case "UQ":
                        //    {
                        //        GetTreeFilterByDelegation(userID, listUserTree);
                        //    }
                        //    break;
                        //case "BG":
                        //    {
                        //        GetTreeFilterByHandler(userID, listUserTree);
                        //    }
                        //    break;
                        default:
                            {
                                GetChildTreeFilterByTask(userID, parentID, listUserTree, keySearch: keySearch, isMobile: isMobile);
                            }
                            break;
                    }
                }

                return listUserTree;
            }
        }

        /// <summary>
        /// Lấy cây lọc menu theo hồ sơ cá nhân
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="userID"></param>
        /// <param name="listUserTree"></param>
        private async SystemTask GetTreeFilterByPersonalFolder(Guid userID, Guid parentID, List<JsTreeViewModel> listUserTree)
        {
            var userFolders = await _projectFolderService.GetByUser(userID);
            if (userFolders.Any())
            {
                foreach (var folder in userFolders)
                {
                    var jsChild = new JsTreeViewModel();
                    var parentFolderId = folder.ParentId == null || folder.ParentId.Value == Guid.Empty ? "#" : folder.ParentId.Value.ToString();
                    jsChild.parent = parentFolderId.ToString();
                    jsChild.id = folder.Id.ToString();
                    jsChild.text = folder.Name;
                    jsChild.state = new JsTreeStateObject();
                    jsChild.left_paramvalues = "@PrivateFolder:" + folder.Id;
                    var m_datacontext =
                        "{ \"ParamValues\" : \"@PrivateFolder:" + folder.Id + "\","
                        + " \"ParentID\" : \"" + parentFolderId + "\" }";
                    jsChild.li_attr = new
                    {
                        datacontext = m_datacontext
                    };

                    jsChild.children = null;
                    listUserTree.Add(jsChild);
                }
            }
        }

        /// có filter theo module code
        /// </summary>
        /// <param name="userID">mã tài khoản cần user xét</param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public List<ProjectFilterParam> GetRootCheckSubProjectFilterParams(Guid userID)
        {
            var models = new List<ProjectFilterParam>();
            List<ProjectFilterParam> listUserTree = null;
            using (var scope = _dbContextScopeFactory.Create())
            {
                var dbcontext = scope.DbContexts.Get<TaskContext>();
                listUserTree = _objectRepository.GetAll().Where(
                        tfd =>
                        tfd.IsActive == true &&
                        tfd.ParentID.HasValue &&
                        tfd.Code != null && (
                            tfd.Code.Contains("TASK") //|| tfd.Code.Contains("UQ") || tfd.Code.Contains("BG")
                                                      //|| tfd.Code == "HSCN" || tfd.Code == "HSCT"
                            )
                        ).ToList();
                if (!listUserTree.Any())
                {
                    ProjectFilterParam filter = new ProjectFilterParam()
                    {
                        Code = "TASK",
                        CreatedBy = null,
                        CreatedDate = DateTime.Now,
                        IsActive =true,
                        IsLable = true,
                        Id = Guid.NewGuid(),
                        Name = "Bộ lọc",
                        NoOrder = 0,
                    };
                    ProjectFilterParam filterChild = new ProjectFilterParam()
                    {
                        Code = "TASK",
                        CreatedBy = null,
                        CreatedDate = DateTime.Now,
                        IsActive = true,
                        IsLable = false,
                        Id = Guid.NewGuid(),
                        ParentID = filter.Id,
                        Name = "Tất cả",
                        NoOrder = 0,
                    };
                    _objectRepository.Add(filter);
                    _objectRepository.Add(filterChild);
                    scope.SaveChanges();
                    listUserTree = _objectRepository.GetAll().Where(
                        tfd =>
                        tfd.IsActive == true &&
                        tfd.ParentID.HasValue &&
                        tfd.Code != null && (
                            tfd.Code.Contains("TASK") //|| tfd.Code.Contains("UQ") || tfd.Code.Contains("BG")
                                                      //|| tfd.Code == "HSCN" || tfd.Code == "HSCT"
                            )
                        ).ToList();
                }
                // translate
                //foreach (var item in listUserTree)
                //{
                //    item.Name = TranslateTreeFilterResource(item);
                //}

                #region Get list user delegations tree filterdocument
                var userDelegation = listUserTree.Find(t => t.Code == "UQ");
                if (userDelegation != null)
                {
                    var userDelegationRepository = new RepositoryBase<UserDelegation>(dbcontext);
                    var userDelegations = userDelegationRepository.GetAll(t => t.IsActive == true
                        && t.ToUserID == userID && t.ToDate.Value >= DateTime.Now).ToList();
                    if (userDelegations.Any() == false)
                    {
                        listUserTree.Remove(userDelegation);
                    }
                }
                #endregion

                #region Get list user handover tree filterdocument
                var userHandover = listUserTree.Find(t => t.Code == "BG");
                if (userHandover != null)
                {
                    var userUserHandoverRepository = new RepositoryBase<UserHandOver>(dbcontext);
                    var userHandovers = userUserHandoverRepository.GetAll(t => t.IsActive == true
                        && t.ToUserID == userID).ToList();

                    if (userHandovers.Any() == false)
                    {
                        listUserTree.Remove(userHandover);
                    }
                }
                #endregion

                #region Filter by role permission

                if (listUserTree != null)
                {
                    foreach (var menuTree in listUserTree)
                    {
                        if (menuTree.IsPrivate != true || menuTree.CreatedBy == userID)
                        {
                            models.Add(menuTree);
                        }
                    }
                }

                #endregion 
            }

            return models;
        }

        public ProjectFilterParamDto GetById(Guid id)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var entity = _objectRepository.GetAll().Where(x => x.Id == id).FirstOrDefault();
                var dto = _mapper.Map<ProjectFilterParamDto>(entity);
                return dto;
            }
        }

        public List<ProjectFilterParamDto> GetProjectFilterParam(string parentCode, Guid userId, bool isGetLv2 = false)
        {
            using (_dbContextScopeFactory.CreateReadOnly())
            {
                var parentData = _objectRepository.GetAll().FirstOrDefault(e => e.Code == parentCode);

                if (parentData != null)
                {
                    var data = _objectRepository.GetAll().Where(e => e.ParentID == parentData.Id && (e.IsPrivate != true || e.CreatedBy == userId)).OrderBy(e => e.NoOrder).ThenBy(e => e.Name).ToList();

                    return _mapper.Map<List<ProjectFilterParamDto>>(data);
                }

                return null;
            }
        }

        public bool Save(ProjectFilterParamDto dto, Guid currenUserId)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                if (dto.ParentID == null || dto.ParentID == Guid.Empty)
                {
                    var entityParent = _objectRepository.GetAll().Where(x => x.ParentID == Guid.Empty && x.Code == "TASK").FirstOrDefault();
                    dto.ParentID = entityParent.Id;
                }
                if (dto.Id != Guid.Empty)
                {
                    var entity = _objectRepository.GetAll().Where(x => x.Id == dto.Id).FirstOrDefault();
                    entity.Name = dto.Name;
                    entity.Code = string.Format("TASK_{0}", string.Join("", dto.Name.ToUpper().Split(' ')));
                    entity.IsCount = dto.IsCount;
                    entity.IsPrivate = dto.IsPrivate;
                    entity.IsLable = dto.IsLable;
                    entity.ParamValue = dto.ParamValue;
                    entity.NoOrder = dto.NoOrder;
                    entity.ParentID = dto.ParentID;
                    entity.ModifiedBy = currenUserId;
                    entity.ModifiedDate = DateTime.Now;
                    _objectRepository.Modify(entity);
                    scope.SaveChanges();
                }
                else
                {
                    var entity = new ProjectFilterParam();
                    entity.Id = Guid.NewGuid();
                    entity.IsActive = true;
                    entity.Name = dto.Name;
                    entity.IsCount = dto.IsCount;
                    entity.IsPrivate = dto.IsPrivate;
                    entity.IsLable = dto.IsLable;
                    entity.ParamValue = dto.ParamValue;
                    entity.NoOrder = dto.NoOrder;
                    entity.ParentID = dto.ParentID;
                    entity.CreatedBy = currenUserId;
                    entity.CreatedDate = DateTime.Now;

                    _objectRepository.Add(entity);
                    scope.SaveChanges();
                }
            }

            return true;
        }

        public bool Delete(Guid Id)
        {
            using (var scope = _dbContextScopeFactory.Create())
            {
                var entity = _objectRepository.GetAll().Where(x => x.Id == Id).FirstOrDefault();
                if (entity == null)
                {
                    return false;
                }

                var entityChild = _objectRepository.GetAll().Where(x => x.ParentID == entity.Id).ToList();

                _objectRepository.DeleteRange(entityChild);

                _objectRepository.Delete(entity);
                scope.SaveChanges();
            }

            return true;
        }
    }
}
