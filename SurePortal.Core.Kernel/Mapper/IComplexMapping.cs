using AutoMapper;

namespace SurePortal.Core.Kernel.Mapper
{
    public interface IComplexMapping : IMapping
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapperConfigurationExpression"></param>
        void CreateMap(IMapperConfigurationExpression mapperConfigurationExpression);
    }
}
