using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_banhang.Data;
using Web_banhang.DataContext;
using Web_banhang.Models;
namespace Web_banhang.Service
{
    public class ProductCategoryRepository : IRepository<ProdCategoryVM>
    {
        private readonly WebBanHangContext _context;

        private IMapper _mapper
        {
            get;
        }

        public ProductCategoryRepository(WebBanHangContext context, IMapper mapper)
        {
            this._mapper = mapper;
            this._context = context;
        }
        public async Task<bool> CreateAsync(ProdCategoryVM t)
        {
            if (_context.TbProductCategories.Any())
            {
                try
                {
                    TbProductCategory item = _mapper.Map<TbProductCategory>(t);
                    item.CreatedDate = DateTime.Now;
                    item.ModifiedDate = DateTime.Now;
                    item.Alias = Filter.FilterChar(t.Title);
                    await _context.AddAsync(item);
                    await _context.SaveChangesAsync();
                    return true;
                }

                catch (Exception )
                {
                    return false;
                }

            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = _context.TbProductCategories.Where(e => e.Id == id).FirstOrDefault();
            if (result != null)
            {
                _context.TbProductCategories.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<ProdCategoryVM>> GetAllAsync()
        {
            var list = await _context.TbProductCategories.OrderByDescending(x => x.Id).ToListAsync();
            //var listitem = from s in _context.TbProductCategories
            //               orderby s.Id descending
            //               select s;
            List<ProdCategoryVM> result = new List<ProdCategoryVM>();
            foreach (var item in list)
            {

                ProdCategoryVM prodCategoryVMprodCategoryVM = _mapper.Map<ProdCategoryVM>(item);
                prodCategoryVMprodCategoryVM.Icon = "~/files/Image/" + prodCategoryVMprodCategoryVM.Icon;
                result.Add(prodCategoryVMprodCategoryVM);
            }
            return result;
        }

        public async Task<ProdCategoryVM?> GetById(int? id)
        {

            if (id == null || _context.TbProductCategories == null)
            {
                return null;
            }

            var item = await _context.TbProductCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return null;
            }
            ProdCategoryVM prodCategoryVMprodCategoryVM = _mapper.Map<ProdCategoryVM>(item);
            prodCategoryVMprodCategoryVM.Icon = "~/files/Image/" + prodCategoryVMprodCategoryVM.Icon;
            return prodCategoryVMprodCategoryVM;
        }

        public Task<List<ProdCategoryVM>> GetPageListAsync(int? page)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProdCategoryVM>> GetSearchAsync(string searchstring)
        {
            throw new NotImplementedException();
        }

        public int IsActive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(ProdCategoryVM t)
        {
            if (_context.TbProductCategories != null)
            {
                try
                {
                    TbProductCategory tbProductCategory = _mapper.Map<TbProductCategory>(t);

                    _context.Entry(tbProductCategory).State = EntityState.Modified;
                    tbProductCategory.CreatedDate = DateTime.Now;
                    tbProductCategory.ModifiedDate = DateTime.Now;
                    tbProductCategory.Alias = Filter.FilterChar(t.Title);
                    _context.Entry(tbProductCategory).State = EntityState.Modified;
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

        public Task<ProdCategoryVM> UpdateByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
