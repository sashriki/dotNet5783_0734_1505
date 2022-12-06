using DO;
namespace Dal;
using DalApi;


internal class DalOrder : Iorder
{
    /// <summary>
    /// Creating a new order and adding it to the list of orders
    /// </summary>
    /// <param name="NewOrder"></param>
    /// <returns></returns>
    public int Add(Order NewOrder)
    {
        NewOrder.OrderId = DataSource.Config.orderIndex;
        int x = DataSource.orders.FindIndex(x => x.Value.OrderId == NewOrder.OrderId);
        if (x != -1)
            throw new DuplicationException("Order");
        DataSource.orders.Add(NewOrder);
        return NewOrder.OrderId;
    }
    /// <summary>
    /// Returning an order from the order list
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order? GetById(int idOrder)
    {
        Order? order=new Order();
        int x = DataSource.orders.FindIndex(x => x?.OrderId == idOrder);
        if (x == -1)
            throw new NotfoundException("Order");
        order =DataSource.orders[x];
        return order;
    }
    /// <summary>
    /// Updating an order in the order list
    /// </summary>
    /// <param name="UpdatedOrder"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order UpdatedOrder)
    {
        int x = DataSource.orders.FindIndex(x => x?.OrderId == UpdatedOrder.OrderId);
        if (x == -1)
            throw new NotfoundException("Order");
        DataSource.orders[x] = UpdatedOrder;
    }
    /// <summary>
    /// Deleting an order from the order list
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        for (int i = 0; i < DataSource.orders.Count(); i++)
        {
            if (DataSource.orders[i]?.OrderId == removeById)
            {
                DataSource.orders.Remove(DataSource.orders[i]);
                return;
            }
        }
        throw new NotfoundException("Order");
    }
    public Order? GetByCondition(Func<Order?, bool>? condition)
    {
        Order? NewOrder = DataSource.orders.Find(x => condition!(x!.Value));
        if (NewOrder == null)
            throw new NotfoundException("Order");
        return NewOrder;
    }

    /// <summary>
    /// Returning the order list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? condition=null)
    {
        IEnumerable<Order?> orderReturn;
        if (condition == null)
        {
            orderReturn = new List<Order?>();
            for (int i = 0; i < DataSource.orders.Count(); i++) 
                orderReturn.Append(DataSource.orders[i]);
            return orderReturn;
        }
        return orderReturn = from item in DataSource.orders
                             where condition(item) == true
                             select item;
    }
}
