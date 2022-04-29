using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Programing.OOP;
public class Employee : Person, IPerson, IComparable<Employee>
{ 
    //Fields
    public int WorkingDay { get; set; }
    public double BaseSalary { get; set; }

    //Consturctor base

    public Employee()
    {

    }

    public Employee(string name, DateTime dob, Address address, int? id) : base(name, dob, address, id)
    {

    }

    public Employee(string name, DateTime dob, Address address, int? id, int workingDay, double baseSalary) : base(name, dob, address, id)
    {
        WorkingDay = workingDay;
        BaseSalary = baseSalary;
    }

    //override virtual method of class base
    protected override string Display() => $"{base.Display()} and Working day:{WorkingDay}, Base Salary:{BaseSalary}";

    //local function
    public override double GetSalary()
    {
        if(WorkingDay < 0)
        {
            throw new ArgumentException($"{nameof(WorkingDay)} canot be negative");
        }
        return localGetSalary();
        //local function closure
        //chi su dung trong GetSalary
        double localGetSalary()
        {
            return WorkingDay * BaseSalary;
        }
    }

    public int GetAge()
    {
        return DateTime.Now.Year - Dob.Year;
    }

    //Employee da co phuong thuc nay
    public int CompareTo(Employee? other)
    {
        //Dev chi ra tieu chi de so sanh 2 object(other, this)
        //0 bang nhau; -1 this < other; 1 this > other
        int compare = (int) this.GetSalary().CompareTo(other.GetSalary());//casting type
        if(compare is 0)
        {
            return this.Name.CompareTo(other.Name);
        }
        return compare;
    }
}
