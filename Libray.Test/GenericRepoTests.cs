using Library.Data.Data;
using Library.Data.Models;
using Library.Data.Repositories;
using Library.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Libray.Test
{
    public class GenericRepoTests
    {
        [Fact]
        public async Task GetById_ShouldReturnEntity_WhenExists()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();


            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            var result = await genericRepository.GetById(1);
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
        }

        [Fact]
        public async Task GetById_ShouldThrow_WhenDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);

            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => genericRepository.GetById(1));
        }

        [Fact]
        public async Task Add_ShouldCreateEntity_WhenDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };


            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            await genericRepository.Add(category);

            var result = await context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            Assert.NotNull(result);
            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
        }

        [Fact]
        public async Task Add_ShouldThrow_WhenExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            await Assert.ThrowsAsync<ArgumentException>(() => genericRepository.Add(category));
        }

        [Fact]
        public async Task Delete_ShouldDeleteEntity_WhenExists()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            await genericRepository.Delete(1);

            var result = await context.Categories.FirstOrDefaultAsync(c => c.Id == 1);
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldThrow_WhenDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);

            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => genericRepository.Delete(1));
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllEntities_WhenExists()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            var result = await genericRepository.GetAll();
            var contextResult = await context.Categories.ToListAsync();
            Assert.NotNull(result);
            Assert.Equal(contextResult.Count(), result.Count());
            Assert.Equal(contextResult, result);
        }

        [Fact]
        public async Task Update_ShouldUpdateEntity_WhenExists()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var category = new Category
            {
                Id = 1,
                Name = "Test"
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            var updatedCategory = new Category
            {
                Id = 1,
                Name = "TestUpdated"
            };


            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            await genericRepository.Update(updatedCategory);

            category = await context.Categories.FirstOrDefaultAsync(c => c.Id == updatedCategory.Id);

            Assert.Equal(category.Id, updatedCategory.Id);
            Assert.Equal(category.Name, updatedCategory.Name);
        }

        [Fact]
        public async Task Update_ShouldThrow_WhenDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DataContext(options);
            var category = new Category()
            {
                Id = 1,
                Name = "Test"
            };

            IGenericRepository<Category> genericRepository = new GenericRepository<Category>(context);
            await Assert.ThrowsAsync<KeyNotFoundException>(() => genericRepository.Update(category));
        }
    }
}
