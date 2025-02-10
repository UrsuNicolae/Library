using Library.Data.Models;

namespace Library.Data.Repositories.Inteterfaces
{
    public interface IAuthorRepository
    {
        Task<Author> GetById(int id);

    }
}
