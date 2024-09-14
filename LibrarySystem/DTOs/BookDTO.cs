using LibrarySystem.Model;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.DTOs;

public class BookDTO
{
    public int BookId { get; set; }

    [Required(ErrorMessage = "The Title is required.")]
    [Display(Name = "Book Title")]
    public string Title { get; set; }

    [Display(Name = "Date of Publication")]
    [DataType(DataType.Date)]
    public DateTime PublishedDate { get; set; }

    public int AuthorId { get; set; }

    // Include basic author information if needed
    public AuthorDTO Author { get; set; }
}