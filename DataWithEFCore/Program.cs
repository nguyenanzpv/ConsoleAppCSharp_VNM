using SolidEdu.Shared;
using static System.Console;
using Microsoft.EntityFrameworkCore; // Include extension method
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry; using Exlecipt load
WriteLine($"Using {ProjectConstants.DatabaseProvider} database provider.");
//QueryingCategories();
//FilteredIncludes();
//QueryingProducts();
//add product
if (AddProduct(categoryId: 6,
productName: "Bob's Burgers", price: 500M))
{
    WriteLine("Add product successful.");
}
//update product
if (IncreaseProductPrice(productNameStartsWith: "Bob", amount: 20M))
{
    WriteLine("Update product price successful.");
}
//delete product
int deleted = DeleteProducts(productNameStartsWith: "Bob");
WriteLine($"{deleted} product(s) were deleted.");
ListProducts();

//query in ef core
static void QueryingCategories()
{
    using (SolidStore db = new())
    {
        WriteLine("Categories and how many products they have:");
        // a query to get all categories and their related products
        IQueryable<Category>? categories = db.Categories?
        .Include(c => c.Products); //eager load -> table lon se giam performance ; neu dung lazy ko can Include van load duoc c.Products lien quan
        if (categories is null)
        {
            WriteLine("No categories found.");
            return;
        }
        // execute query and enumerate results
        foreach (Category c in categories)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
            foreach(Product p in c.Products)
            {
                WriteLine($"Product-Id: {p.ProductId} - Product Name:{p.ProductName}");
            }
        }
    }
}
//filter in ef core
static void FilteredIncludes()
{
    using (SolidStore db = new())
    {
        Write("Enter a minimum for units in stock: ");
        string unitsInStock = ReadLine() ?? "10";
        int stock = int.Parse(unitsInStock);
        IQueryable<Category>? categories = db.Categories?
        .Include(c => c.Products.Where(p => p.Stock >= stock));//su dung where de loc du lieu
        if (categories is null)
        {
            WriteLine("No categories found.");
            return;
        }
        foreach (Category c in categories)
        {
            WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of { stock} units in stock.");
            foreach (Product p in c.Products)
            {
                WriteLine($" {p.ProductName} has {p.Stock} units in stock.");
            }
        }
    }
}
//filter an sort in ef core
static void QueryingProducts()
{
    using (SolidStore db = new())
    {
        WriteLine("Products that cost more than a price, highest at top.");
        string? input;
        decimal price;
        do
        {
            Write("Enter a product price: ");
            input = ReadLine();
        } while (!decimal.TryParse(input, out price));

        IQueryable<Product>? products = db.Products?
        .Where(product => product.Cost > price)
        .OrderByDescending(product => product.Cost);

        if (products is null)
        {
            WriteLine("No products found.");
            return;
        }
        foreach (Product p in products)
        {
            WriteLine(
            "{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
            p.ProductId, p.ProductName, p.Cost, p.Stock);
        }
    }
}
//Loading patterns with EF Core
/* ○ Eager loading: Load data early
 * Eager loading entities
    We enabled eager loading by calling the Include method for the related products. 
    • Let's see what happens if we do not call Include:
        IQueryable<Category>? categories = db.Categories; //.Include(c => c.Products);

○ Lazy loading: Load data automatically just before it is needed
    Lazy loading was introduced in EF Core 2.1, and it can automatically load missing related data.
    ○ Reference a NuGet package for proxies.
    ○ Configure lazy loading to use a proxy.
    Let's see this in action:
        In the DataWithEFCore project, add a package reference for EF Core proxies, as shown in the following markup:

            <PackageReference
            Include="Microsoft.EntityFrameworkCore.Proxies"
            Version="6.0.0" />

             Build the project to restore packages.
             Open SolidStore.cs, and call an extension method to use lazy loading proxies at the top of the OnConfiguring method, as shown highlighted in the following code:
            -=============
            protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
○ Explicit loading: Load data manually 
    Another type of loading is explicit loading. It works in a similar way to lazy loading, with the difference being that you are in control of exactly what related data is loaded and 
when:
        At the top of Program.cs, import the change tracking namespace to enable us to use the CollectionEntry class to manually load related entities, as shown in the 
following code:
    using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionEntry
 */
//query with Explicit load
static void QueryingCategoriesWithExplicit()
{
    using (SolidStore db = new())
    {
        IQueryable<Category>? categories;
        // = db.Categories;
        // .Include(c => c.Products);
        db.ChangeTracker.LazyLoadingEnabled = false;//disable lazy load
        Write("Enable eager loading? (Y/N): ");
        bool eagerloading = (ReadKey().Key == ConsoleKey.Y);
        bool explicitloading = false;
        WriteLine();
        if (eagerloading)
        {
            categories = db.Categories?.Include(c => c.Products);
        }           
        else
        {
            categories = db.Categories;
            Write("Enable explicit loading? (Y/N): ");
            explicitloading = (ReadKey().Key == ConsoleKey.Y);
            WriteLine();

            foreach(Category c in categories)
            {
                if (explicitloading)
                {
                    Write($"Explicitly load products for {c.CategoryName}? (Y/N): ");
                    ConsoleKeyInfo key = ReadKey();
                    WriteLine();
                    if (key.Key == ConsoleKey.Y)
                    {
                        CollectionEntry<Category, Product> products = db.Entry(c).Collection(c2 => c2.Products);
                        if (!products.IsLoaded)
                            products.Load();
                    }

                }
                WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
            }
            
        }                  
    }    
}
//add Entity
static bool AddProduct(int categoryId, string productName, decimal? price)
{
    using (SolidStore db = new())
    {
        Product p = new()
        {
            CategoryId = categoryId,
            ProductName = productName,
            Cost = price
        };
        // mark product as added in change tracking
        db.Products.Add(p);//insert data to database
        // save tracked change to database
        int affected = db.SaveChanges();//commit transaction
        return (affected == 1);
    }
}

//get product
static void ListProducts()
{
    using (SolidStore db = new())
    {
        WriteLine("{0,-3} {1,-35} {2,8} {3,5} {4}",
        "Id", "Product Name", "Cost", "Stock", "Disc.");
        foreach (Product p in db.Products
        .OrderByDescending(product => product.Cost))
        {
            WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
            p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);
        }
    }
}
//update product
static bool IncreaseProductPrice(
string productNameStartsWith, decimal amount)
{
    using (SolidStore db = new())
    {
        // get first product whose name starts with name
        Product updateProduct = db.Products.First(
        p => p.ProductName.StartsWith(productNameStartsWith));
        updateProduct.Cost += amount;
        int affected = db.SaveChanges();
        return (affected == 1);
    }
}
//delete product
static int DeleteProducts(string productNameStartsWith)
{
    using (SolidStore db = new())
    {
        IQueryable<Product>? products = db.Products?.Where(
        p => p.ProductName.StartsWith(productNameStartsWith));
        if (products is null)
        {
            WriteLine("No products found to delete.");
            return 0;
        }
        else
        {
            db.Products.RemoveRange(products);
        }
        int affected = db.SaveChanges();
        return affected;
    }
}


