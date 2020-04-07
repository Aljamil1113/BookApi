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
    public class AuthorsController : Controller
    {
        public IAuthorRepository authorRepository { get; set; }

        public AuthorsController(IAuthorRepository _authorRepository)
        {
            authorRepository = _authorRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        //api/authors

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        public IActionResult GetAuthors()
        {
            var authors = authorRepository.GetAuthors().ToList();

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

        [HttpGet("{authorId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(AuthorDto))]
        public IActionResult GetAuthor(int authorId)
        {
            if (!authorRepository.IsAuthorIdExist(authorId))
                return NotFound();

            var author = authorRepository.GetAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var authorDto = new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            return Ok(authorDto);
        }

        [HttpGet("country/{countryId}/authors")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        public IActionResult GetAuthorsFromCountry(int countryId)
        {

            var authors = authorRepository.GetAuthorsFromCountry(countryId).ToList();

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

        [HttpGet("books/{bookId}/authors")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        public IActionResult GetAuthorFromBooks(int bookId)
        {
            var authors = authorRepository.GetAuthorsFromBook(bookId).ToList();

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

        [HttpGet("{authorId}/country")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        public IActionResult GetCountryFromAuthor(int authorId)
        {
            if (!authorRepository.IsAuthorIdExist(authorId))
                return NotFound();

            var country = authorRepository.GetCountryFromAuthor(authorId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryDto = new CountryDto()
            {
                Id = country.Id,
                Name = country.Name
            };

            

            return Ok(countryDto);
        }

        [HttpGet("{authorId}/books")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookDto>))]
        public IActionResult GetBooksFromAuthor(int authorId)
        {
            if (!authorRepository.IsAuthorIdExist(authorId))
                return NotFound();

            var books = authorRepository.GetBooksFromAuthor(authorId).ToList();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bookDtos = new List<BookDto>();

            foreach (var book in books)
            {
                bookDtos.Add(new BookDto
                {
                    Id = book.Id,
                    Isbn = book.Isbn,
                    DatePublish = book.DatePublish,
                    Title = book.Title
                });
            }

            return Ok(bookDtos);
        }
    }
}