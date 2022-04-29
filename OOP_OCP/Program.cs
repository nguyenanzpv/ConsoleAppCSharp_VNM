namespace OOP_OCP;
public class program{
    public static void Main(String[] args) {
        //BookDiscount mdiscount = new MathDiscount();
        //BookDiscount hdiscount = new HistoryDiscount();
        //BookDiscount ldiscount = new LiteraryDiscount();
        //double amount = mdiscount.GetDiscountByTypeBook(1000000);
        //Console.WriteLine(amount);

        //Phan Employee
        //Dung voi toi uu 1 co chinh sua -> ko phai OCP
        //1. Khai bao 1 list cac Emp
        var empReports = new List<Employee>
        {
            new Employee{Id= 1, Name = "Dong", Level="Senior", HourlyRate=30.5, WorkingHours=160},
            new Employee{Id= 2, Name = "An", Level="Fresher", HourlyRate=60.5, WorkingHours=160},
            new Employee{Id= 1, Name = "Luc", Level="Junior", HourlyRate=50.5, WorkingHours=240},
            new Employee{Id= 1, Name = "Duy", Level="Junior", HourlyRate=20.5, WorkingHours=140},
            new Employee{Id= 1, Name = "Hung", Level="Fresher", HourlyRate=10.5, WorkingHours=160},
        };
        //Goi tinh tong luong n nhan vien tren
        var calculator = new SalaryCalculator(empReports);
        Console.WriteLine($"tong luong {calculator.CalculateTotalSalaries()}. dollars");

        //2. Dung voi toi uu 2 -> OCP
        var empCalculations = new List<BaseSalaryCalculator> {
            new SeniorEmployeeSalaryCalculator(new Employee
            {
                Id=1,Name="Dong",Level="Senior",HourlyRate=30.5,WorkingHours=160
            }),
            new FresherEmployeeSalaryCalculator(new Employee{Id=2,Name="An",Level="Fresher",HourlyRate=60.5,WorkingHours=160})
        };
        var calculatornew = new SalaryCalculatorOCP(empCalculations);
        Console.WriteLine($"tong luong {calculatornew.CalculateTotalSalaries()}. dollars");
    }
}
