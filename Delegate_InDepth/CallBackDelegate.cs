namespace DLG;
/*
 Yeu cau la 1 list cac person, trong person co nhieu loai tuoi khac nhau
 Dat ra cau hoi:
    Voi moi yeu cau loc theo 1 tuoi nao do -> ds cac person thoa man
 */
//Dinh nghia ra cac dieu kien o cac ham khac nhau
public class OperationFilter
{
    public delegate bool FilterPersonDelegate(Person p);
    public static bool isChild(Person p)
    {
        return p.Age <= 17;
    }

    public static bool isAdult(Person p)
    {
        return p.Age >= 18;
    }

    public static bool isSenior(Person p)
    {
        return p.Age >= 60;
    }

    public static void DisplayPeople(string title, List<Person> lstPerson, FilterPersonDelegate filter)
    {
        Console.WriteLine(title);
        foreach(Person p in lstPerson)
        {
            if (filter(p))
            {
                Console.WriteLine($"Ten la: {p.Name} va tuoi la: {p.Age}");
            }
        }
    }

}