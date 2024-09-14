using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.Model;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Services
{
    public class BookServices
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public BookServices(LibraryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        // Get all books
        public async Task<List<SimpleBookDTO>> GetAllBooksAsync(int authorId)
        {
            var books = await _context.Books.Where(b => b.AuthorId == authorId)
                .ToListAsync();
            return _mapper.Map<List<SimpleBookDTO>>(books);
        }
        
        public async Task<(List<BookDTO> Books, int TotalCount)> GetBooksPaginatedAsync(int authorId, int pageNumber, int pageSize)
        {
            var query = _context.Books
                .Where(b => b.AuthorId == authorId)
                .OrderBy(b => b.Title); // Ensure consistent ordering

            var totalCount = await query.CountAsync();

            var books = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (Books: _mapper.Map<List<BookDTO>>(books), TotalCount: totalCount);
        }

        // Get a book by its ID
        public async Task<BookDTO> GetBookByIdAsync(int bookId)
        {
            var book = await _context.Books.Include(b => b.Author)
                                           .FirstOrDefaultAsync(b => b.BookId == bookId);
            if (book == null)
            {
                return null;
            }

            return _mapper.Map<BookDTO>(book);
        }
        
        public async Task<BookDTO> CreateBookForAuthorAsync(SimpleBookDTO bookDTO, int authorId)
        {
            // Retrieve the author by ID
            var author = await _context.Authors.FindAsync(authorId);

            if (author == null)
            {
                throw new Exception($"Author with ID {authorId} not found.");
            }

            // Map the incoming SimpleBookDTO to a Book entity
            var book = _mapper.Map<Book>(bookDTO);
        
            // Assign the author to the book
            book.AuthorId = authorId;

            // Add and save the new book
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            // Return the created book as a BookDTO
            return _mapper.Map<BookDTO>(book);
        }
        
        public async Task<BookDTO> UpdateBookForAuthorAsync(SimpleBookDTO bookDTO, int authorId)
        {
            // Retrieve the book by ID
            var book = await _context.Books.FindAsync(bookDTO.BookId);

            if (book == null)
            {
                throw new Exception($"Book with ID {bookDTO.BookId} not found.");
            }

            // Ensure the book belongs to the correct author
            if (book.AuthorId != authorId)
            {
                throw new Exception($"The book does not belong to author with ID {authorId}.");
            }

            // Map the updated SimpleBookDTO data to the existing book
            _mapper.Map(bookDTO, book); // This will update the book with the DTO data

            // Save the changes
            await _context.SaveChangesAsync();

            // Return the updated book as a BookDTO
            return _mapper.Map<BookDTO>(book);
        }
        
        // Delete a book by its ID
        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return true;
        }
        
        public async Task<int> GetBooksCountAsync()
        {
            return await _context.Authors.CountAsync();
        }
    }
}
