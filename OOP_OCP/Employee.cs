namespace OOP_OCP;
/*Nguyen tac 2-solid: dung abstract*/
//Có thể thoải mái mở rộng 1 class, nhưng không được sửa đổi bên trong class đó 
//(open for extension but closed for modification).
public class Employee //lop base
{
    public int Id { get; set; } 
    public string Name { get; set; }   
    public string Level { get; set; }//cac muc nhan vien khac nhau
    public int WorkingHours { get; set; }
    public double HourlyRate { get; set; }

}

public class SalaryCalculator
{
    //co gi
    private readonly IEnumerable<Employee> _employees;//1 ds co the lap ben trong
    public SalaryCalculator(List<Employee> employees)
    {
        this._employees = employees;
    }
    //nguyen thuy
    //han che -> neu thay doi cach tinh luong thi phai sua ham nay
    public double CalculateTotalSalaries()
    {
        double totalSalaries = 0D;
        foreach (Employee e in _employees)
        {
            totalSalaries += e.HourlyRate * e.WorkingHours;
        }
        return totalSalaries;
    }

    //toi uu 1
    //Co thay doi cach tinh luong theo level
    public double NewCalculateTotalSalaries()
    {
        double totalSalaries = 0D;
        foreach (Employee e in _employees)
        {
            if(e.Level == "Senior")
            {
                totalSalaries += e.HourlyRate * e.WorkingHours * 1.2;
            }
            else
            {
                totalSalaries += e.HourlyRate * e.WorkingHours;
            }
        }
        return totalSalaries;
    }
}

//Toi uu 2: dung OCP
public abstract class BaseSalaryCalculator
{
    protected Employee Employee { get; private set; } //1 nhan vien 
    public BaseSalaryCalculator(Employee emp)
    {
        Employee = emp;
    }

    public abstract double CalculateSalary();//phuong thuc truu tuong tinh luong theo tung loai nhan vien
}

public class SeniorEmployeeSalaryCalculator : BaseSalaryCalculator
{
    public SeniorEmployeeSalaryCalculator(Employee emp) : base(emp)
    {

    }
    public override double CalculateSalary() => Employee.HourlyRate * Employee.WorkingHours * 1.2;
}

public class JuniorEmployeeSalaryCalculator : BaseSalaryCalculator
{
    public JuniorEmployeeSalaryCalculator(Employee emp) : base(emp)
    {

    }
    public override double CalculateSalary() => Employee.HourlyRate * Employee.WorkingHours * 1.1;
}

public class FresherEmployeeSalaryCalculator : BaseSalaryCalculator
{
    public FresherEmployeeSalaryCalculator(Employee emp) : base(emp)
    {

    }
    public override double CalculateSalary() => Employee.HourlyRate * Employee.WorkingHours * 1.0;
}

//dung nhu the nao
public class SalaryCalculatorOCP { 
    private readonly IEnumerable<BaseSalaryCalculator> _employeeCalculation; //DI not DI Uoc
    public SalaryCalculatorOCP(IEnumerable<BaseSalaryCalculator> employeeCalculation)
    {
        this._employeeCalculation = employeeCalculation;
    }

    public double CalculateTotalSalaries()
    {
        double totalSalaries = 0D;
        foreach (var e in _employeeCalculation)
        {
            totalSalaries += e.CalculateSalary();
        }
        return totalSalaries;
    }
}


