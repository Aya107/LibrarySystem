using LibrarySystem.Model;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.DTOs;

public class AuthorDTO
{
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "The First Name field is required.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The Last Name field is required.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    public List<SimpleBookDTO> Books { get; set; } = new List<SimpleBookDTO>();
}