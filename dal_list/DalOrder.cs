using DO;
namespace Dal;

public class DalOrder
{
    //create
    public int AddOrder(Order NewOrder)
    {

        DataSource.orders.Add(NewOrder);
        return NewOrder.OrderId;
    }

    //Request all
    public List<Order> GetOrders()
    {
        List<Order> orderReturnList = new List<Order>();
        for (int i = 0; i < DataSource.orders.Count(); i++) //warning??
            orderReturnList[i] = DataSource.orders[i];
        return orderReturnList;
    }
    //      orderReturnArr.Insert(i, DataSource.orders[i]);

    //Request By Id
    public Order GetOrderById(int idOrder)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
            if (DataSource.orders[i].OrderId == idOrder)
                return DataSource.orders[i];

        throw new Exception($"course id {idOrder} is not found in orders");
    }

    //update
    public void UpdateOrder(Order UpdatedOrder)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
            if (DataSource.orders[i].OrderId == UpdatedOrder.OrderId)
            {
                DataSource.orders[i] = UpdatedOrder;
                return;
            }
        throw new Exception($"Order id {UpdatedOrder} is not found in orders");
    }

    //delete
    public void DeleteOrder(int removeById)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
            if (DataSource.orders[i].OrderId == removeById)
            {
                DataSource.orders.Remove(DataSource.orders[i]);
                return;
            }
        throw new Exception($"order id {removeById} is not found in items.");
    }
}
