using Microsoft.EntityFrameworkCore;

namespace project1_AH;

public class appDbContext : DbContext
{
    public DbSet<Book> Books {get;set;}

    
    

}
