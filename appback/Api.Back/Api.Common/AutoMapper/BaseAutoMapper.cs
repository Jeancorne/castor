using Api.Common.Dto.ClsEmployees;
using Api.Core.Entities;
using AutoMapper;

namespace Api.Common.AutoMapper
{
    public class BaseAutoMapper : Profile
    {
        public BaseAutoMapper()
        {
            //ClsEmployees
            CreateMap<RegisterEmployeeDto, ClsEmployees>().ReverseMap();
        }
    }
}