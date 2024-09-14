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
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Pages.Library.Authors
{
    public class AuthorBooksModel : PageModel
    {
        private readonly AuthorServices _authorServices;
        private readonly BookServices _bookServices;

        public AuthorBooksModel(AuthorServices authorServices, BookServices bookServices)
        {
            _authorServices = authorServices;
            _bookServices = bookServices;
        }

        public AuthorDTO Author { get; set; } 
        public IList<BookDTO> Books { get; set; } = new List<BookDTO>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        

        public async Task<IActionResult> OnGetAsync(int? id, int? pageNumber)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            // Use AuthorServices to get author details
            Author = await _authorServices.GetAuthorByIdAsync(id.Value);

            if (Author == null)
            {
                return NotFound();
            }
            
            const int pageSize = 1; // Define how many items you want per page
            CurrentPage = pageNumber ?? 1; // Set the current page, default is 1
            
            var result = await _bookServices.GetBooksPaginatedAsync(id.Value, CurrentPage, pageSize);

            Books = result.Books;
            var totalItems = result.TotalCount;
            TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return Page();
        }
    }
}
