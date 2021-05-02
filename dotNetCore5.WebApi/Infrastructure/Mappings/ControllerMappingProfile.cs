using AutoMapper;
using dotNetCore5.Business.Dtos;
using dotNetCore5.WebApi.ViewModels;

namespace dotNetCore5.WebApi.Infrastructure.Mappings
{
    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            this.CreateMap<CustomersDto, CustomersViewModel>();

            this.CreateMap<CustomersCreateViewModel, CustomersCreateDto>();
            this.CreateMap<CustomersUpdateViewModel, CustomersUpdateDto>();
        }
    }
}
