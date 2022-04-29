using OOP_DIP.Core;
using OOP_DIP.Data.InMemory;
namespace OOP_DIP.App;
public class Program
{
    private static BookPresenter presenter = new();//khoi tao object BookPresenter
    public static async Task Main(string[] args)
    {
        var isPublic = args?.Length == 0 || args?[0] != "admin"; //true
        Console.WriteLine($"isPublic: {isPublic}");
        if (isPublic)
        {
            await PublicAppAsync();
        }
        else
        {
            await AdminAppAsync();
        }
    }

    private static async Task PublicAppAsync()
    {
        var publicService = Composer.CreatePublicService();
        var books = await publicService.FindAllAsync();
        foreach (var book in books)
        {
            presenter.Display(book);
        }
    }

    private static async Task AdminAppAsync()
    {
        var adminService = Composer.CreateAdminService();
        var books = await adminService.FindAllAsync();
        foreach (var book in books)
        {
            presenter.Display(book);
        }
    }

    /*Inner class*/
    private static class Composer
    {
        private readonly static BookStore bookStore = new();//property
        public static AdminService CreateAdminService()//tao ra class tinh
        {
            return new AdminService(bookWriter: bookStore, bookReader: bookStore);
        }
        public static PublicService CreatePublicService()
        {
            return new PublicService(bookReader: bookStore);
        }
    }
}

