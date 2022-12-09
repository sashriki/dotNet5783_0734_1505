using DO;
using DalApi;
using static Dal.DataSource;
namespace Dal;

internal class DalOrder : Iorder
{
    /// <summary>
    /// Creating a new order and adding it to the list of orders
    /// </summary>
    /// <param name="NewOrder"></param>
    /// <returns></returns>
    public int Add(Order NewOrder)
    {
        NewOrder.OrderId = Config.orderIndex;
        int x = orders.FindIndex(x => x?.OrderId == NewOrder.OrderId);
        if (x != -1)
            throw new DuplicationException("Order");
        orders.Add(NewOrder);
        return NewOrder.OrderId;
    }
    /// <summary>
    /// Returning an order from the order list
    /// </summary>
    /// <param name="idOrder"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Order GetById(int idOrder)
    {
        Order order = new Order();
        int x = DataSource.orders.FindIndex(x => x?.OrderId == idOrder);
        if (x == -1)
            throw new NotfoundException("Order");
        order = orders[x] ?? throw new NotfoundException("Order");
        return order;
    }
    /// <summary>
    /// Updating an order in the order list
    /// </summary>
    /// <param name="UpdatedOrder"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order UpdatedOrder)
    {
        int x = orders.FindIndex(x => x?.OrderId == UpdatedOrder.OrderId);
        if (x == -1)
            throw new NotfoundException("Order");
        orders[x] = UpdatedOrder;
    }
    /// <summary>
    /// Deleting an order from the order list
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        for (int i = 0; i < orders.Count(); i++)
        {
            if (orders[i]?.OrderId == removeById)
            {
                orders.Remove(orders[i]);
                return;
            }
        }
        throw new NotfoundException("Order");
    }
    public Order GetByCondition(Func<Order?, bool>? condition)
    {
        Order NewOrder = orders.Find(x => condition(x)) ?? throw new NotfoundException("Order");
        return NewOrder;
    }

    /// <summary>
    /// Returning the order list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? condition = null)
    {
        IEnumerable<Order?> orderReturn;
        if (condition == null)
        {
            orderReturn = new List<Order?>();
            for (int i = 0; i < orders.Count(); i++)
                orderReturn.Append(orders[i]);
            return orderReturn;
        }
        return orderReturn = from item in orders
                             where condition(item) == true
                             select item;
    }
}
