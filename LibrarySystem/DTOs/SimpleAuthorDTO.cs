using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.DTOs;

public class SimpleAuthorDTO
{
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "The First Name field is required.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The Last Name field is required.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}