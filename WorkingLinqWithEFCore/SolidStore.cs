using Microsoft.EntityFrameworkCore;// DbContext, DbSet<T>

namespace SolidEdu.Shared;

// this manages the connection to the database 
public class SolidStore : DbContext
{
    // these properties map to tables in the database 
    public DbSet<Category>? Categories { get; set; } //lay ra tat ca data trong table Categories
    public DbSet<Product>? Products { get; set; } //lay ra tat ca data trong table Products
    protected override void OnConfiguring(
    DbContextOptionsBuilder optionsBuilder)
    {
        string connection = "Data Source=.;" +
        "Initial Catalog=SolidStore;" +
        "Integrated Security=true;" +
        "MultipleActiveResultSets=true;";
        optionsBuilder.UseSqlServer(connection);
    }
    protected override void OnModelCreating(
    ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
        .Property(product => product.UnitPrice)
        .HasConversion<double>();//convert decimal ve double
    }
}
