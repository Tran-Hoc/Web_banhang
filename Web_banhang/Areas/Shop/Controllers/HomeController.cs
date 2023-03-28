using Microsoft.AspNetCore.Mvc;

namespace Web_banhang.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
