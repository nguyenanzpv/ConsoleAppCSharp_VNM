namespace OOP_SRP;
public class program
{
    private static readonly BookStore _bookStore = new();
    private static readonly BookPresenter _bookPresenter = new();

    public static void Main(string[] args)
    {
        var run = true;
        do
        {
            Console.Clear();
            Console.WriteLine("Choices....");
            Console.WriteLine("1. Fetch and display the book with id(1)");
            Console.WriteLine("2. Create a book");
            Console.WriteLine("3. List all book");
            Console.WriteLine("4. Fail to fetch a book");
            Console.WriteLine("0. Exit");
            var input = Console.ReadLine();
            Console.Clear();
            try
            {
                switch (input)
                {
                    case "0":
                        run = false;
                        break;
                    case "1":
                        FetchAndDisplayBook();
                        break;
                    case "2":
                        CreateBook();
                        break;
                    case "3":
                        ListAllBook();
                        break;
                    default:
                        Console.WriteLine("Press enter to go back main menu");
                        break;
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following exception occured, press enter to continue... ");
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }
        } while (run);
    }

    private static void FetchAndDisplayBook()
    {
        var book = _bookStore.Load(1);
        _bookPresenter.Display(book!);//! de book ko bi null
    }

    private static void CreateBook()
    {
        Console.Clear();
        Console.WriteLine("Please enter the book title");
        var title = Console.ReadLine();
        var book = new Book
        {
            Id = BookStore.NextId,
            Title = title
        };
    }

    private static void ListAllBook()
    {
        foreach (var book in _bookStore.Books)
        {
            _bookPresenter.Display(book);
        }
    }
}
