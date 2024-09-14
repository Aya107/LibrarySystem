using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibrarySystem.Data;
using LibrarySystem.DTOs;
using LibrarySystem.Model;
using LibrarySystem.Services;

namespace LibrarySystem.Pages.Library.Authors.Books
{
    public class CreateModel : PageModel
    {
        private readonly BookServices _bookServices;

        public CreateModel(BookServices bookServices)
        {
            _bookServices = bookServices;
        }

        [BindProperty]
        public SimpleBookDTO Book { get; set; } = new SimpleBookDTO();

        [BindProperty(SupportsGet = true)]
        public int AuthorId { get; set; }

        public IActionResult OnGet(int? id)
        {
            AuthorId = id.Value;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            AuthorId = id.Value;
            await _bookServices.CreateBookForAuthorAsync(Book, AuthorId);
            return RedirectToPage("/Library/Authors/AuthorBooks", new { id = AuthorId });
        }
    }
}
