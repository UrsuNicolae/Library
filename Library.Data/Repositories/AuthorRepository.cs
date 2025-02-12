using Library.Data.Data;
using Library.Data.Models;
using Library.Data.Repositories.Interfaces;

namespace Library.Data.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context)
        {
            
        }
    }
}
