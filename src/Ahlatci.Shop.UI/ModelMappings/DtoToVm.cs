using Ahlatci.Shop.UI.Models.Dtos;
using Ahlatci.Shop.UI.Models.RequestModels;
using AutoMapper;

namespace Ahlatci.Shop.UI.ModelMappings
{
    public class DtoToVm : Profile
    {
        public DtoToVm()
        {
            CreateMap<CategoryDto, UpdateCategoryVM>()
                .ForMember(x => x.CategoryName, y => y.MapFrom(e => e.Name));
        }
    }
}
