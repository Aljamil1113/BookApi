using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Model;

namespace BookApi.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private BookDbContext db;

        public CategoryRepository(BookDbContext _db)
        {
            db = _db;
        }

        public bool CategoryExist(int categoryId)
        {
            return db.Categories.Any(c => c.Id == categoryId);
        }

        public ICollection<Book> GetAllBooksForCategory(int categoryId)
        {
            return db.BookCategories.Where(b => b.CategoryId == categoryId).Select(b => b.Book).ToList();
        }

        public ICollection<Category> GetCategories()
        {
            return db.Categories.OrderBy(c => c.Name).ToList();
        }

        public ICollection<Category> GetCategoriesOfABook(int bookId)
        {
            return db.BookCategories.Where(c => c.BookId== bookId).Select(c => c.Category).ToList();
        }

        public Category GetCategory(int categoryId)
        {
            return db.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
        }
    }
}
