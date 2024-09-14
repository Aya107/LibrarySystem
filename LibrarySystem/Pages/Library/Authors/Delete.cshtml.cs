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

namespace LibrarySystem.Pages.Library.Authors
{
    public class DeleteModel : PageModel
    {
        private readonly AuthorServices _authorServices;

        public DeleteModel(AuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        [BindProperty]
        public AuthorDTO Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Fetch the author using AuthorServices
            var authorDto = await _authorServices.GetAuthorByIdAsync(id.Value);

            if (authorDto == null)
            {
                return NotFound();
            }

            Author = authorDto;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Call the DeleteAuthorAsync method from AuthorServices
            var success = await _authorServices.DeleteAuthorAsync(id.Value);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
