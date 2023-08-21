using Ahlatci.Shop.UI.Models.Dtos.ProductImages;
using Ahlatci.Shop.UI.Models.RequestModels.ProductImages;
using Ahlatci.Shop.UI.Models.Wrapper;
using Ahlatci.Shop.UI.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

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

            //Upload edilen resim dosyasını base64 string'e çevirerek istek yapacağımız modele ekleyelim.
            var ms = new MemoryStream();
            productImageModel.ImageFile.CopyTo(ms);
            var fileAsByte = ms.ToArray();

            var fileAsBase64String = Convert.ToBase64String(fileAsByte);
            //Gelen modelde yer alan ve şu an null olan ilgili property'ye dosya içeriği
            //base64 string olarak yazılır. Bu bilgiyi api bekliyor.
            productImageModel.UploadedImage = fileAsBase64String;

            var formData = new Dictionary<string, string>
            {
                {"ProductId",productImageModel.ProductId.ToString() },
                {"Order",productImageModel.Order.ToString() },
                {"IsThumbnail",productImageModel.IsThumbnail.ToString() },
                {"UploadedImage",productImageModel.UploadedImage }
            };

            var response = await _restService.PostFormAsync<Result<int>>(formData, "productImage/create");

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


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //api endpointi çağır
            //productImage/delete/id

            var response = await _restService.DeleteAsync<Result<int>>($"productImage/delete/{id}");

            return Json(response.Data);

        }

    }
}
