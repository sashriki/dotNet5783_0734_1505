using System.Collections.Generic;
using DO;
namespace Dal
{
    public class Program
    {
        public static void main()
        {
            DalOrder dalorder = new DalOrder();

            //הודעה למשתמש לבחור על איזה אובייקט הוא רוצה לבצע פעולה
            int action = Console.Read();
            switch (action)
            {
                case 1:
                    //קריאה לפונקצייה שתתן למשתמש לבצע פעולות בסיסיות
                    OrderFunction.AddProduct();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }


        public class OrderFunction
        {
            public void AddProduct()
            {
                Order order = new Order();
                //תקבל מהמשתמש את כל הפרטים של המזמין חוץ מID שאת זה נקבל מdatasource
                // order.name =
                //   order.adress =
                // order.gmail =
                //order.phone =

            }
        }
    


}
}
