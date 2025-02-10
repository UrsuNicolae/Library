namespace Library.Data.Models
{
    public sealed class Book : BaseEntity
    {

        public string Title { get; set; }

        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public Author? Author { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
