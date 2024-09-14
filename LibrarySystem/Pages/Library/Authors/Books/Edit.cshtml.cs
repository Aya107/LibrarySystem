using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.Model;
using LibrarySystem.Services;

namespace LibrarySystem.Pages.Library.Authors.Books
{
    public class EditModel : PageModel
    {
        private readonly BookServices _bookServices;

        public EditModel(BookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [BindProperty] public SimpleBookDTO Book { get; set; } = new SimpleBookDTO();

        [BindProperty(SupportsGet = true)] public int BookId { get; set; }

        [BindProperty(SupportsGet = true)] public int AuthorId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? authorId)
        {
            BookId = id.Value;
            AuthorId = authorId.Value;

            var bookDTO = await _bookServices.GetBookByIdAsync(id.Value);

            if (bookDTO == null || bookDTO.AuthorId != authorId.Value)
            {
                return NotFound($"Book with ID {id} not found for author {authorId}.");
            }

            Book = new SimpleBookDTO
            {
                BookId   = bookDTO.BookId,
                Title = bookDTO.Title,
                PublishedDate = bookDTO.PublishedDate
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _bookServices.UpdateBookForAuthorAsync(Book, AuthorId);

            return RedirectToPage("/Library/Authors/AuthorBooks", new { id = AuthorId });
        }
    }
}
