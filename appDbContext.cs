using Microsoft.EntityFrameworkCore;

namespace project1_AH;

public class appDbContext : DbContext
{
    public DbSet<Book> Books {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=dotnet2fsai2024.database.windows.net;Database=BookTrackerDB;User Id=AHarr_Revature;Password=Ts2tOE(D+H;q;");
    }
    

}