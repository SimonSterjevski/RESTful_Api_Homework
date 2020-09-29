using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            try
            {
                return StaticDb.Books;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("queryString")]
        public ActionResult<Book> GetOneBookByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request!");
                }
                if (index >= StaticDb.Books.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Book not found!");
                }
                return StatusCode(StatusCodes.Status200OK, StaticDb.Books[index]);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("queryParams")]
        public ActionResult<Book> GetOneBookByParams(string author, string title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(title))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad Request! No parameters!");
                }
                if (string.IsNullOrEmpty(author))
                {
                    Book book1 = StaticDb.Books.FirstOrDefault(x => x.Title == title);
                    if (book1 == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No such book found!");
                    }
                    return StatusCode(StatusCodes.Status200OK, book1);
                }
                if (string.IsNullOrEmpty(title))
                {
                    Book book2 = StaticDb.Books.FirstOrDefault(x => x.Author == author);
                    if (book2 == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "No such book found!");
                    }
                    return StatusCode(StatusCodes.Status200OK, book2);
                }
                Book book3 = StaticDb.Books.FirstOrDefault(x => x.Author == author && x.Title == title);
                if (book3 == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No such book found!");
                }
                return StatusCode(StatusCodes.Status200OK, book3);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("contentType")]
        public IActionResult GetContentType([FromHeader(Name = "Content-Type")] string contentType)
        {
            try
            {
               
                return StatusCode(StatusCodes.Status200OK, contentType);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, $"Book {book.Title} added to list!");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }


        [HttpPost("postReturn")]
        public IActionResult ReturnBookTitles([FromBody] List<Book> books)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, books.Select(x => x.Title));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }
    }
}
