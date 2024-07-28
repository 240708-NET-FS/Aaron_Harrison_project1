// BookService.cs

using Microsoft.EntityFrameworkCore;

namespace AaronH_project1.Entities
{
    public class BookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public List<Book> GetBooks()
        {
            return _context.Books.Include(b => b.Author).ToList();
        }

        public Book? GetBookById(int id)
        {
            return _context.Books.Include(b => b.Author).FirstOrDefault(b => b.BookID == id);
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }

        public Author GetOrCreateAuthor(string authorName)
        {
            var author = _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Name == authorName);
            if (author == null)
            {
                author = new Author { Name = authorName, Books = new List<Book>() };
                _context.Authors.Add(author);
                _context.SaveChanges();
            }
            return author;
        }
    }
}
