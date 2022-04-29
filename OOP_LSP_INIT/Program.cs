namespace solid.asp.lsp;
/*
 Trong một chương trình, các object của class con có thể thay thế class cha 
 mà không làm thay đổi tính đúng đắn của chương trình
 Hãy tưởng tượng bạn có 1 class cha tên Vịt. Các class VịtBầu, VịtXiêm có thể kế thừa class này
 chương trình chạy bình thường. Tuy nhiên nếu ta viết class VịtChạyPin, cần pin mới chạy được. 
 Khi class này kế thừa class Vịt, vì không có pin không chạy được, sẽ gây lỗi. 
 Đó là 1 trường hợp vi phạm nguyên lý này.
 */
//Vi du: co 1 mang so nguyen -> tinh tong cac so nguyen va return lai
//===============Theo Cach Thuong====================================
//Cach nay da hinh se ko the override duoc -> van dung phuong thuc lop cha
public class SumCalculator
{
    protected readonly int[] _numbers;
    public SumCalculator(int[] numbers)
    {
        _numbers = numbers;
    }

    public int Calculate() => this._numbers.Sum();
}

//tinh tong cac so chan
public class EvenNumberSumCalculator : SumCalculator
{
    public EvenNumberSumCalculator(int[] numbers) : base(numbers)
    {

    }
    //thuc thi viec tinh tong cac so chan
    public new int Calculate() => this._numbers.Where(x=>x%2==0).Sum();
}
//End===============Theo Cach Thuong====================================
//======================Theo cach ke thua =============================
//Cach nay khi dung da hinh se chay theo ham cua con
public class SumCalculatorVirtual
{
    protected readonly int[] _numbers;
    public SumCalculatorVirtual(int[] numbers)
    {
        _numbers = numbers;
    }

    public virtual int Calculate() => this._numbers.Sum();
}

//tinh tong cac so chan
public class EvenNumberSumCalculatorVirtual : SumCalculatorVirtual
{
    public EvenNumberSumCalculatorVirtual(int[] numbers) : base(numbers)
    {

    }
    //thuc thi viec tinh tong cac so chan
    public override int Calculate() => this._numbers.Where(x => x % 2 == 0).Sum();
}
//End======================Theo cach ke thua =============================
//=====================Theo cach Liskov==========================================
//Ke thua gian tiep qua Calculator
//Khi dung da hinh se chay phuong thuc cua ham con
public abstract class Calculator
{
    protected readonly int[] _numbers;
    public Calculator(int[] numbers)
    {
        _numbers = numbers;
    }

    public abstract int Calculate();
}

public class SumCalculator2 : Calculator
{
    public SumCalculator2(int[] numbers) : base(numbers)
    {

    }

    public override int Calculate() => _numbers.Sum();
}

public class EvenNumberSumCalculatorLokhov : SumCalculator2
{
    public EvenNumberSumCalculatorLokhov(int[] numbers) : base(numbers)
    {

    }
    //thuc thi viec tinh tong cac so chan
    public override int Calculate() => this._numbers.Where(x => x % 2 == 0).Sum();
}
//End =====================Theo cach Liskov==========================================
class Program
{
    static void Main(string[] args)
    {
        var numbers = new int[] {5,6,7,4,2,3};
        SumCalculator sum = new SumCalculator(numbers);
        Console.WriteLine($"Sum: {sum.Calculate()}");
        EvenNumberSumCalculator evenNumberSumCalculator = new EvenNumberSumCalculator(numbers);
        Console.WriteLine($"SumEvent: {evenNumberSumCalculator.Calculate()}");

        //Su dung da hinh
        SumCalculator evenSum = new EvenNumberSumCalculator(numbers);
        Console.WriteLine($"SumEventByPolimerphine: {evenSum.Calculate()}");//no van chay phuong thuc cua SumCalculator -> dung phai chay EvenSumCalculator

        //Run theo Loskov
        var numbers2 = new int[] { 5, 6, 7, 4, 2, 3 };
        Calculator sum2 = new SumCalculator2(numbers2);
        Console.WriteLine($"Sum: {sum2.Calculate()}");
        Calculator evenNumberSumLokhov = new EvenNumberSumCalculatorLokhov(numbers2);
        Console.WriteLine($"Sum: {evenNumberSumLokhov.Calculate()}");
    }
}
