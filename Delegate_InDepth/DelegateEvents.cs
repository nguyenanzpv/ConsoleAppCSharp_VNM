namespace DLG;
/*
 Gia su ta co 1 ham tinh tong 2 so truyen vao. neu nhu a+b >100 thi vang ra 1 su kien. lam the nao de nhan su kien vua vang ra de xu ly no
 */
public class Total
{
    public delegate void dgEventRaise();//dinh nghia 1 delegate cho event
    public event dgEventRaise OnSumEvent;//dinh nghia event voi kieu dgEventRaise

    public int Sum(int a, int b)
    {
        int sum = a + b;
        if((sum >= 100) && (OnSumEvent != null))
        {
            OnSumEvent();//nhay vao su kien
        }
        return sum;
    }
}
