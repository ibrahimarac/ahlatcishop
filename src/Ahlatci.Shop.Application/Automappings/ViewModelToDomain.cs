using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Domain.Entities;
using AutoMapper;

namespace Ahlatci.Shop.Application.Automappings
{
    public class ViewModelToDomain : Profile
    {
        public ViewModelToDomain()
        {
            //Kaynak ve hedef arasında property isimleri veya türleri eşleşmezse manuel tanımlama yapmak gerekir.
            CreateMap<CreateCategoryVM, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(e => e.CategoryName));

            CreateMap<UpdateCategoryVM, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(e => e.CategoryName));
        }
    }
}
