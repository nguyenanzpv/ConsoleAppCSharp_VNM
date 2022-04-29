namespace OOP_ISP;
public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }

}

public class BookStore
{
    private static int _lastId = 0;//id cuoi cung cua book
    private static List<Book> _books { get; }
    public static int NextId => ++_lastId;//tang lastId roi tra ve
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

    public IEnumerable<Book> Books => _books;//ko dung this voi static; IEnumerable cung cap cac phuoc thuc de tu lap noi than
    public void Save(Book book)
    {
        //check if exists
        if (_books.Any(b => b.Id == book.Id))
        {
            //update
            var index = _books.FindIndex(b => b.Id == book.Id);
            _books[index] = book;
        }
        else
        {
            _books.Add(book);
        }
    }

    public Book? Load(int bookId)
    {
        return _books.FirstOrDefault(b => b.Id == bookId);
    }
}
