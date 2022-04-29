using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Programing.OOP;
//base class
public abstract class Person
{
    //fields
    private int? _id;

    public Person() { }

    public Person(string name, DateTime dob, Address address, int? id)
    {
        Id = id;
        Name = name;
        Dob = dob;
        Address = address;
        Id = id;
    }



    //properties
    public string Name { get; set; }
    public DateTime Dob { get; set; }
    public Address Address { get; set; } = new Address(); //ke thua has-a
    public int? Id { get => _id; set => _id = value; }

    public override string? ToString()
    {
        return $"[{Id}-{Name}-{Dob}-{Address}]";
    }

    //phuong thuc voi tu khoa virtual
    /*
     - Su dung truoc method hoac properties o lop cha. Cho phep lop ke thua (lop con;lop dan xuat; subclass) co the overide
     - Voi virtual ho tro dac biet cho tinh da hinh -> luc runtime no se quyet dinh phuong thuc nao cua lop cha/con duoc chay
     - Neu nhieu virtual -> giam hieu nang. Mac dinh C# ko virtual
     */

    protected virtual string Display() => $"ID:{Id}, Name:{Name}, DOB:{Dob}, Address:{Address} ";

    //Abstract method
    //Ham tru tuong ko co than
    //Class chua phai la abstract; Class ke thua phai implement phuong thuc nay hoac co tu khoa abstract tren class
    public abstract double GetSalary();
}
