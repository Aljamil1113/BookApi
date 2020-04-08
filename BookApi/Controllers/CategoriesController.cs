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
    public class CategoriesController : Controller
    {
        private ICategoryRepository categoryRepository { get; set; }
        public IBookRepository bookRepository { get; set; }

        public CategoriesController(ICategoryRepository _categoryRepository, IBookRepository _bookRepository)
        {
            categoryRepository = _categoryRepository;
            bookRepository = _bookRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // api/categories

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = categoryRepository.GetCategories().ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoryDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoryDtos);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategory(int categoryId)
        {
            if (!categoryRepository.CategoryExist(categoryId))
                return NotFound();

            var category = categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name
            };

            return Ok(categoryDto);
        }


        [HttpGet("books/{bookId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategoriesOfABook(int bookId)
        {
            if (!bookRepository.IsBookIdExist(bookId))
                return NotFound();
       
            var categories = categoryRepository.GetCategoriesOfABook(bookId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoryDtos.Add(new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                });
            }

            return Ok(categoryDtos);
        }

        //BOOKS

        [HttpGet("{categoryId}/books")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public IActionResult GetAllBooksCategory(int categoryId)
        {
            if (!categoryRepository.CategoryExist(categoryId))
                return NotFound();

            var books = categoryRepository.GetAllBooksForCategory(categoryId).ToList();

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
    }
}