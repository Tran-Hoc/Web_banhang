using Microsoft.AspNetCore.Mvc;
using Web_banhang.Models;
using Web_banhang.Service;
using X.PagedList;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductRepository<ProductVM> _context;

        public ProductController(IProductRepository<ProductVM> context)
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
            ViewBag.ProdcateList = _context.GetListProductCategory();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductVM model, List<string> LstImage)
        {

            if (ModelState.IsValid)
            {
                if (await _context.CreateAsync(model, LstImage))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                ProductVM vmodel = await _context.GetById(id);
                if (vmodel == null)
                {
                    return NotFound();
                }
                return View(vmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductVM models)
        {

            if (ModelState.IsValid)
            {
                await _context.UpdateAsync(models);
                return RedirectToAction("Index");
            }
            return View(models);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _context.DeleteAsync(id) == false)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (await _context.DeleteAsync(Convert.ToInt32(item)) == false)
                        {
                            return Json(new { success = false });
                        }
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        [HttpPost, ActionName("IsActive")]
        [ValidateAntiForgeryToken]
        public IActionResult IsActive(int id)
        {
            var isactive = _context.IsActive(id);

            if (isactive == 1)
            {
                return Json(new { success = true, isActive = true });
            }
            else if (isactive == 2)
            {
                return Json(new { success = true, isActive = false });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        [HttpPost, ActionName("IsSale")]
        [ValidateAntiForgeryToken]
        public IActionResult IsSale(int id)
        {
            var isactive = _context.IsSale(id);

            if (isactive == 1)
            {
                return Json(new { success = true, isActive = true });
            }
            else if (isactive == 2)
            {
                return Json(new { success = true, isActive = false });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        [HttpPost, ActionName("IsHome")]
        [ValidateAntiForgeryToken]
        public IActionResult IsHome(int id)
        {
            var isactive = _context.IsHome(id);

            if (isactive == 1)
            {
                return Json(new { success = true, isActive = true });
            }
            else if (isactive == 2)
            {
                return Json(new { success = true, isActive = false });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        [HttpPost, ActionName("IsHot")]
        [ValidateAntiForgeryToken]
        public IActionResult IsHot(int id)
        {
            var isactive = _context.IsHot(id);

            if (isactive == 1)
            {
                return Json(new { success = true, isActive = true });
            }
            else if (isactive == 2)
            {
                return Json(new { success = true, isActive = false });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
