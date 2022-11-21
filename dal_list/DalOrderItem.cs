using DO;
using System.Runtime.CompilerServices;
using DalApi;


namespace Dal;

internal class DalOrderItem: Iorderitem
{
    /// <summary>
    /// Adding an ordered item to the list
    /// </summary>
    /// <param name="NewOrderItem"></param>
    /// <returns></returns>
    public int Add(OrderItem NewOrderItem)
    {
        NewOrderItem.OrderItemId = DataSource.Config.orderItemIndex++;
        DataSource.orderItems.Add(NewOrderItem);
        return NewOrderItem.OrderItemId;
    }

    /// <summary>
    /// Returning the list of items in the order
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> GetAll()
    {
        List<OrderItem> orderItemReturnList = new List<OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)   //warning??
            orderItemReturnList.Add( DataSource.orderItems[i]);
        return orderItemReturnList;
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
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderId == idOrder&& DataSource.orderItems[i].ProductId== idProduct)
                return DataSource.orderItems[i];

        throw new Exception($"order item:\n order id: {idOrder} and\n product id: { idProduct }\n  is not found in order items.\n");                   
    }

    /// <summary>
    /// Returning an item in an order according to the ID number of an item in the order
    /// </summary>
    /// <param name="idOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public OrderItem GetById(int idOrderItem)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderItemId == idOrderItem)
                return DataSource.orderItems[i];
        throw new NotfoundException($"Order Item ID: {idOrderItem} not found in order items.\n");
    }

    /// <summary>
    /// Update an item in the order
    /// </summary>
    /// <param name="UpdatedOrderItem"></param>
    /// <exception cref="Exception"></exception>
    public void Update(OrderItem UpdatedOrderItem)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderItemId == UpdatedOrderItem.OrderItemId)
            {
                DataSource.orderItems[i] = UpdatedOrderItem;
                return;
            }
        throw new NotfoundException($"Order Item ID: {UpdatedOrderItem.OrderItemId} not found in order items.\n");
    }

    /// <summary>
    /// Deleting an item in the order
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderItemId == removeById)
            {
                DataSource.orderItems.Remove(DataSource.orderItems[i]);
                return;
            }
        throw new NotfoundException($"Order Item ID: {removeById} not found in order items.\n");
    }
}

