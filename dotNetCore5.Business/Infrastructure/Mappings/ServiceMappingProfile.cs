using AutoMapper;
using dotNetCore5.Business.Dtos;
using dotNetCore5.DataAccess.DbModel;

namespace dotNetCore5.Business.Infrastructure.Mappings
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<CustomersDbModel, CustomersDto>();

            this.CreateMap<CustomersCreateDto, CustomersCreateDbModel>();
            this.CreateMap<CustomersUpdateDto, CustomersUpdateDbModel>();
        }
    }
}
