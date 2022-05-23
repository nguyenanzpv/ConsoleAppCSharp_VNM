using SolidEdu.Shared; // SolidStore, Category, Product
using Microsoft.EntityFrameworkCore; // DbSet<T>
using static System.Console;FilterAndSort();
JoinCategoriesAndProducts();
GroupJoinCategoriesAndProducts();
AggregateProducts();

static void FilterAndSort()
{
    using (SolidStore db = new())
    {
        DbSet<Product> allProducts = db.Products;//using Entity Framework get all data

        IQueryable<Product> filteredProducts =
        allProducts.Where(product => product.UnitPrice < 10M);

        IOrderedQueryable<Product> sortedAndFilteredProducts =
        filteredProducts.OrderByDescending(product => product.UnitPrice);

        var projectedProducts = sortedAndFilteredProducts
             .Select(product => new // anonymous type-> select new field -> objetc initialized
             {
                 product.ProductId,
                 product.ProductName,
                 product.UnitPrice
             });

        WriteLine("Products that cost less than $10:");

        //foreach (Product p in sortedAndFilteredProducts)
        //{
        //    WriteLine("{0}: {1} costs {2:$#,##0.00}",
        //    p.ProductId, p.ProductName, p.UnitPrice);
        //}

        foreach (var p in projectedProducts) //ko biet projectedProducts kieu gi nen dung var
        {
            WriteLine("{0}: {1} costs {2:$#,##0.00}",
            p.ProductId, p.ProductName, p.UnitPrice);
        }
        WriteLine();
    }
}//inner join on foreigen keystatic void JoinCategoriesAndProducts()
{
    using (SolidStore db = new())
    {
        // join every product to its category to return 77 matches
        var queryJoin = db.Categories.Join(
        inner: db.Products,
        outerKeySelector: category => category.CategoryId,
        innerKeySelector: product => product.CategoryId,
        resultSelector: (c, p) => new { c.CategoryName, p.ProductName, p.ProductId });
        foreach (var item in queryJoin)
        {
            WriteLine("{0}: {1} is in {2}.",
            arg0: item.ProductId,
            arg1: item.ProductName,
            arg2: item.CategoryName);
        }
    }
}//Leftouter joinstatic void GroupJoinCategoriesAndProducts()
{
    using (SolidStore db = new())
    {
        // group all products by their category to return 8 matches
        var queryGroup = db.Categories.AsEnumerable().GroupJoin(//AsEnumerable de co the lap duoc
        inner: db.Products,
        outerKeySelector: category => category.CategoryId,
        innerKeySelector: product => product.CategoryId,
        resultSelector: (c, matchingProducts) => new
        {
            c.CategoryName,
            Products = matchingProducts.OrderBy(p => p.ProductName)
        });

        foreach (var category in queryGroup)
        {
            WriteLine("{0} has {1} products.",
            arg0: category.CategoryName,
            arg1: category.Products.Count());
            foreach (var product in category.Products)
            {
                WriteLine($" {product.ProductName}");
            }
        }
    }
}static void AggregateProducts()
{
    using (SolidStore db = new())
    {
        WriteLine("{0,-25} {1,10}",
        arg0: "Product count:",
        arg1: db.Products.Count());
        WriteLine("{0,-25} {1,10:$#,##0.00}",
        arg0: "Highest product price:",
        arg1: db.Products.Max(p => p.UnitPrice));
        WriteLine("{0,-25} {1,10:N0}",
        arg0: "Sum of units in stock:",
        arg1: db.Products.Sum(p => p.UnitsInStock));
        WriteLine("{0,-25} {1,10:N0}",
        arg0: "Sum of units on order:",
        arg1: db.Products.Sum(p => p.UnitsOnOrder));
        WriteLine("{0,-25} {1,10:$#,##0.00}",
        arg0: "Average unit price:",
        arg1: db.Products.Average(p => p.UnitPrice));
        WriteLine("{0,-25} {1,10:$#,##0.00}",
        arg0: "Value of units in stock:",
        arg1: db.Products
        .Sum(p => p.UnitPrice * p.UnitsInStock));
    }
}