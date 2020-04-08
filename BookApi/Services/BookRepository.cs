using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Model;

namespace BookApi.Services
{
    public class BookRepository : IBookRepository
    {
        private BookDbContext bookContext;

        public BookRepository(BookDbContext _bookContext)
        {
            bookContext = _bookContext;
        }

        public Book GetBook(int bookId)
        {
            return bookContext.Books.Where(b => b.Id == bookId).FirstOrDefault();
        }

        public Book GetBook(string bookIsbn)
        {
            return bookContext.Books.Where(b => b.Isbn == bookIsbn).FirstOrDefault();
        }

        public decimal GetBookRating(int bookId)
        {
            var reviews = bookContext.Reviews.Where(r => r.Book.Id == bookId);

            if (reviews.Count() <= 0)
                return 0;

            return ((decimal)(reviews.Sum(r => r.Rating) / reviews.Count()));
        }

        public ICollection<Book> GetBooks()
        {
            return bookContext.Books.OrderBy(b => b.Title).ToList();
        }

        public bool IsBookExist(string bookIsbn)
        {
            return bookContext.Books.Any(b => b.Isbn == bookIsbn);
        }

        public bool IsBookIdExist(int bookId)
        {
            return bookContext.Books.Any(b => b.Id == bookId);
        }

        public bool IsDuplicateIsbn(int bookId, string bookIsbn)
        {
            var book = bookContext.Books.Where(b => b.Isbn.Trim().ToUpper() == bookIsbn.Trim().ToUpper()
               && b.Id != bookId).FirstOrDefault(); ;

            return book == null ? false : true;
        }

        //public ICollection<Author> GetAuthorsFromBook(int bookId)
        //{
        //    return bookContext.BookAuthors.Where(b => b.BookId == bookId).Select(a => a.Author).ToList();
        //}      

        //public Book GetBookFromReviews(int reviewId)
        //{
        //    return bookContext.Reviews.Where(r => r.Id == reviewId).Select(b => b.Book).FirstOrDefault();
        //}    

        //public ICollection<Book> GetBooksFromAuthor(int authorId)
        //{
        //    return bookContext.BookAuthors.Where(a => a.AuthorId == authorId).Select(b => b.Book).ToList();
        //}

        //public ICollection<Book> GetBooksFromCategory(int categoryId)
        //{
        //    return bookContext.BookCategories.Where(c => c.CategoryId == categoryId).Select(b => b.Book).ToList();
        //}

        //public ICollection<Category> GetCategoriesFromBook(int bookId)
        //{
        //    return bookContext.BookCategories.Where(b => b.BookId == bookId).Select(c => c.Category).ToList();
        //}

        //public ICollection<Review> GetReviewsFromBook(int bookId)
        //{
        //    return bookContext.Books.Where(b => b.Id == bookId).SelectMany(r => r.Reviews).ToList();
        //}


    }
}
