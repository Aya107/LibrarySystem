using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Model;

public class Author
{
    [Key]
    public int AuthorId { get; set; }
    [Required(ErrorMessage = "The First Name field is required.")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "The Last Name field is required.")]
    public string LastName { get; set; }
    public List<Book> Books { get; set; } = new List<Book>();
}