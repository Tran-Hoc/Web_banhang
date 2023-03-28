using Microsoft.AspNetCore.Mvc.Rendering;
using Web_banhang.Data;


namespace Web_banhang.Service
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        //Task<List<T>> GetPageListAsync(int? page);
        Task<List<T>> GetSearchAsync(string searchstring);
        Task<T> GetById(int? id);
        Task<bool> CreateAsync(T t);
        Task<bool> UpdateAsync(T t);
        //public Task<T> UpdateByIdAsync(int? id);
        Task<bool> DeleteAsync(int id);
        int IsActive(int id);

        //crud: create read:get update delete
    }
    public interface IProductRepository<T> : IRepository<T>
    {
        Task<bool> CreateAsync(T t, List<string> listImage);
        SelectList GetListProductCategory();
        int IsSale(int id);
        int IsHome(int id);
        int IsHot(int id);
    }
}
