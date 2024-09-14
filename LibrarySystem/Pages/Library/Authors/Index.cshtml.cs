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
    public class IndexModel : PageModel
    {
        private readonly AuthorServices _authorServices;

        public IndexModel(AuthorServices authorServices)
        {
            _authorServices = authorServices;
        }

        public IList<SimpleAuthorDTO> Authors { get; set; } = new List<SimpleAuthorDTO>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task OnGetAsync(int? pageNumber)
        {
            // Use AuthorServices to fetch the list of authors
            // Author = await _authorServices.GetAllAuthorsAsync();
            const int pageSize = 1; // Define how many items you want per page
            CurrentPage = pageNumber ?? 1; // Set the current page, default is 1

            var totalAuthors = await _authorServices.GetAuthorsCountAsync(); // Create a method to get total count
            TotalPages = (int)Math.Ceiling(totalAuthors / (double)pageSize);

            Authors = await _authorServices.GetAuthorsPaginatedAsync(CurrentPage, pageSize);
        }
    }
}
