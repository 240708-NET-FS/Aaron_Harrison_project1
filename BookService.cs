using System;
using System.Collections.Generic;
using System.Linq;

namespace project1_AH;
public class BookService
{
    private readonly appDbContext _context;

    public BookService(appDbContext context)
    {
        _context = context;
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public List<BookService> GetBooks()
    {
        return _context.Books.ToList();
    }
}