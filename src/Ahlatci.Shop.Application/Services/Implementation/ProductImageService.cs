using Ahlatci.Shop.Application.Behaviors;
using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.Dtos.ProductImages;
using Ahlatci.Shop.Application.Models.RequestModels.ProductImages;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Validators.ProductImages;
using Ahlatci.Shop.Application.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ahlatci.Shop.Application.Services.Implementation
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitWork _unitWork;
        private readonly IMapper _mapper;

        public ProductImageService(IUnitWork unitWork, IMapper mapper)
        {
            _unitWork = unitWork;
            _mapper = mapper;
        }

        [ValidationBehavior(typeof(GetAllProductImageByProductValidator))]
        public async Task<Result<List<ProductImageDto>>> GetAllImagesByProduct(GetAllProductImageByProductVM getByProductVM)
        {
            var result = new Result<List<ProductImageDto>>();

            var productImageEntities = await _unitWork.GetRepository<ProductImage>().GetByFilterAsync(x => x.ProductId == getByProductVM.ProductId);
            var productImageDtos = await productImageEntities.ProjectTo<ProductImageDto>(_mapper.ConfigurationProvider).ToListAsync();

            result.Data = productImageDtos;
            return result;
        }

        [ValidationBehavior(typeof(GetAllProductImageByProductValidator))]
        public async Task<Result<List<ProductImageWithProductDto>>> GetAllProductImagesWithProduct(GetAllProductImageByProductVM getByProductVM)
        {
            var result = new Result<List<ProductImageWithProductDto>>();

            var productImageEntities = await _unitWork.GetRepository<ProductImage>().GetByFilterAsync(x => x.ProductId == getByProductVM.ProductId);
            var productImageDtos = await productImageEntities.ProjectTo<ProductImageWithProductDto>(_mapper.ConfigurationProvider).ToListAsync();

            result.Data = productImageDtos;
            return result;
        }

        [ValidationBehavior(typeof(CreateProductImageValidator))]
        public Task<Result<int>> CreateProductImage(CreateProductImageVM createProductImageVM)
        {
            throw new NotImplementedException();
        }

        [ValidationBehavior(typeof(DeleteProductImageValidator))]
        public Task<Result<int>> DeleteProductImage(DeleteProductImageVM deleteProductImageVM)
        {
            throw new NotImplementedException();
        }

        
    }
}
