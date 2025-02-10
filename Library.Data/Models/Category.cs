namespace Library.Data.Models
{
    public sealed class Category : BaseEntity
    {

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
