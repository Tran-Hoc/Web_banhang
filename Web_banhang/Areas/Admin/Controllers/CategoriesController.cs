using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;

namespace Web_banhang.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly WebBanHangContext _context;

        public CategoriesController(WebBanHangContext context)
        {
            _context = context;
        }

        // GET: Admin/TbCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbCategories.ToListAsync());
        }

        // GET: Admin/TbCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        // GET: Admin/TbCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TbCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,SeoTitle,SeoDescription,SeoKeywords,Position")] TbCategory tbCategory)
        {
            if (ModelState.IsValid)
            {
                tbCategory.CreatedDate = DateTime.Now;
                tbCategory.ModifiedDate = DateTime.Now;
                tbCategory.Alias = Filter.FilterChar(tbCategory.Title);
                _context.Add(tbCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCategory);
        }

        // GET: Admin/TbCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories.FindAsync(id);
            if (tbCategory == null)
            {
                return NotFound();
            }
            return View(tbCategory);
        }

        // POST: Admin/TbCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,SeoTitle,SeoDescription,SeoKeywords,Position,CreatedBy,CreatedDate,ModifiedDate,Modifiedby,Alias,IsActive")] TbCategory tbCategory)
        {
            if (id != tbCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tbCategory.ModifiedDate = DateTime.Now;
                    tbCategory.Alias = Filter.FilterChar(tbCategory.Title);
                    _context.Update(tbCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCategoryExists(tbCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tbCategory);
        }

        // GET: Admin/TbCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TbCategories == null)
            {
                return NotFound();
            }

            var tbCategory = await _context.TbCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tbCategory == null)
            {
                return NotFound();
            }

            return View(tbCategory);
        }

        // POST: Admin/TbCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TbCategories == null)
            {
                //return Problem("Entity set 'WebBanHangContext.TbCategories'  is null.");
                return Json(new { success = false });
            }
            var tbCategory = await _context.TbCategories.FindAsync(id);
            if (tbCategory != null)
            {
                _context.TbCategories.Remove(tbCategory);
            }

            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return Json(new { success = true });
        }

        private bool TbCategoryExists(int id)
        {
            return _context.TbCategories.Any(e => e.Id == id);
        }
    }
}
