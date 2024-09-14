using LibrarySystem.DTOs;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[Route("api/Authors/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookServices _bookServices;

    public BooksController(BookServices bookServices)
    {
        _bookServices = bookServices;
    }

    // GET: api/Authors/Books/5     //authorId
    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks(int id)
    {
        var books = await _bookServices.GetAllBooksAsync(id);
        return Ok(books);
    }

    // // GET: api/Authors/Books/1     //bookId
    // [HttpGet("{id}")]
    // public async Task<ActionResult<BookDTO>> GetBook(int id)
    // {
    //     var book = await _bookServices.GetBookByIdAsync(id);
    //
    //     if (book == null)
    //     {
    //         return NotFound();
    //     }
    //
    //     return Ok(book);
    // }

    // POST: api/Authors/Books/5     //authorId
    [HttpPost("{id}")]
    public async Task<IActionResult> PostBook(SimpleBookDTO bookDto, int id)
    {
        var createdBook = await _bookServices.CreateBookForAuthorAsync(bookDto, id);
        if (createdBook == null)
        {
            return BadRequest("Failed to create the book for the specified author.");
        }
        // return CreatedAtAction(nameof(GetBook), new { id = createdBook.BookId }, createdBook);
        return Ok("Book created successfully.");
    }

    // PUT: api/Authors/Books/5     //authorId
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(SimpleBookDTO bookDto, int id)
    {
        var updatedBook = await _bookServices.UpdateBookForAuthorAsync(bookDto, id);

        if (updatedBook == null)
        {
            return NotFound("Book not found for the specified author.");
        }

        return NoContent();
    }

    // DELETE: api/Authors/Books/1     //bookId
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookServices.DeleteBookAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
