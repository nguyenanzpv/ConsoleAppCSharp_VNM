using OOP_DIP.Data;
namespace OOP_DIP.App;
public class BookPresenter
{
    public void Display(Book book)
    {
        Console.WriteLine($"The book with:{book.Id} and {book.Title}");
    }
}


