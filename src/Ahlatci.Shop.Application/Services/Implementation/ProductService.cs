using Ahlatci.Shop.Application.Exceptions;
using Ahlatci.Shop.Application.Models.Dtos.Products;
using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Application.Models.RequestModels.Products;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Ahlatci.Shop.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitWork _uWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitWork uWork, IMapper mapper)
        {
            _uWork = uWork;
            _mapper = mapper;
        }

        public async Task<Result<List<ProductDto>>> GetAllProducts()
        {
            var result = new Result<List<ProductDto>>();

            var products = await _uWork.GetRepository<Product>().GetAllAsync();
            var productDtos = await products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider).ToListAsync();

            result.Data = productDtos;
            return result;
        }

        public async Task<Result<List<ProductWithCategoryDto>>> GetAllProductsWithCategory()
        {
            var result = new Result<List<ProductWithCategoryDto>>();

            var products = await _uWork.GetRepository<Product>().GetAllAsync();
            var productDtos = await products.ProjectTo<ProductWithCategoryDto>(_mapper.ConfigurationProvider).ToListAsync();

            result.Data = productDtos;
            return result;
        }

        public async Task<Result<ProductDto>> GetProductById(GetProductByIdVM getProductByIdVM)
        {
            var result = new Result<ProductDto>();

            var productEntity = await _uWork.GetRepository<Product>().GetById(getProductByIdVM.Id);
            var productDto = _mapper.Map<ProductDto>(productEntity);

            result.Data = productDto;
            return result;
        }


        public async Task<Result<int>> CreateProduct(CreateProductVM createProductVM)
        {
            var result = new Result<int>();

            var productExistsSameName = await _uWork.GetRepository<Product>().AnyAsync(x => x.Name == createProductVM.Name.Trim());
            if (productExistsSameName)
            {
                throw new AlreadyExistsException($"{createProductVM.Name} isminde bir ürün daha önce eklenmiştir.");
            }

            var productEntity = _mapper.Map<Product>(createProductVM);
            _uWork.GetRepository<Product>().Add(productEntity);
            await _uWork.CommitAsync();

            result.Data = productEntity.Id;
            return result;
        }

        public async Task<Result<int>> DeleteProduct(DeleteProductVM deleteProductVM)
        {
            var result = new Result<int>();

            var productEntity = await _uWork.GetRepository<Product>().GetById(deleteProductVM.Id);
            if (productEntity is null)
            {
                throw new NotFoundException($"{deleteProductVM.Id} numaralı ürün bulunamadı.");
            }

            _uWork.GetRepository<Product>().Delete(productEntity);
            await _uWork.CommitAsync();

            result.Data = productEntity.Id;
            return result;
        }

        public async Task<Result<int>> UpdateProduct(UpdateProductVM updateProductVM)
        {
            var result = new Result<int>();

            var productEntity = await _uWork.GetRepository<Product>().GetById(updateProductVM.Id);
            if (productEntity is null)
            {
                throw new NotFoundException($"{updateProductVM.Id} numaralı ürün bulunamadı.");
            }

            var productExistsSameName = await _uWork.GetRepository<Product>().AnyAsync(x => x.Name.Trim() == updateProductVM.Name && x.Id != updateProductVM.Id);
            if (productExistsSameName)
            {
                throw new AlreadyExistsException($"{updateProductVM.Name} isimli ürün mevcuttur.");
            }

            _mapper.Map(updateProductVM, productEntity);
            _uWork.GetRepository<Product>().Update(productEntity);
            await _uWork.CommitAsync();

            result.Data = productEntity.Id;
            return result;
        }
    }
}
