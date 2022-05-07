/*
 Delegate la con tro ham -> tro toi 1 ham de thuc thi khi can
 Ko co body
 
 */
namespace DLG;

//dinh nghia delegate -> pham vi global
public delegate int SumDelegate(int x, int y);
public delegate void DisplayDelegate(int sum);
//for multiple cast delegate
public delegate void MathematicDelegate(int x, int y);

public class Program
{
    //Khai bao delegate thay the interface
    public delegate double PersonSalaryDelegate(double s);
    public static void Main(string[] args)
    {
        //cau hinh de console hieu unicode
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        Program obj = new Program();
        //goi phuong thuc theo cach thong thuong
        //obj.Display(obj.Sum(10, 11));
        //goi phuong thuc theo delegate: single delegate
        SumDelegate sumDele = new SumDelegate(obj.Sum);//Cach 1
        SumDelegate SumDele2 = obj.Sum;//Cach 2
        DisplayDelegate displayDele = obj.Display;
        //Excute
        int total = sumDele(10, 20);
        displayDele(total);

        //Multicast Delegate
        //Truong hop 1: co the su dung += hoac -=
        MathematicDelegate mathDele = new MathematicDelegate(Program.Add);
        mathDele += Program.Subtract;
        //Truong hop 2: -> recomdend
        MathematicDelegate math2 = new MathematicDelegate(Add);
        MathematicDelegate math3 = new MathematicDelegate(Subtract);
        MathematicDelegate math4 = Multiply;
        MathematicDelegate math5 = new MathematicDelegate(Divide);
        MathematicDelegate math6 = math2 + math3 + math4 + math5;//theo thu tu khai bai cac ham + - * /
        math6.Invoke(10, 20);
        // bo 1 delagate ra
        math6 -= math3;
        math6.Invoke(10, 20);

        Console.WriteLine("===========Begin Interface Demo===============");
        double empSalary = CalculationSalary(new Employee(), 1000000);
        Console.WriteLine($"Luong cua employee la: {empSalary}");
        Console.WriteLine("===========End Begin Interface Demo===============");

        Console.WriteLine("===========Begin Delegate Demo===============");
        double empSalaryDelegate = CalculationSalaryDelegate(GetSalaryByEmpDele, 10000000);
        Console.WriteLine($"Luong cua employee la: {empSalaryDelegate}");
        double workerSalaryDelegate = CalculationSalaryDelegate(GetSalaryByWorkerDele, 10000000);
        Console.WriteLine($"Luong cua worker la: {workerSalaryDelegate}");
        Console.WriteLine("===========End Begin Delegate Demo===============");

        Console.WriteLine("===========Begin Delegate Callback===============");
        //1.create list person
        Person p1 = new Person() { Name = "Dong", Age = 40 };
        Person p2 = new Person() { Name = "Hai", Age = 17 };
        Person p3 = new Person() { Name = "Minh", Age = 65 };
        Person p4 = new Person() { Name = "Luc", Age = 12 };
        Person p5 = new Person() { Name = "Duy", Age = 32 };
        Person p6 = new Person() { Name = "Huy", Age = 15 };
        //2.Dua vao collection
        List<Person> lstPerson = new List<Person>() { p1, p2, p3, p4, p5, p6 };
        //3.Invoke by Delegate
        OperationFilter.DisplayPeople("Adults",lstPerson,OperationFilter.isAdult);
        Console.WriteLine("===========End Begin Delegate Callback===============");

        Console.WriteLine("===========Begin using Delegate Predefined built-in===============");
        //Delegate Func
        //ko nhan parameter vao -> tra lai gia tri theo ham
        Func<string> getMessage = GetInfoMessage;

        Console.WriteLine(getMessage());

        string GetInfoMessage()
        {
            return "Day la message";
        }
        //Delegate Action giong nhu func
        //C# Predicate delegate
        List<int> numbers = new List<int> { 4, 3, 6, 8, 3, 99, 66, 7, 8 };
        //Vao la kieu T -> ra la boolean
        Predicate<int> predicate = checkEven;

        bool checkEven(int x)
        {
            return x % 2 == 0;
        }

        List<int> lstNumberResult = numbers.FindAll(predicate);//c2 numbers.FindAll(x=>x%2==0)
        foreach(int x in lstNumberResult)
        {
            Console.WriteLine(x);
        }

        Console.WriteLine("===========End Begin using Delegate Predefined built-in===============");

        Console.WriteLine("===========Begin using Delegate Event===============");
        Total sumDelegate = new Total();
        sumDelegate.OnSumEvent += new Total.dgEventRaise(evSumOverTrigger);

        /*Thuc thi viec tinh tong , neu vuot qua 100 thi trigger ev*/
        int totalResult = sumDelegate.Sum(99, 150);
        Console.WriteLine($"tong la : {totalResult}");

        static void evSumOverTrigger()
        {
            Console.WriteLine("Tong cua hai so lon hon 100....!");
        }

        // EventHandler delegate su dung san co cua C#
        Console.WriteLine("===========End Begin using Delegate Event===============");
        Console.ReadKey();
    }
    //Ham tinh luong voi delegate
    public static double CalculationSalaryDelegate(PersonSalaryDelegate p, double salary)
    {
        return p.Invoke(salary);
    }

    //Ham de tra luong voi interface
    public static double CalculationSalary(IPerson p, double salary)
    {
        return p.CalculateSalary(salary);
    }

    //dinh nghia cac cach tinh luong dung cho delegate
    public static double GetSalaryByEmpDele(double s)
    {
        return s - (s * 0.1);
    }

    public static double GetSalaryByWorkerDele(double s)
    {
        return s - (s * 0.05);
    }
    //end dinh nghia cac cach tinh luong dung cho delegate
    public int Sum(int a, int b)
    {
        return a + b;
    }

    public void Display(int sum)
    {
        Console.WriteLine($"Tong hai so la: {sum}");
    }
    /*Gia su co nhieu phuong thuc thuc hien nhieu viec -> dung cho multicast delegate*/
    public static void Add(int x, int y)
    {
        Console.WriteLine($"Tong hai so {x} va {y} la {x + y}");
    }

    public static void Subtract(int x, int y)
    {
        Console.WriteLine($"Hieu hai so {x} va {y} la {x - y}");
    }

    public static void Multiply(int x, int y)
    {
        Console.WriteLine($"Tich hai so {x} va {y} la {x * y}");
    }

    public static void Divide(int x, int y)
    {
        Console.WriteLine($"Thuong hai so {x} va {y} la {x / y}");
    }
}
