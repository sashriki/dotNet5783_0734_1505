using DalApi;
using DO;
using static Dal.DataSource;

namespace Dal;

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
    /// Returning the list of items in the order
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
        for (int i = 0; i < orderItems.Count(); i++)
            if (orderItems[i]?.OrderId == idOrder && orderItems[i]?.ProductId == idProduct)
            {
                orderItem= orderItems[i].Value;
                return orderItem;
            }
        throw new NotfoundException("order item");
    }
    /// <summary>
    /// Returning an item in an order according to the ID number of an item in the order
    /// </summary>
    /// <param name="idOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetById(int idOrderItem)
    {
        int index  = orderItems.FindIndex(x => x?.OrderItemId == idOrderItem);
        if (index == -1)
            throw new NotfoundException("Order Item");

        return orderItems[index] ?? throw new NotfoundException("Order Item");
    }
    /// <summary>
    /// Update an item in the order
    /// </summary>
    /// <param name="UpdatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem UpdatedOrderItem)
    {
        for (int i = 0; i < orderItems.Count(); i++)
            if (orderItems[i]?.OrderItemId == UpdatedOrderItem.OrderItemId)
            {
                orderItems[i] = UpdatedOrderItem;
                return;
            }
        throw new NotfoundException($"Order Item");
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
    public OrderItem GetByCondition(Func<OrderItem?, bool>? condition)
    {
        OrderItem NewOrderItem = orderItems.Find(x => condition(x)) ??
            throw new NotfoundException("Order Item");
        return NewOrderItem;
    }
}

