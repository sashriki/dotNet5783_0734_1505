
using DO;
namespace Dal
{
    public class DalOrder
    {
        void Add(Order NewOrder)
        {
            DataSource.orders.Add(NewOrder);
        }
    }
}
