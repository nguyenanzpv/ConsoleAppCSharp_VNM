namespace OOP_DIP.Data;
public interface IBookReader
{
    public IEnumerable<Book> Books { get; }//ko dung this voi static; IEnumerable cung cap cac phuoc thuc de tu lap noi than
    Book? Find(int bookId);
}
