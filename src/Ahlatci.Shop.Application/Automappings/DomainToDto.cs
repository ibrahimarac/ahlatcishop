using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Domain.Entities;
using AutoMapper;

namespace Ahlatci.Shop.Application.Automappings
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
