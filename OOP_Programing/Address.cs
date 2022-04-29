using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solid.Programing.OOP;
public class Address
{
    //fields
    private string? _street;
    private string? _ward;
    private string? _city;
    private string? _district;

    public Address()
    {

    }

    public Address(string? street, string? ward, string? city, string? district)
    {
        _street = street;
        _ward = ward;
        _city = city;
        _district = district;
    }

    //properties
    public string? Street { get; init; }//C#9.0
    public string? Ward { get => _ward; set => _ward = value; }
    public string? City { get => _city; set => _city = value; }
    public string? District { get => _district; set => _district = value; }

    public override string? ToString()
    {
        return $"[{Street}-{Ward}-{District}-{City}]";
    }
}
