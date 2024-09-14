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
    public class DetailsModel : PageModel
    {
        private readonly AuthorServices _authorServices;

        public DetailsModel(AuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        public AuthorDTO Author { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use AuthorServices to get the author details
            var authorDto = await _authorServices.GetAuthorByIdAsync(id.Value);

            if (authorDto == null)
            {
                return NotFound();
            }

            Author = authorDto;
            return Page();
        }
    }
}
