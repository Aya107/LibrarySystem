using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.DTOs;

public class SimpleBookDTO
{
    public int BookId { get; set; }

    [Required(ErrorMessage = "The Title is required.")]
    [Display(Name = "Book Title")]
    public string Title { get; set; }

    [Display(Name = "Date of Publication")]
    [DataType(DataType.Date)]
    public DateTime PublishedDate { get; set; }
}