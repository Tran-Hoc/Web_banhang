namespace Web_banhang.Service
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetPageListAsync(int? page);
        Task<List<T>> GetSearchAsync(string searchstring);
        Task<T> GetById(int? id);
        Task<bool> CreateAsync(T t);
        Task<bool> UpdateAsync(T t);
        public Task<T> UpdateByIdAsync(int? id);
        Task<bool> DeleteAsync(int id);
        int IsActive(int id);

        //crud: create read:get update delete
    }
}
