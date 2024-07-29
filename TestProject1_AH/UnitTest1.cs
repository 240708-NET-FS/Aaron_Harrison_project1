using Xunit;
using Moq;
using AaronH_project1.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestProject1_AH
{
    public class UnitTest1
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BookTrackerDB")
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void AddBook_ShouldAddBookToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var bookService = new BookService(context);
            var book = new Book
            {
                Title = "Test Book",
                Author = new Author 
                { 
                    Name = "Test Author"
                },
                Genre = "Test Genre",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(1),
                Rating = 5,
                Notes = "Test Notes"
            };

            // Act
            bookService.AddBook(book);
            context.SaveChanges();

            // Assert
            var addedBook = context.Books.Include(b => b.Author).FirstOrDefault(b => b.Title == "Test Book");
            Assert.NotNull(addedBook);
            Assert.NotNull(addedBook?.Author); // Check if Author is not null

            if (addedBook != null && addedBook.Author != null)
            {
                Assert.Equal("Test Author", addedBook.Author.Name);
                Assert.Equal("Test Genre", addedBook.Genre);
                Assert.Equal(5, addedBook.Rating);
            }
        }

        [Fact]
        public void AddBook_WithMinimalDetails_ShouldAddBookToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var bookService = new BookService(context);
            var book = new Book
            {
                Title = "Minimal Book",
                Author = new Author 
                { 
                    Name = "Minimal Author"
                }
            };

            // Act
            bookService.AddBook(book);
            context.SaveChanges();

            // Assert
            var addedBook = context.Books.Include(b => b.Author).FirstOrDefault(b => b.Title == "Minimal Book");
            Assert.NotNull(addedBook);
            Assert.NotNull(addedBook?.Author); // Check if Author is not null

            if (addedBook != null && addedBook.Author != null)
            {
                Assert.Equal("Minimal Author", addedBook.Author.Name);
                Assert.Null(addedBook.Genre);
                Assert.Null(addedBook.Rating);
            }
        }

        [Fact]
        public void AddBook_WithMissingOptionalDetails_ShouldAddBookToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var bookService = new BookService(context);
            var book = new Book
            {
                Title = "Missing Details Book",
                Author = new Author 
                { 
                    Name = "Author With Missing Details"
                },
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(1)
            };

            // Act
            bookService.AddBook(book);
            context.SaveChanges();

            // Assert
            var addedBook = context.Books.Include(b => b.Author).FirstOrDefault(b => b.Title == "Missing Details Book");
            Assert.NotNull(addedBook);
            Assert.NotNull(addedBook?.Author); // Check if Author is not null

            if (addedBook != null && addedBook.Author != null)
            {
                Assert.Equal("Author With Missing Details", addedBook.Author.Name);
                Assert.Null(addedBook.Genre);
                Assert.Null(addedBook.Rating);
                Assert.Null(addedBook.Notes);
            }
        }

        [Fact]
        public void AddMultipleBooks_ShouldAddBooksToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var bookService = new BookService(context);
            var firstBook = new Book
            {
                Title = "First Book",
                Author = new Author 
                { 
                    Name = "Author One"
                }
            };

            var secondBook = new Book
            {
                Title = "Second Book",
                Author = new Author 
                { 
                    Name = "Author Two"
                }
            };

            // Act
            bookService.AddBook(firstBook);
            bookService.AddBook(secondBook);
            context.SaveChanges();

            // Assert
            var addedFirstBook = context.Books.Include(b => b.Author).FirstOrDefault(b => b.Title == "First Book");
            var addedSecondBook = context.Books.Include(b => b.Author).FirstOrDefault(b => b.Title == "Second Book");

            Assert.NotNull(addedFirstBook);
            Assert.NotNull(addedSecondBook);
            Assert.NotNull(addedFirstBook?.Author);
            Assert.NotNull(addedSecondBook?.Author);

            if (addedFirstBook != null && addedFirstBook.Author != null)
            {
                Assert.Equal("Author One", addedFirstBook.Author.Name);
            }

            if (addedSecondBook != null && addedSecondBook.Author != null)
            {
                Assert.Equal("Author Two", addedSecondBook.Author.Name);
            }
        }

        [Fact]
        public void AddBook_WithDifferentAuthor_ShouldAddBookToDatabase()
        {
            // Arrange
            var context = GetDbContext();
            var bookService = new BookService(context);
            var book = new Book
            {
                Title = "Different Author Book",
                Author = new Author 
                { 
                    Name = "Different Author"
                },
                Genre = "Different Genre",
                StartDate = System.DateTime.Now,
                EndDate = System.DateTime.Now.AddDays(1),
                Rating = 4,
                Notes = "Different Notes"
            };

            // Act
            bookService.AddBook(book);
            context.SaveChanges();

            // Assert
            var addedBook = context.Books.Include(b => b.Author).FirstOrDefault(b => b.Title == "Different Author Book");
            Assert.NotNull(addedBook);
            Assert.NotNull(addedBook?.Author); // Check if Author is not null

            if (addedBook != null && addedBook.Author != null)
            {
                Assert.Equal("Different Author", addedBook.Author.Name);
                Assert.Equal("Different Genre", addedBook.Genre);
                Assert.Equal(4, addedBook.Rating);
            }
        }
    }
}
