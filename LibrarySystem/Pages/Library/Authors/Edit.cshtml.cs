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

namespace LibrarySystem.Pages.Library.Authors
{
    public class EditModel : PageModel
    {
        private readonly AuthorServices _authorServices;

        public EditModel(AuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        [BindProperty]
        public SimpleAuthorDTO Author { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use AuthorServices to retrieve the author
            var authorDto = await _authorServices.GetAuthorByIdAsync(id.Value);

            if (authorDto == null)
            {
                return NotFound();
            }

            Author = new SimpleAuthorDTO
            {
                AuthorId = authorDto.AuthorId,
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName
            };
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Use AuthorServices to update the author
                var updatedAuthor = await _authorServices.UpdateAuthorAsync(Author);

                if (updatedAuthor == null)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(Author.AuthorId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private async Task<bool> AuthorExists(int id)
        {
            var author = await _authorServices.GetAuthorByIdAsync(id);
            return author != null;
        }
    }
}
