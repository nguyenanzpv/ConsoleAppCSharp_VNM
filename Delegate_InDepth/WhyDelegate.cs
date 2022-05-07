namespace DLG;
public interface IPerson
{
    double CalculateSalary(double salary);
}

public class Employee : IPerson
{
    public double CalculateSalary(double salary)
    {
        //Neu la Employee giam 10%
        return salary - (salary * 0.1);
    }
}

public class Worker : IPerson
{
    public double CalculateSalary(double salary)
    {
        return (salary - (salary * 0.05));
    }
}
