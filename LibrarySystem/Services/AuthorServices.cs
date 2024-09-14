using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services;

public class AuthorServices
{
    private readonly LibraryDbContext _context;
    private readonly IMapper _mapper;

    public AuthorServices(LibraryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // READ all
    public async Task<List<SimpleAuthorDTO>> GetAllAuthorsAsync()
    {
        var authors = await _context.Authors
            .ToListAsync();

        return _mapper.Map<List<SimpleAuthorDTO>>(authors); // Map List of Authors to List of AuthorDTOs
    }
    
    // READ by ID
    public async Task<AuthorDTO> GetAuthorByIdAsync(int id)
    {
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.AuthorId == id);

        if (author == null) return null;

        return _mapper.Map<AuthorDTO>(author); // Map Author to AuthorDTO
    }
    
    public async Task<List<SimpleAuthorDTO>> GetAuthorsPaginatedAsync(int pageNumber, int pageSize)
    {
        var authors = await _context.Authors
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return _mapper.Map<List<SimpleAuthorDTO>>(authors);
    }

    // CREATE
    public async Task<AuthorDTO> CreateAuthorAsync(AuthorDTO authorDto)
    {
        var author = _mapper.Map<Author>(authorDto); // Map AuthorDTO to Author
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return _mapper.Map<AuthorDTO>(author); // Return the newly created AuthorDTO
    }

    // UPDATE
    public async Task<SimpleAuthorDTO> UpdateAuthorAsync(SimpleAuthorDTO simpleAuthorDto)
    {
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.AuthorId == simpleAuthorDto.AuthorId);

        if (author == null) return null;

        // Map the fields from AuthorUpdateDTO to Author
        _mapper.Map(simpleAuthorDto, author);

        await _context.SaveChangesAsync();

        return _mapper.Map<SimpleAuthorDTO>(author);
    }

    // DELETE
    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null) return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<int> GetAuthorsCountAsync()
    {
        return await _context.Authors.CountAsync();
    }
}
