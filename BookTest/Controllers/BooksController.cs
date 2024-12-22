using BookTest.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public readonly BookContext _context;

        public BooksController(BookContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var books = _context.Book.ToList();
                return Ok(books);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var book = _context.Book.FirstOrDefault(b =>b.Id == id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(BookDto bookDto)
        {
            try
            {
                Book book = new Book { 
                    Author = bookDto.Author , Genre = bookDto.Genre , PublishedYear= bookDto.PublishedYear ,Title =bookDto.Title};
                await _context.Book.AddAsync(book);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> put(int id,BookDto bookDto)
        {
            try
            {
                var book =  _context.Book.FirstOrDefault(b=>b.Id == id);
                if (book != null)
                {
                    book.Author = bookDto.Author;
                    book.Genre = bookDto.Genre;
                    book.PublishedYear = bookDto.PublishedYear;
                    book.Title = bookDto.Title;
                    _context.Book.Update(book);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var book = _context.Book.FirstOrDefault(b => b.Id == id);
                if (book != null)
                {
                    _context.Book.Remove(book);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();

            }
            catch (Exception ex) { 
            return BadRequest(ex.Message);
            }
        }

    }
}
