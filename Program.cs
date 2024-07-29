using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AaronH_project1.Entities;

namespace AaronH_project1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlServer("DefaultConnection"))
                .AddScoped<BookService>()
                .BuildServiceProvider();

            var bookService = serviceProvider.GetService<BookService>();

            // Main menu loop
            while (true)
            {
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View Books");
                Console.WriteLine("3. Update Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("5. Exit");
                var choice = Console.ReadLine() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        AddBook(bookService);
                        break;
                    case "2":
                        ViewBooks(bookService);
                        break;
                    case "3":
                        UpdateBook(bookService);
                        break;
                    case "4":
                        DeleteBook(bookService);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public static void AddBook(BookService bookService)
        {
            if (bookService == null)
            {
                Console.WriteLine("BookService is not available.");
                return;
            }

            Console.Write("Enter book title: ");
            string title = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter author's name: ");
            string authorName = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter book genre: ");
            string genre = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter start date (yyyy-mm-dd): ");
            DateTime startDate;
            while (!DateTime.TryParse(Console.ReadLine(), out startDate))
            {
                Console.Write("Invalid date format. Enter start date (yyyy-mm-dd): ");
            }

            Console.Write("Enter end date (yyyy-mm-dd): ");
            DateTime endDate;
            while (!DateTime.TryParse(Console.ReadLine(), out endDate))
            {
                Console.Write("Invalid date format. Enter end date (yyyy-mm-dd): ");
            }

            Console.Write("Enter book rating (1-5, optional): ");
            int? rating = null;
            var ratingInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ratingInput) && int.TryParse(ratingInput, out var parsedRating))
            {
                rating = parsedRating;
            }

            Console.Write("Enter notes (optional): ");
            string notes = Console.ReadLine() ?? string.Empty;

            var author = bookService.GetOrCreateAuthor(authorName);

            var book = new Book
            {
                Title = title,
                AuthorID = author.AuthorID,
                Genre = genre,
                StartDate = startDate,
                EndDate = endDate,
                Rating = rating,
                Notes = notes
            };

            bookService.AddBook(book);
            Console.WriteLine("Book added successfully!");
        }

        public static void ViewBooks(BookService bookService)
        {
            if (bookService == null)
            {
                Console.WriteLine("BookService is not available.");
                return;
            }

            var books = bookService.GetBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"ID: {book.BookID}, Title: {book.Title}, Author: {book.Author?.Name}, Genre: {book.Genre}, Start Date: {book.StartDate.ToShortDateString()}, End Date: {book.EndDate.ToShortDateString()}, Rating: {book.Rating}, Notes: {book.Notes}");
                }
            }
        }

        public static void UpdateBook(BookService bookService)
        {
            if (bookService == null)
            {
                Console.WriteLine("BookService is not available.");
                return;
            }

            Console.Write("Enter the ID of the book to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var book = bookService.GetBookById(id);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            Console.WriteLine("Leave a field empty to keep its current value.");

            Console.Write($"Enter new title (current: {book.Title}): ");
            string title = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(title)) book.Title = title;

            Console.Write($"Enter new author's name (current: {book.Author?.Name}): ");
            string authorName = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(authorName))
            {
                var author = bookService.GetOrCreateAuthor(authorName);
                book.AuthorID = author.AuthorID;
            }

            Console.Write($"Enter new genre (current: {book.Genre}): ");
            string genre = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(genre)) book.Genre = genre;

            Console.Write($"Enter new start date (yyyy-mm-dd) (current: {book.StartDate.ToShortDateString()}): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate)) book.StartDate = startDate;

            Console.Write($"Enter new end date (yyyy-mm-dd) (current: {book.EndDate.ToShortDateString()}): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate)) book.EndDate = endDate;

            Console.Write($"Enter new rating (1-5) (current: {book.Rating}): ");
            if (int.TryParse(Console.ReadLine(), out int rating)) book.Rating = rating;

            Console.Write($"Enter new notes (current: {book.Notes}): ");
            string notes = Console.ReadLine() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(notes)) book.Notes = notes;

            bookService.UpdateBook(book);
            Console.WriteLine("Book updated successfully!");
        }

        public static void DeleteBook(BookService bookService)
        {
            if (bookService == null)
            {
                Console.WriteLine("BookService is not available.");
                return;
            }

            Console.Write("Enter the ID of the book to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var book = bookService.GetBookById(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            bookService.DeleteBook(bookId);
            Console.WriteLine("Book deleted successfully!");
        }
    }
}
