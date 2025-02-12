using Library.Data.Data;
using Library.Data.Models;
using Library.Data.Repositories.Interfaces;

namespace Library.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context)
        {
            
        }
    }
}
