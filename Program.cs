using System;
using Microsoft.Extensions.DependencyInjection;

namespace project1_AH;
class Program
{
    static void Main(string[] args)
    {
        // Setup DI and DbContext
        var serviceProvider = new ServiceCollection()
            .AddDbContext<appDbContext>()
            .AddTransient<BookService>()
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
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBook(bookService);
                    break;
                case "2":
                    // View books logic
                    break;
                case "3":
                    // Update book logic
                    break;
                case "4":
                    // Delete book logic
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
