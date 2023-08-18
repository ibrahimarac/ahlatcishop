using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.Dtos.Accounts;
using Ahlatci.Shop.Application.Models.Dtos.Cities;
using Ahlatci.Shop.Application.Models.Dtos.Customers;
using Ahlatci.Shop.Application.Models.Dtos.OrderDetails;
using Ahlatci.Shop.Application.Models.Dtos.Orders;
using Ahlatci.Shop.Application.Models.Dtos.ProductImages;
using Ahlatci.Shop.Application.Models.Dtos.Products;
using Ahlatci.Shop.Domain.Entities;
using AutoMapper;

namespace Ahlatci.Shop.Application.Automappings
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<Customer, CustomerDto>();

            CreateMap<Account, AccountDto>();

            CreateMap<City, CityDto>();

            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductWithCategoryDto>();
            CreateMap<Product, ProductWithImagesDto>();

            CreateMap<ProductImage, ProductImageDto>();
            CreateMap<ProductImage, ProductImageWithProductDto>();

            CreateMap<Order, OrderDto>();

            CreateMap<OrderDetail, OrderDetailDto>();
        }
    }
}
