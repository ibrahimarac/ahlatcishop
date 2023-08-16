using Microsoft.AspNetCore.Mvc;

namespace Ahlatci.Shop.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Header = "Ahlatçı Shop";
            ViewBag.Title = "Yönetim Paneli";
            return View();
        }
    }
}
