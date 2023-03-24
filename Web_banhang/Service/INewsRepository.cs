namespace Web_banhang.Service
{
    public interface INewsRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int? id);
        Task<bool> AddAsync(T t);
        Task<bool>  Update(T t);
        public Task<T> UpdateById(int? id);
        bool Delete(int id);
        int IsActive(int id);
    }
}
