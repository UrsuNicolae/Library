using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Library.Data.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

                entity.Property(p => p.AuthorId)
                .IsRequired();

                entity.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

                entity.Property(p => p.CategoryId)
                .IsRequired();

                entity.HasOne(e => e.Author)
                .WithMany(a => a.Books)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Category)
                .WithMany(a => a.Books)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

               /* entity.HasMany(e => e.Books)
               .WithOne(a => a.Author)
               .OnDelete(DeleteBehavior.Restrict);*/
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);

               /* entity.HasMany(e => e.Books)
               .WithOne(a => a.Category)
               .OnDelete(DeleteBehavior.Restrict);*/
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
