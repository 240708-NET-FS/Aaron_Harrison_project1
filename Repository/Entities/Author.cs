using System.ComponentModel.DataAnnotations;

namespace AaronH_project1.Entities;

public class Author
{
    public Author()
        {
            Books = new HashSet<Book>();
        }
    [Key]    
    public int AuthorID {get; set; }
    [Required]
    public string? Name { get; set; }
    
    public required ICollection<Book> Books { get; set; }
}
