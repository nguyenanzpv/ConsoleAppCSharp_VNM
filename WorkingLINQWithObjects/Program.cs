using static System.Console;
using System.Linq;
using System.Collections.Generic;

// a string array is a sequence that implements IEnumerable<string>
string[] names = new[] { "Mai", "Dong", "Duy", "Huy", "Man", "Long", "Luc", "Minh" };
WriteLine("Deferred execution"); //hoãn lại thực thi

// Question: Which names end with an Y?
// (written using a LINQ extension method)
var query1 = names.Where(name => name.EndsWith("y"));

// Question: Which names end with an Y?
// (written using LINQ query comprehension syntax) ->cu phap query linq day du
var query2 = from name in names where name.EndsWith("y") select name;

// Answer returned as an array of strings containing Duy and Huy
string[] result1 = query1.ToArray();

// Answer returned as a list of strings containing Huy and Duy
List<string> result2 = query2.ToList();

// Answer returned as we enumerate over the results
foreach (string name in query1)
{
    WriteLine(name); // outputs 
    names[2] = "Dong"; // change to Dong

}

WriteLine("Writing queries");
/*
var query = names.Where(
 new Func<string, bool>(NameLongerThanFour));
var query = names.Where(NameLongerThanFour);
 foreach (string item in query)
{
 WriteLine(item);
}
*/

static bool NameLongerThanFour(string name)
{
    return name.Length > 4;
}

//query like stream
var query4 = names
    .Where(name => name.Length > 4)
    .OrderBy(name => name.Length)
    .ThenBy(name => name);
//or 
var query5 = from name1 in names
             where name1.Length > 4
             orderby name1.Length, name1 descending
             select name1;

//or
var query6 = (
        from name in names
        where name.Length > 4
        select name
    )
    .Skip(1)
    .Take(1);

//Use IOrderedEnumerable to sort
IOrderedEnumerable<string> query = names
 .Where(name => name.Length > 4)
 .OrderBy(name => name.Length)
 .ThenBy(name => name);
foreach (string item in query)
{
    WriteLine(item);
}
WriteLine("Filtering by type");

/*Working with sets and bags using LINQ*/
//Sets: la tap hop cac phan tu khong trung nhau
//bags la tap hop cac phan tu co the trung nhau
//Thuc thi code voi cac methods cua Sets
string[] names1 = new[]
{ "dong", "truc", "minh", "huy" };
string[] names2 = new[]
{ "duy", "luc", "minh", "huy","huy"};
//In ra
WriteLine(string.Join(", ", names2.ToArray()));//in ra phan tu cach nhau dau , -> neu ko Join se in ra dia chi vung nho
WriteLine(string.Join(", ", names2.Distinct().ToArray()));
WriteLine(string.Join(", ", names2.DistinctBy(name => name.Substring(0, 2))));// loc trung sau khi subtring tu 0-2
WriteLine(string.Join(",", names1.Union(names2).ToArray()));// merge 2 mang va loai cac phan tu duplicate
//Cac ham khac: Concat(noi), Intersect(lay phan giong nhau), Except(lay phan co o 1 ma ko co o 2)

