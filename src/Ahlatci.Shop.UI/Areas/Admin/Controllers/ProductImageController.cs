using Ahlatci.Shop.UI.Models.Dtos.ProductImages;
using Ahlatci.Shop.UI.Models.RequestModels.ProductImages;
using Ahlatci.Shop.UI.Models.Wrapper;
using Ahlatci.Shop.UI.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahlatci.Shop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy ="Admin")]
    public class ProductImageController : Controller
    {
        private readonly IRestService _restService;

        public ProductImageController(IRestService restService)
        {
            _restService = restService;
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery]int productId)
        {
            var response = await _restService.GetAsync<Result<List<ProductImageDto>>>($"productImage/getAllByProduct/{productId}");

            if(response.StatusCode == HttpStatusCode.OK) // herşey yolunda
            {
                return View(response.Data.Data);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create([FromQuery]int productId)
        {
            var model = new CreateProductImageVM
            {
                ProductId = productId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateProductImageVM productImageModel)
        {
            //Fluent validation içerisinde tanımlanan kurallardan bir veya birkaçı ihlal edildiyse
            if (!ModelState.IsValid)
            {
                return View(productImageModel);
            }

            var formValues = new Dictionary<string, string>
            {
                { "ProductId",productImageModel.ProductId.ToString()},
                {"Order",productImageModel.Order.ToString() },
                {"IsThumbnail",productImageModel.IsThumbnail.ToString() }
            };

            var response = await _restService.PostFormAsync<Result<int>>(formValues, "productImage/create", productImageModel.UploadedImage, true);

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
                return View();
            }
            else // herşey yolunda
            {
                TempData["success"] = $"{response.Data.Data} numaralı kayıt başarıyla eklendi.";
                return RedirectToAction("List", "ProductImage", new { Area = "Admin", ProductId=productImageModel.ProductId });
            }

        }

    }
}
