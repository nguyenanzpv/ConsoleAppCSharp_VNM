namespace OOP_OCP;
/*
 Nguyen tac 2: dung virtual
//Có thể thoải mái mở rộng 1 class, nhưng không được sửa đổi bên trong class đó 
//(open for extension but closed for modification).
 */
public class BookDiscount
{
    public virtual double GetDiscountByTypeBook(double amount)
    {
        return amount - 20;
    }
}

public class MathDiscount : BookDiscount
{
    public override double GetDiscountByTypeBook(double amount)
    {
        return base.GetDiscountByTypeBook(amount)-150;
    }
}

public class HistoryDiscount : BookDiscount
{
    public override double GetDiscountByTypeBook(double amount)
    {
        return base.GetDiscountByTypeBook(amount) - 100;
    }
}

public class LiteraryDiscount : BookDiscount
{
    public override double GetDiscountByTypeBook(double amount)
    {
        return base.GetDiscountByTypeBook(amount) - 50;
    }
}