using Library.Data.Models;

namespace Library.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);

        Task<IEnumerable<T>> GetAll();

        Task Delete(int id);

        Task Update(T entity);

        Task Add(T entity);
    }
}
