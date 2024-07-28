
namespace AaronH_project1.Entities
{
    public class Book
    {
        public int BookID { get; set; }
        public required string Title { get; set; }
        public string? Genre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? Rating { get; set; }
        public string? Notes { get; set; }

        // Foreign key and navigation property for Author
        public int AuthorID { get; set; }
        public Author? Author { get; set; }
    }



}
