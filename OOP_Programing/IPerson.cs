using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IPerson
{
    //chua cac phuong thuc abstract
    //Neu co bien thi phai la static va const
    const string NameOfCompany = "Solid";
    public abstract int GetAge();
}
