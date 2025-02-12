using Library.Data.Data;
using Library.Data.Models;
using Library.Data.Repositories.Interfaces;

namespace Library.Data.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
            
        }
    }
}
