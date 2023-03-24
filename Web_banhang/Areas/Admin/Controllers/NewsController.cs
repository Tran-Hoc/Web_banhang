using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;
using Web_banhang.Service;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly INewsRepository<VMNews> _context;

        public NewsController(INewsRepository<VMNews> context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.GetAll());
            }
            catch
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
        public async Task<IActionResult> Create(VMNews newsModel)
        {

            if (ModelState.IsValid)
            {
               if( await _context.AddAsync(newsModel))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(newsModel);
                }
            }
            return View(newsModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                VMNews vMNews = await _context.GetById(id);
                if (vMNews == null)
                {
                    return NotFound();
                }
                return View(vMNews);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VMNews newsModel)
        {

            if (ModelState.IsValid)
            {
                _context.Update(newsModel);
                //    newsModel.CreatedDate = DateTime.Now;
                //    newsModel.ModifiedDate = DateTime.Now;
                //    newsModel.Alias = Web_banhang.Models.Filter.FilterChar(newsModel.Title);
                //    _context.TbNews.AddAsync(newsModel);
                //    await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newsModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Delete(id) == false)
            {
                //return Problem("Entity set 'WebBanHangContext.TbCategories'  is null.");
                return Json(new { success = false });
            }

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Json(new { success = true });
        }
        [HttpPost]
        public IActionResult IsActive(int id)
        {
            var isactive = _context.IsActive(id);
            if (isactive == 0)
            {
                return Json(new { success = false });
            }
            else if (isactive == 1) 
            {
                return Json(new { success = true, isActive = true });
            }
            else
            {
                return Json(new { success = true, isActive = true });
            }

        }

    }
}
