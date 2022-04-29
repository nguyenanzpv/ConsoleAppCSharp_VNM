using System.Collections.ObjectModel;
using OOP_DIP.Data;
namespace OOP_DIP.Data.InMemory;
public class BookStore : IBookReader, IBookWriter
{
    private static int _lastId = 0;//id cuoi cung cua book
    private static List<Book> _books { get; }
    public static int NextId => ++_lastId;//tang lastId roi tra ve
    public IEnumerable<Book> Books => new ReadOnlyCollection<Book>(_books);//collectuion chi doc

    static BookStore()
    {
        _books = new List<Book> {
            new Book
            {
                Id = NextId,
                Title = "Pratical ASP.Net and VueJS"
            }
        };
    }

    public void Create(Book book)
    {
        if (book.Id != default)//Khac 0
        {
            throw new Exception("A new book cannot create with an id");
        }
        book.Id = NextId;
        _books.Add(book);
    }

    public Book? Find(int bookId)
    {
        return _books.FirstOrDefault(x => x.Id == bookId);
    }

    public void Remove(Book book)
    {
        if (!_books.Any(b => b.Id == book.Id))
        {
            throw new Exception($"The book {book.Id}does not exists");
        }
        _books.Remove(book);
    }

    public void Replace(Book book)
    {
        if (!_books.Any(b => b.Id == book.Id))
        {
            throw new Exception($"The book {book.Id}does not exists");
        }
        var index = _books.FindIndex(x => x.Id == book.Id);
        _books[index] = book;
    }
}

