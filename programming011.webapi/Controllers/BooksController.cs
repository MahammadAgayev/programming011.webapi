using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

using programming011.webapi.Entitites;
using programming011.webapi.Mappers;
using programming011.webapi.Models;

using System.Net;

namespace programming011.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        public BooksController(ILogger<BooksController> logger)
        {
            _logger = logger;
        }

        private static List<Book> _books = new List<Book>()
        {
            new Book
            {
                Id = 1,
                AuthorName = "Fyodor Dostoyevski",
                ReleaseYear = 1868,
                Title = "Idiot"
            },
            new Book
            {
                Id = 2,
                AuthorName = "Friedrich Nietzsche",
                ReleaseYear = 1886,
                Title = "Beyond Good and Evil"
            },
            new Book
            {
                Id = 3,
                AuthorName = "J.K. Rowling",
                ReleaseYear = 1998,
                Title = "Harry Potter"
            }
        };

        [HttpGet]
        public IActionResult Get()
        {
            // Log levels:
            // Trace
            // Debug
            // Info
            // Warning
            // Error
            // Critical Error (FATAL)

            _logger.LogTrace("I'M TRACE LOG");
            _logger.LogInformation("I'M INFO LOG");
            _logger.LogError("I'M ERROR LOG");
            _logger.LogCritical("I'M FATAL LOG");

            var bookModels = _books.Select(BookMapper.MapModel).ToList();

            return Ok(bookModels);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book b = _books.FirstOrDefault(x => x.Id == id);

            if (b == null)
            {
                return NotFound();
            }

            BookModel model = BookMapper.MapModel(b);

            return Ok(model);
        }

        [HttpPost]
        public IActionResult Post(BookModel model)
        {
            Book b = BookMapper.MapBook(model);

            b.Id = _books.Count() + 1;

            _books.Add(b);

            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, BookModel model)
        {
            Book book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.ReleaseYear = model.ReleaseYear;
            book.AuthorName = model.AuthorName;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Book b = _books.FirstOrDefault(b => b.Id == id);

            if (b == null)
            {
                return NotFound();
            }

            _books.Remove(b);

            return Ok();
        }
    }
}
