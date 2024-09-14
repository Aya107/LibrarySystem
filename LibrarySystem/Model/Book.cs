using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Model;

public class Book
{
    [Key]
    public int BookId { get; set; }
    
    [Required(ErrorMessage = "The Title is required.")]
    [Display(Name = "Book Title")]
    public string Title { get; set; }
    
    [Display(Name = "Date of Publication")]
    [DataType(DataType.Date)]
    public DateTime PublishedDate { get; set; }
    
    // Foreign key relationship with Author
    public int AuthorId { get; set; }

    // Navigation property
    [ForeignKey("AuthorId")]
    public Author Author { get; set; }
    
}