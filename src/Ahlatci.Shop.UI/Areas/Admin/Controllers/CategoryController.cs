using Ahlatci.Shop.UI.Models.Dtos;
using Ahlatci.Shop.UI.Models.RequestModels;
using Ahlatci.Shop.UI.Models.Wrapper;
using Ahlatci.Shop.UI.Services.Abstraction;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ahlatci.Shop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IRestService _restService;
        private readonly IMapper _mapper;

        public CategoryController(IRestService restService, IMapper mapper)
        {
            _restService = restService;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            ViewBag.Header = "Kategori İşlemleri";
            ViewBag.Title = "Yeni Kategori Oluştur";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryVM categoryModel)
        {
            //Fluent validation içerisinde tanımlanan kurallardan bir veya birkaçı ihlal edildiyse
            if (!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            //Model validasyonu başarılı. Kaydı gerçekleştir.
            var response = await _restService.PostAsync<CreateCategoryVM, Result<int>>(categoryModel, "category/create");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                TempData["error"] = "Devam etmek için sisteme giriş yapmanız gerekmektedir.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                TempData["error"] = "Bu işlem için gerekli yetkiye sahip değilsiniz.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
                return View();
            }
            else // herşey yolunda
            {
                TempData["success"] = $"{response.Data.Data} numaralı kayıt başarıyla eklendi.";
                return RedirectToAction("List", "Category", new { Area = "Admin" });
            }
        }



        public async Task<IActionResult> List()
        {
            ViewBag.Header = "Kategori İşlemleri";
            ViewBag.Title = "Kategori Düzenle";

            //Apiye istek at
            //category/get
            var response = await _restService.GetAsync<Result<List<CategoryDto>>>("category/get");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                TempData["error"] = "Devam etmek için sisteme giriş yapmanız gerekmektedir.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                TempData["error"] = "Bu işlem için gerekli yetkiye sahip değilsiniz.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", "İşlem esnasında sunucu taraflı bir hata oluştu. Lütfen sistem yöneticinize başvurunuz.");
                return View();
            }
            else
            {
                return View(response.Data.Data);
            }
        }



        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Header = "Kategori İşlemleri";
            ViewBag.Title = "Kategori Güncelle";

            //ilgili kategoriyi bul ve View'e git
            var response = await _restService.GetAsync<Result<CategoryDto>>($"category/get/{id}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                TempData["error"] = "Devam etmek için sisteme giriş yapmanız gerekmektedir.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                TempData["error"] = "Bu işlem için gerekli yetkiye sahip değilsiniz.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
                return View();
            }
            else // herşey yolunda
            {
                var model = _mapper.Map<UpdateCategoryVM>(response.Data.Data);
                return View(model);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryVM updateCategoryModel)
        {
            var response = await _restService.PutAsync<UpdateCategoryVM, Result<int>>(updateCategoryModel, $"category/update/{updateCategoryModel.Id}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                TempData["error"] = "Devam etmek için sisteme giriş yapmanız gerekmektedir.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                TempData["error"] = "Bu işlem için gerekli yetkiye sahip değilsiniz.";
                return RedirectToAction("SignIn", "Login", new { Area = "Admin" });
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                ModelState.AddModelError("", response.Data.Errors[0]);
                return View();
            }
            else // herşey yolunda
            {
                TempData["success"] = $"{response.Data.Data} numaralı kayıt başarıyla güncellendi.";
                return RedirectToAction("List", "Category", new { Area = "Admin" });
            }

        }




        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //api endpointi çağır
            //category/delete/id

            var response = await _restService.DeleteAsync<Result<int>>($"category/delete/{id}");

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return Json(new Result<int>
                {
                    Success = false,
                    Errors = new List<string> { "Sisteme giriş yapmanız gerekmektedir." }
                });
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                return Json(new Result<int>
                {
                    Success = false,
                    Errors = new List<string> { "Bu işlem için gerekli yetkiye sahip değilsiniz." }
                });
            }
            else
            {
                return Json(response.Data);
            }

        }
    }
}
