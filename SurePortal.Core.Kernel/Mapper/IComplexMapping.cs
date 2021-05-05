using AutoMapper;

namespace DaiPhatDat.Core.Kernel.Mapper
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
