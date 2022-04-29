using OOP_DIP.Data;
namespace OOP_DIP.Core;
/*
 Core depends on Data -> Core chua Data
 */
public class PublicService
{
    private readonly IBookReader _bookReader;
    public PublicService(IBookReader bookReader)
    {
        _bookReader = bookReader;
    }
    /*
     Task: muc tieu giup cho chuong trinh co the chay song song(paralles) => async, await
     1 Task co nhieu process
     1 Thread co the chay nhieu task cung luc
     */
    public Task<IEnumerable<Book>> FindAllAsync()
    {
        return Task.FromResult(_bookReader.Books);
    }

    public Task<Book?> FindAsync(int bookId)
    {
        var book = _bookReader.Find(bookId);
        return Task.FromResult(book);
    }

}

