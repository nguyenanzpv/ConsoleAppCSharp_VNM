namespace OOP_OCP;
/*
  Nguyen tac 2 solid: Open/Closed Principal
  Tao lop dan xuat moi ke thua lop cu
  Truy xuat lop cu qua interface
 */
public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }

}

//Tra ra so tien discount theo tung loai sach
public class DiscountBook
{
    public double GetDiscountBookType(double amount, BookType bookType)
    {
        /*
         Thay vi if...else -> su dung 1 class BookDiscount de cho ke thua
         */
        double finalAmount = 0;
        if(bookType == BookType.Math)
        {
            finalAmount = amount -150;
        }
        else if(bookType == BookType.History)
        {
            finalAmount = amount - 100;
        }
        else if(bookType == BookType.Literary)
        {
            finalAmount = amount - 50;
        }
        return finalAmount;
    }
}

public enum BookType
{
    Math,
    History,
    Literary
}
