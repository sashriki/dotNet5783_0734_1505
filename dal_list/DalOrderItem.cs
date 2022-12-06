using DalApi;
using DO;
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
        NewOrderItem.OrderItemId = DataSource.Config.orderItemIndex++;
        int x = DataSource.orderItems.FindIndex(x => x?.OrderItemId == NewOrderItem.OrderItemId);
        if (x != -1)
            throw new DuplicationException("orderItem");
        DataSource.orderItems.Add(NewOrderItem);
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
            for (int i = 0; i < DataSource.orderItems.Count(); i++)   //warning??
                orderItemReturn.Append(DataSource.orderItems[i]);
            return orderItemReturn;
        }
        return orderItemReturn =from item in DataSource.orderItems
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
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i]?.OrderId == idOrder && DataSource.orderItems[i]?.ProductId == idProduct)
            {
                orderItem= DataSource.orderItems[i].Value;
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
        OrderItem orderItem = new OrderItem();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i]?.OrderItemId == idOrderItem)
            { 
                orderItem= DataSource.orderItems[i].Value;
                return orderItem;
            }
        throw new NotfoundException("Order Item");
    }
    /// <summary>
    /// Update an item in the order
    /// </summary>
    /// <param name="UpdatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem UpdatedOrderItem)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i]?.OrderItemId == UpdatedOrderItem.OrderItemId)
            {
                DataSource.orderItems[i] = UpdatedOrderItem;
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
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i]?.OrderItemId == removeById)
            {
                DataSource.orderItems.Remove(DataSource.orderItems[i]);
                return;
            }
        throw new NotfoundException("Order Item");
    }
    public OrderItem GetByCondition(Func<OrderItem?, bool>? condition)
    {
        OrderItem? NewOrderItem = DataSource.orderItems.Find(x => condition(x));
        if (NewOrderItem == null)
            throw new NotfoundException("Order Item");
        return NewOrderItem.Value;
    }
}

