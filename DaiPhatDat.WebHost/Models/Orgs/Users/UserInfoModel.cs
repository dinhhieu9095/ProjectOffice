using AutoMapper;
using SurePortal.Core.Kernel.Application.Helpers;
using SurePortal.Core.Kernel.Mapper;
using SurePortal.Core.Kernel.Orgs.Application.Contract;
using SurePortal.Core.Kernel.Orgs.Application.Dto;
using SurePortal.WebHost.Models.Orgs.Departments;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SurePortal.WebHost.Models.Orgs.Users
{
    public class UserInfoModel : IMapping<UserDepartmentModel>, IComplexMapping
    {
        public UserInfoModel()
        {
            Departments = new List<DepartmentInfoModel>();
            Roles = new List<string>();
        }
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string Title
        {
            get
            {
                return $"{JobTitle} - {DepartmentName}";
            }
        }
        [IgnoreDataMember]
        public string SearchText
        {
            get
            {
                return StringHelper.ConvertToNoSign($"{UserName}{FullName}{Phone}{JobTitle}{DepartmentName}");
            }
        }

        public List<DepartmentInfoModel> Departments { get; set; }
        public List<string> Roles { get; set; }

        public void CreateMap(IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.CreateMap<UserDepartmentModel, UserInfoModel>()
           .ForMember(dest => dest.Id, member => member.MapFrom(src => src.UserID))
           .ForMember(dest => dest.JobTitle, member => member.MapFrom(src => src.JobTitleName))
           .ForMember(dest => dest.DepartmentName, member => member.MapFrom(src => src.DeptName));
        }
    }
}