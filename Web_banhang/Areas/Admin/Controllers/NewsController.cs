using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;
using Web_banhang.Service;
using X.PagedList;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : Controller
    {
        private readonly IRepository<NewsVM> _context;

        public NewsController(IRepository<NewsVM> context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchstring, int? page)
        {
            try
            {
                List<NewsVM> list;
                if (!string.IsNullOrEmpty(searchstring))
                {
                    //list = list.Where(x => x.Alias.Contains(searchstring) || x.Title.Contains(searchstring));
                    ViewBag.searchstring = searchstring ?? null;
                    list = await _context.GetSearchAsync(searchstring);
                   
                }
                else
                {
                    list = await _context.GetAllAsync();
                }
                int pagesize = 5;
                int pageNumber = (page ?? 1);
                return View(list.ToPagedList(pageNumber, pagesize));
                //return View(list);
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
        public async Task<IActionResult> Create(NewsVM newsModel)
        {

            if (ModelState.IsValid)
            {
                if (await _context.CreateAsync(newsModel))
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
                NewsVM vMNews = await _context.GetById(id);
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
        public async Task<IActionResult> Edit(NewsVM newsModel)
        {

            if (ModelState.IsValid)
            {
                await _context.UpdateAsync(newsModel);
                //    newsModel.CreatedDate = DateTime.Now;
                //    newsModel.ModifiedDate = DateTime.Now;
                //    newsModel.Alias = Web_banhang.Models.Filter.FilterChar(newsModel.Title);
                //    _context.TbNews.CreateAsync(newsModel);
                //    await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(newsModel);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (await _context.DeleteAsync(id) == false)
            {
                //return Problem("Entity set 'WebBanHangContext.TbCategories'  is null.");
                return Json(new { success = false });
            }

            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
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

    }
}
