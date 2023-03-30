using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;

namespace Web_banhang.Service
{
    public class ProductRepository : IProductRepository<ProductVM>
    {
        private readonly WebBanHangContext _context;

        private IMapper _mapper
        {
            get;
        }

        public ProductRepository(WebBanHangContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }

        public async Task<bool> CreateAsync(ProductVM t, List<string> listimage)
        {
            if (_context.TbProductCategories.Any())
            {
                try
                {

                    TbProduct item = _mapper.Map<TbProduct>(t);
                    item.CreatedDate = DateTime.Now;
                    item.ModifiedDate = DateTime.Now;
                    item.Alias = Filter.FilterChar(t.Title);
                    await _context.AddAsync(item);
                    await _context.SaveChangesAsync();

                    int productid = item.Id;
                    foreach (var image in listimage)
                    {
                        TbProductImage imageitem = new TbProductImage
                        {
                            ProductId = productid,
                            Image = image
                        };
                        await _context.AddAsync(imageitem);
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }

                catch (Exception)
                {
                    return false;
                }

            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = _context.TbProducts.Where(e => e.Id == id).FirstOrDefault();
            if (result != null)
            {
                _context.TbProducts.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            var list = await _context.TbProducts.OrderByDescending(x => x.Id).ToListAsync();

            List<ProductVM> result = new List<ProductVM>();
            foreach (var item in list)
            {

                ProductVM element = _mapper.Map<ProductVM>(item);
                element.Image = "~/files/Image/" + element.Image;
                result.Add(element);
            }
            return result;
        }

        public async Task<ProductVM?> GetById(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return null;
            }

            var item = await _context.TbProducts.FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return null;
            }
            ProductVM element = _mapper.Map<ProductVM>(item);
            element.Image = "~/files/Image/" + element.Image;
            return element;
        }

        public Task<List<ProductVM>> GetPageListAsync(int? page)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductVM>> GetSearchAsync(string searchstring)
        {
            throw new NotImplementedException();
        }

        public int IsActive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ProductVM t)
        {
            if (_context.TbProducts != null)
            {
                try
                {
                    TbProduct element = _mapper.Map<TbProduct>(t);

                    _context.Entry(element).State = EntityState.Modified;
                    element.CreatedDate = DateTime.Now;
                    element.ModifiedDate = DateTime.Now;
                    element.Alias = Filter.FilterChar(t.Title);
                    _context.Entry(element).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;

        }


        public int IsHot(int id)
        {
            var item = _context.TbProducts.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.IsHot = !item.IsHot;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return item.IsHot ? 1 : 2; // true 1 : false 2
            }
            return 0;
        }

        public int IsHome(int id)
        {
            var item = _context.TbProducts.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return item.IsHome ? 1 : 2; // true 1 : false 2
            }
            return 0;
        }

        public int IsSale(int id)
        {
            var item = _context.TbProducts.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return item.IsSale ? 1 : 2; // true 1 : false 2
            }
            return 0;
        }

        public int IsActive(int id)
        {
            var item = _context.TbProducts.FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
                return item.IsActive ? 1 : 2; // true 1 : false 2
        }
            return 0;
        }

        public Task<ProductVM> UpdateByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(ProductVM t)
        {
            throw new NotImplementedException();
        }

        public SelectList GetListProductCategory()
        {
            try
            {
                var list = new SelectList(_context.TbProductCategories, "Id", "Title");
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
