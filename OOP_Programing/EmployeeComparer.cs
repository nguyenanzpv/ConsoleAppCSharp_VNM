using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Programing.OOP;
public class EmployeeComparer : IComparer<Employee>
{
    public int Compare(Employee? x, Employee? y)
    {
        //Logic so sanh theo ten
        if(x is null || y is null)
        {
            return 0;
        }
        //so sanh chieu dai name
        int result = x.Name.Length.CompareTo(y.Name.Length);
        if(result == 0)
        {
            return x.Name.CompareTo(y.Name);
        }
        return result;
    }
}

