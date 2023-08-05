using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Application.Models.RequestModels.Accounts;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Utils;
using AutoMapper;
using Microsoft.Extensions.Configuration;

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

            //Kullanıcı oluşturma isteği
            CreateMap<RegisterVM, Customer>();
            CreateMap<RegisterVM, Account>()
                .ForMember(x => x.Role, y => y.MapFrom(e => Roles.User));
        }
    }
}
