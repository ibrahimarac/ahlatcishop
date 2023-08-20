using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.Dtos.ProductImages;
using Ahlatci.Shop.Application.Models.Dtos.Products;
using Ahlatci.Shop.Application.Models.RequestModels.ProductImages;
using Ahlatci.Shop.Application.Models.RequestModels.Products;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{

    [ApiController]
    [Route("productImage")]    
    //[Authorize("Admin")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpGet("getAllByProduct/{id:int?}")]
        public async Task<ActionResult<Result<List<ProductImageDto>>>>GetAllImagesByProduct(int? id)
        {
            var result = await _productImageService.GetAllImagesByProduct(new GetAllProductImageByProductVM { ProductId=id});
            return Ok(result);
        }

        [HttpGet("getAllDetailByProduct/{id:int?}")]
        public async Task<ActionResult<Result<List<ProductImageWithProductDto>>>> GetAllImagesWithProductByProduct(int? id)
        {
            var result = await _productImageService.GetAllProductImagesWithProduct(new GetAllProductImageByProductVM { ProductId = id });
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result<int>>> CreateProductImage([FromForm]CreateProductImageVM createProductImageVM)
        {
            var result = await _productImageService.CreateProductImage(createProductImageVM);
            return Ok(result);
        }

        [HttpDelete("delete/{id:int?}")]
        public async Task<ActionResult<Result<int>>> DeleteProductImage(int? id)
        {
            var result = await _productImageService.DeleteProductImage(new DeleteProductImageVM { Id = id});
            return Ok(result);
        }


    }
}

