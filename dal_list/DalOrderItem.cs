using DalApi;
using DO;
namespace Dal;
using static Dal.DataSource;

internal class DalOrderItem : Iorderitem
{
    /// <summary>
    /// Adding an ordered item to the list
    /// </summary>
    /// <param name="NewOrderItem"></param>
    /// <returns></returns>
    public int Add(OrderItem NewOrderItem)
    {
        NewOrderItem.OrderItemId = Config.orderItemIndex++;
        int x = orderItems.FindIndex(x => x?.OrderItemId == NewOrderItem.OrderItemId);
        if (x != -1)
            throw new DuplicationException("orderItem");
        orderItems.Add(NewOrderItem);
        return NewOrderItem.OrderItemId;
    }

    /// <summary>
    /// A function that receives a condition for filtering 
    /// and returns the list according to the condition, when there is no condition
    /// the function will return the entire list of objects.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? condition = null)
    {
        IEnumerable<OrderItem?> orderItemReturn;
        if (condition == null)
        {
            orderItemReturn = new List<OrderItem?>();
            for (int i = 0; i < orderItems.Count(); i++)   //warning??
                orderItemReturn.Append(orderItems[i]);
            return orderItemReturn;
        }
        return orderItemReturn =from item in orderItems
                                where condition(item) == true
                                select item;
    }
    /// <summary>
    /// Returning an item in an order by product ID number and order ID number
    /// </summary>
    /// <param name="idOrder"></param>
    /// <param name="idProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetbyIdOfProductAndOrder(int idOrder, int idProduct)
    {
        OrderItem orderItem = new OrderItem();
        int x = orderItems.FindIndex(x => x?.OrderId == idOrder && x?.ProductId== idProduct);
        if (x == -1)
            throw new NotfoundException("order item");
        orderItem= orderItems[x] ?? throw new NotfoundException("order item");
        return orderItem;
    }
    /// <summary>
    /// Returning an item in an order according to the ID number of an item in the order
    /// </summary>
    /// <param name="idOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetById(int idOrderItem)
    {
        OrderItem orderItem = new OrderItem();
        int x = orderItems.FindIndex(x => x?.OrderItemId == idOrderItem);
        if (x == -1)
            throw new NotfoundException("Order Item");
        orderItem= orderItems[x] ?? throw new NotfoundException("Order Item");
        return orderItem;
    }
    /// <summary>
    /// Update an item in the order
    /// </summary>
    /// <param name="UpdatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem UpdatedOrderItem)
    {
        int x = orderItems.FindIndex(x => x?.OrderItemId == UpdatedOrderItem.OrderItemId);
        if (x == -1)
            throw new NotfoundException($"Order Item");
        orderItems[x] = UpdatedOrderItem;
    }
    /// <summary>
    /// Deleting an item in the order
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        for (int i = 0; i < orderItems.Count(); i++)
            if (orderItems[i]?.OrderItemId == removeById)
            {
                orderItems.Remove(orderItems[i]);
                return;
            }
        throw new NotfoundException("Order Item");
    }
    /// <summary>
    /// Returns an object by condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    public OrderItem GetByCondition(Func<OrderItem?, bool>? condition)
    {
        int x = orderItems.FindIndex(x => condition(x));
        if (x == -1)
            throw new NotfoundException("Order Item");
        OrderItem NewOrderItem = orderItems[x] ?? throw new NotfoundException("Order Item");
        return NewOrderItem;
    }
}

