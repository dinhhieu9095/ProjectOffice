using AutoMapper;
using DaiPhatDat.Core.Kernel.Application.Helpers;
using DaiPhatDat.Core.Kernel.Mapper;
using DaiPhatDat.Core.Kernel.Orgs.Application.Contract;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DaiPhatDat.Core.Kernel.Orgs.Application.Dto
{
    public class UserInfo : IMapping<UserDepartmentDto>, IComplexMapping
    {
        public UserInfo()
        {
            Departments = new List<DepartmentInfo>();
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

        public List<DepartmentInfo> Departments { get; set; }
        public List<string> Roles { get; set; }

        public void CreateMap(IMapperConfigurationExpression mapperConfigurationExpression)
        {
            mapperConfigurationExpression.CreateMap<UserDepartmentDto, UserInfo>()
           .ForMember(dest => dest.Id, member => member.MapFrom(src => src.UserID))
           .ForMember(dest => dest.JobTitle, member => member.MapFrom(src => src.JobTitleName))
           .ForMember(dest => dest.DepartmentName, member => member.MapFrom(src => src.DeptName));
        }
    }
}