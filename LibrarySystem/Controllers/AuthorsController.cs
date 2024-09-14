using LibrarySystem.DTOs;
using LibrarySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly AuthorServices _authorServices;

    public AuthorsController(AuthorServices authorServices)
    {
        _authorServices = authorServices;
    }

    // GET: api/Authors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
    {
        var authors = await _authorServices.GetAllAuthorsAsync();
        return Ok(authors);
    }

    // GET: api/Authors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
    {
        var author = await _authorServices.GetAuthorByIdAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    // POST: api/Authors
    [HttpPost]
    public async Task<ActionResult<AuthorDTO>> PostAuthor(AuthorDTO authorDto)
    {
        var createdAuthor = await _authorServices.CreateAuthorAsync(authorDto);
        return CreatedAtAction(nameof(GetAuthor), new { id = createdAuthor.AuthorId }, createdAuthor);
    }

    // PUT: api/Authors/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, SimpleAuthorDTO authorDto)
    {
        authorDto.AuthorId = id;

        var updatedAuthor = await _authorServices.UpdateAuthorAsync(authorDto);

        if (updatedAuthor == null)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/Authors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var deleted = await _authorServices.DeleteAuthorAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
