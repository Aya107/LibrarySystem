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

namespace LibrarySystem.Pages.Library.Authors
{
    public class CreateModel : PageModel
    {
        private readonly AuthorServices _authorServices;

        public CreateModel(AuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuthorDTO Author { get; set; } = new AuthorDTO(); // Bind to AuthorDTO

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Call the AuthorServices to create a new author
            var createdAuthor = await _authorServices.CreateAuthorAsync(Author);

            if (createdAuthor == null)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the author.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
