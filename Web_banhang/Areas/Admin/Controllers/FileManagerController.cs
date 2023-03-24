using Microsoft.AspNetCore.Mvc;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Modal()
        {
            return PartialView();
        }
        public IActionResult GetFile()
        {
            return View();
        }
        public IActionResult Indec()
        {
            return View();
        }
        public IActionResult Summmernote() {
            return View();
        }
    }
}
