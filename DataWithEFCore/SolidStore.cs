using Microsoft.EntityFrameworkCore; // DbContext, DbContextOptionsBuilder
using static System.Console;
namespace SolidEdu.Shared;

// this manages the connection to the database
public class SolidStore : DbContext //dai dien cho 1 session lam viec voi db
{
    // these properties map to tables in the database 
    public DbSet<Category>? Categories { get; set; } //toan bo du lieu o bang categories
    public DbSet<Product>? Products { get; set; } //toan bo du lieu o bang products

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (ProjectConstants.DatabaseProvider == "SQLServer")
        {
            string connection = "Data Source=.;" +
            "Initial Catalog=SolidStore;" +
            "Integrated Security=true;" +
            "MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connection);
        }
        else
        {
            //ket noi toi db khac
        }
        optionsBuilder.UseLazyLoadingProxies();//cau hinh load lazy bang proxies

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Cac ban doc ky noi dung nhe (:
        // example of using Fluent API instead of attributes
        // to limit the length of a category name to under 15
        modelBuilder.Entity<Category>()
        .Property(category => category.CategoryName)
        .IsRequired() // NOT NULL
        .HasMaxLength(15);
        // global filter to remove discontinued products
        modelBuilder.Entity<Product>()
            .HasQueryFilter(p => !p.Discontinued);
    }

}