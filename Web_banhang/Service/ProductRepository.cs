using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;

namespace Web_banhang.Service
{
    public class ProductRepository : IRepository<ProductVM>
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

        public async Task<bool> CreateAsync(ProductVM t)
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

        public Task<ProductVM> UpdateByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
