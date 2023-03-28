using Microsoft.AspNetCore.Mvc;
using Web_banhang.Models;
using Web_banhang.Service;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProdCategoryController : Controller
    {
        private readonly IRepository<ProdCategoryVM> _context;

        public ProdCategoryController(IRepository<ProdCategoryVM> context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<ProdCategoryVM> list = await _context.GetAllAsync();
                return View(list);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdCategoryVM model)
        {

            if (ModelState.IsValid)
            {
                if (await _context.CreateAsync(model))
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                ProdCategoryVM vMNews = await _context.GetById(id);
                if (vMNews == null)
                {
                    return NotFound();
                }
                return View(vMNews);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProdCategoryVM model)
        {
            if (ModelState.IsValid)
            {
                await _context.UpdateAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
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
    }
}
