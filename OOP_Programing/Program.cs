using Solid.Programing.OOP;
using System;
using static System.Console;//ko can Console.
namespace OOPs;

public class Program
{
    public static void Main(string[] args)
    {
        Address address = new (ward:"Phuong 13", street:"Tan Quy", city:"TPHCM", district:"Tan Phu");
        Employee e = new (name:"Bui Huu Dong",dob:new DateTime(1983,02,19),address:address,1);
        //Da hinh -> Dependence Injection
        Person p = new Employee(name: "Bui Huu Dong", dob: new DateTime(1983, 02, 19), address: address, 1);
        WriteLine(e);
        WriteLine("-----------------------------------");
        //So sanh va sap xep 1 mang cac doi tuong
        /*
         - Co 2 cach de so sanh object theo 1 tieu chi nao do
         - C1: Bien lop Employee co the so sanh duoc -> dung IComparable
         */
        Employee[] lstEmps =
        {
            new(name:"Dong",dob:new DateTime(1983,02,19),address:address,1,20,9000000),
            new (name:"AN",dob: new DateTime(1983, 02, 19),address: address,2,30,8000000),
            new (name:"Huy",dob: new DateTime(1983, 02, 19),address: address,3,20,7000000)
        };

        //C1: truyen thong- dung giai thuat sap xep roi return
        //C2:Array.sort
        print(lstEmps);
        Array.Sort(lstEmps);
        print(lstEmps);
        //C2:
        Array.Sort(lstEmps, new EmployeeComparer());
        //Su dung Anonymus
        Array.Sort(lstEmps, (a, b) => a.Name.CompareTo(b.Name));
    }

    public static void print(Employee[] lstData)
    {
        if(lstData is not null)
        {
            foreach(Employee e in lstData)
            {
                WriteLine(e);
            }
        }
    }
}


