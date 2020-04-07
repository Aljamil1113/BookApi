using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Dtos;
using BookApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private IBookRepository bookRepository { get; set; }

        public BooksController(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        // api/books
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public IActionResult GetBooks()
        {
            var books = bookRepository.GetBooks().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Title = book.Title,
                    DatePublish = book.DatePublish
                });
            }

            return Ok(bookDtos);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        public IActionResult GetBook(int bookId)
        {
            if (!bookRepository.IsBookIdExist(bookId))
                return NotFound();

            var book = bookRepository.GetBook(bookId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDto = new BookDto()
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                DatePublish = book.DatePublish
            };

            return Ok(bookDto);
        }

        [HttpGet("reviews/{reviewId}/book")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(BookDto))]
        public IActionResult GetBookFromReviews(int reviewId)
        {
            var book = bookRepository.GetBookFromReviews(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDto = new BookDto()
            {
                Id = book.Id,
                Isbn = book.Isbn,
                Title = book.Title,
                DatePublish = book.DatePublish
            };

            return Ok(bookDto);
        }

        [HttpGet("{bookId}/reviews")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        public IActionResult GetReviewsFromBook(int bookId)
        {

            if (!bookRepository.IsBookIdExist(bookId))
                return NotFound();

            var reviews = bookRepository.GetReviewsFromBook(bookId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewDtos = new List<ReviewDto>();

            foreach (var review in reviews)
            {
                reviewDtos.Add(new ReviewDto
                {
                    Id = review.Id,
                    HeadLine = review.HeadLine,
                    Rating = review.Rating,
                    ReviewText = review.ReviewText
                });
            }

            return Ok(reviewDtos);
        }

        [HttpGet("authors/{authorId}/books")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public IActionResult GetBooksFromAuthor(int authorId)
        {

            var books = bookRepository.GetBooksFromAuthor(authorId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Title = book.Title,
                    DatePublish = book.DatePublish
                });
            }

            return Ok(bookDtos);
        }

        [HttpGet("{bookId}/authors")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        public IActionResult GetAuthorsFromBook(int bookId)
        {

            if (!bookRepository.IsBookIdExist(bookId))
                return NotFound();

            var authors = bookRepository.GetAuthorsFromBook(bookId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorDtos = new List<AuthorDto>();

            foreach (var author in authors)
            {
                authorDtos.Add(new AuthorDto
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                });
            }

            return Ok(authorDtos);
        }

        [HttpGet("category/{categoryId}/books")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public IActionResult GetBooksFromCategory(int categoryId)
        {

            var books = bookRepository.GetBooksFromCategory(categoryId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    Title = book.Title,
                    DatePublish = book.DatePublish
                });
            }

            return Ok(bookDtos);
        }

        [HttpGet("{bookId}/categories")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategoriesFromBook(int bookId)
        {

            if (!bookRepository.IsBookIdExist(bookId))
                return NotFound();

            var categories = bookRepository.GetCategoriesFromBook(bookId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoriesDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoriesDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoriesDtos);
        }
    }
}