using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.Api.Controllers
{

    //Endpoint url : [ControllerRoute]/[ActionRoute]
    //category/getAll

    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return categories;
        }
    }
}

