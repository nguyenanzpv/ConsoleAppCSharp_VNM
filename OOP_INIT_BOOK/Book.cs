namespace INITBOOK;

public class Book
{
    /*
     1. Book class
        - La cau truc du lieu co id va title
        - Co 2 ham save va load data luu book
        - Quan ly tang id 1 cach tu dong
        - Show data output va display method
     2. 
        - Ban than no la Book
        - Thuc hien tinh toan
        - Quan ly data
     */
    public int Id { get; set; }
    public string? Title { get; set; }
    private static int _lastId = 0;//id cuoi cung cua book
    public static int NextId => ++_lastId;//tang lastId roi tra ve
    public static List<Book> Books { get; }
    static Book()//khoi tao Book
    {
        Books = new List<Book> { 
            new Book
            {
                Id = NextId,
                Title = "Pratical ASP.Net and VueJS"
            }
        };
    }

    public Book(int? id = null)
    {
        Id = id ?? default;//thay vi int a = default(int) -> C# 8.0 ??
    }
    //method save de luu neu book chua co, update neu da co id

    public void save()
    {
        //check if exists
        if(Books.Any(b => b.Id == Id))
        {
            //update
            var index = Books.FindIndex(b => b.Id == Id);
            Books[index] = this;//this la book co b.Id == Id
        }
        else
        {
            Books.Add(this);
        }
    }

    public void load()
    {
        if(Id == default)
        {
            //Nem ra 1 ngoai le
            throw new Exception("You must be set Id for this book");
        }
        var book = Books.FirstOrDefault(b => b.Id == Id);//tra lai book dau tien co Id==Id, neu null thi tra ve dafault
        //kiem tra book co ko
        if(book == null)
        {
            throw new Exception("This book does not exits");
        }
        Id= book.Id;
        Title= book.Title;

    }

    public void display()
    {
        Console.WriteLine($"The book with:{Id} and {Title}");
    }

}
