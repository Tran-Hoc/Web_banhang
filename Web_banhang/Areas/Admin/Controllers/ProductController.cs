using Microsoft.AspNetCore.Mvc;
using Web_banhang.Models;
using Web_banhang.Service;
using X.PagedList;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IRepository<ProductVM> _context;

        public ProductController(IRepository<ProductVM> context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? page)
        {
            try
            {
                List<ProductVM> list = await _context.GetAllAsync();
                int pagesize = 5;
                int pageNumber = (page ?? 1);
                return View(list.ToPagedList(pageNumber, pagesize));
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
