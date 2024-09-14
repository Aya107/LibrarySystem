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
    public class DetailsModel : PageModel
    {
        private readonly BookServices _bookServices;

        public DetailsModel(BookServices bookServices)
        {
            _bookServices = bookServices;
        }

        public BookDTO Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Use BookServices to get book details
            var bookDto = await _bookServices.GetBookByIdAsync(id.Value);

            if (bookDto == null)
            {
                return NotFound();
            }
            else
            {
                Book = bookDto;
            }

            return Page();
        }
    }
}
