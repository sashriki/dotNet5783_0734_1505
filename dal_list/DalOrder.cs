using DO;
namespace Dal;

public class DalOrder
{
    //create
    public int AddOrder(Order NewOrder)
    {
        NewOrder.OrderId = DataSource.Config.orderIndex;
        DataSource.orders.Add(NewOrder);
        return NewOrder.OrderId;
    }

    //Request all
    public List<Order> GetOrders()
    {
        List<Order> orderReturnList = new List<Order>();
        for (int i = 0; i < DataSource.orders.Count(); i++) //warning??
            orderReturnList.Add(DataSource.orders[i]);
        return orderReturnList;
    }
    //      orderReturnArr.Insert(i, DataSource.orders[i]);

    //Request By Id
    public Order GetOrderById(int idOrder)
    {
        int x = DataSource.orders.FindIndex(x => x.OrderId  == idOrder);
        if (x == -1)
            throw new Exception($"order id {idOrder} is not found in orders");
        else
            return DataSource.orders[x];
    }

    //update
    public void UpdateOrder(Order UpdatedOrder)
    {
        int x = DataSource.orders.FindIndex(x => x.OrderId == UpdatedOrder.OrderId);
        if (x == -1)
            throw new Exception($"Order id {UpdatedOrder} is not found in orders");
        DataSource.orders[x] = UpdatedOrder;
    }

    //delete
    public void DeleteOrder(int removeById)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i].OrderId == removeById)
            {
                DataSource.orders.Remove(DataSource.orders[i]);
                return;
            }
        }
        throw new Exception($"order id {removeById} is not found in items.");
    }
}
