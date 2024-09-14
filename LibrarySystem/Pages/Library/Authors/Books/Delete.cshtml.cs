using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.Model;
using LibrarySystem.Services;

namespace LibrarySystem.Pages.Library.Authors.Books
{
    public class DeleteModel : PageModel
    {
        private readonly BookServices _bookServices;

        public DeleteModel(BookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [BindProperty]
        public BookDTO Book { get; set; } = new BookDTO();
        
        [BindProperty(SupportsGet = true)]
        public int AuthorId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use BookServices to get the book details
            var bookDto = await _bookServices.GetBookByIdAsync(id.Value);

            if (bookDto == null)
            {
                return NotFound();
            }
            else
            {
                Book = bookDto;
                AuthorId = Book.AuthorId;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            // Use BookServices to delete the book
            var success = await _bookServices.DeleteBookAsync(id.Value);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("/Library/Authors/AuthorBooks", new { id = AuthorId });
        }
    }
}
