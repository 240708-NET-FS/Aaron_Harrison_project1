// ApplicationDbContext.cs file

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace AaronH_project1.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public ApplicationDbContext(){}

    public DbSet<Book> Books {get;set;}
    public DbSet<Author> Authors {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            // Using the builder design pattern
            // We create an object of builder, and then use methods to build out the object to the type we want and then run a build method
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                .SetBasePath(Directory.GetCurrentDirectory())
                                                .AddJsonFile("appsettings.json")
                                                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // LINQ
        // Line Integrated Query
        // Lambda Functions

        /*
            This is what is helping the AppDbContext know how to create the entities themselves, this ensures the relationships between entities are clear.
        */
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorID);
    }
}