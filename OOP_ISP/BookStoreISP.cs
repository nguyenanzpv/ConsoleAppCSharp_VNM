using OOP_ISP;
namespace SOLID.OOP_ISP;
using System.Collections.ObjectModel;
//1. Thuc thi CRUD - create retrive (find/get) update and delet
/*
Thay vì dùng 1 interface lớn, ta nên tách thành nhiều interface nhỏ, với nhiều mục đích cụ thể 
 */
public interface IBookReader
{
    public IEnumerable<Book> Books { get; }//ko dung this voi static; IEnumerable cung cap cac phuoc thuc de tu lap noi than
    Book? Find(int bookId);
}
public interface IBookWriter
{
    void Create(Book book);
    void Replace(Book book);
    void Remove(Book book);
}
public class BookStore : IBookReader, IBookWriter
{
        private static int _lastId = 0;//id cuoi cung cua book
        private static List<Book> _books { get; }
        public static int NextId => ++_lastId;//tang lastId roi tra ve
        public IEnumerable<Book> Books => new ReadOnlyCollection<Book>(_books);

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


