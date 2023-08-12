using Ahlatci.Shop.Application.Models.Dtos.Products;
using Ahlatci.Shop.Application.Models.RequestModels.Products;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{

    [ApiController]
    [Route("product")]    
    [Authorize("Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("get")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<List<ProductDto>>>> GetAllProducts()
        {
            var categories = await _productService.GetAllProducts();
            return Ok(categories);
        }

        [HttpGet("getWithCategory")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<List<ProductWithCategoryDto>>>> GetAllProductsWithCategory()
        {
            var categories = await _productService.GetAllProductsWithCategory();
            return Ok(categories);
        }

        [HttpGet("get/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Result<ProductDto>>> GetProductById(int id)
        {
            var category = await _productService.GetProductById(new GetProductByIdVM { Id = id });
            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result<int>>> CreateProduct(CreateProductVM createProductVM)
        {
            var categoryId = await _productService.CreateProduct(createProductVM);
            return Ok(categoryId);
        }

        [HttpPut("update/{id:int}")]
        public async Task<ActionResult<Result<int>>> UpdateProduct(int id, UpdateProductVM updateProductVM)
        {
            if(id != updateProductVM.Id)
            {
                return BadRequest();
            }
            var categoryId = await _productService.UpdateProduct(updateProductVM);
            return Ok(categoryId);
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<ActionResult<Result<int>>> DeleteProduct(int id)
        {
            var categoryId = await _productService.DeleteProduct(new DeleteProductVM { Id = id});
            return Ok(categoryId);
        }

    }
}

