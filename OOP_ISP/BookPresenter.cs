
namespace OOP_ISP;
public class BookPresenter
{
    public void Display(Book book)
    {
        Console.WriteLine($"The book with:{book.Id} and {book.Title}");
    }
}

