using OOP_DIP.Data;
namespace OOP_DIP.Core;
public class AdminService
{
    private readonly IBookWriter _bookWriter;
    private readonly IBookReader _bookReader;

    public AdminService(IBookReader bookReader, IBookWriter bookWriter)
    {
        _bookReader = bookReader;
        _bookWriter = bookWriter;
    }

    public Task<IEnumerable<Book>> FindAllAsync()
    {
        return Task.FromResult(_bookReader.Books);
    }

    public Task<Book?> FindAsync(int bookId)
    {
        var book = _bookReader.Find(bookId);
        return Task.FromResult(book);
    }

    public Task CreateAsync(Book book)
    {
        _bookWriter.Create(book);
        return Task.CompletedTask;//chi return status
    }

    public Task RemoveAsync(Book book)
    {
        _bookWriter.Remove(book);
        return Task.CompletedTask;
    }

    public Task ReplaceAsync(Book book)
    {
        _bookWriter.Replace(book);
        return Task.CompletedTask;
    }
}

