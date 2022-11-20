using DO;
using System.Runtime.CompilerServices;

namespace Dal;

public class DalOrderItem
{
    //create
    public int AddOrderItem(OrderItem NewOrderItem)
    {
        NewOrderItem.OrderItemId = DataSource.Config.orderItemIndex++;
        DataSource.orderItems.Add(NewOrderItem);
        return NewOrderItem.OrderItemId;
    }

    //request all
    public List<OrderItem> GetOrderItem()
    {
        List<OrderItem> orderItemReturnList = new List<OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)   //warning??
            orderItemReturnList.Add( DataSource.orderItems[i]);
        return orderItemReturnList;
    }

    //Request By Id of product and order
    public OrderItem GetbyIdOfProductAndOrder(int idOrder, int idProduct)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderId == idOrder&& DataSource.orderItems[i].ProductId== idProduct)
                return DataSource.orderItems[i];

        throw new Exception($"order item:\n order id: {idOrder} and\n product id: { idProduct }\n  is not found in order items.\n");                   
    }

    //Request By ID of order item
    public OrderItem GetByOrderItemId(int idOrderItem)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderItemId == idOrderItem)
                return DataSource.orderItems[i];

        throw new Exception($"order item number: {idOrderItem} is not found in order items.\n");
    }


    //update
    public void UpdateOrderItem(OrderItem UpdatedOrderItem)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderItemId == UpdatedOrderItem.OrderItemId)
            {
                DataSource.orderItems[i] = UpdatedOrderItem;
                return;
            }
        throw new Exception($"order items id {UpdatedOrderItem.OrderItemId} is not found in order items.");
    }

    //delete
     public void DeleteOrderItem(int removeById)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderItemId == removeById)
            {
                DataSource.orderItems.Remove(DataSource.orderItems[i]);
                return;
            }
        throw new Exception($"order items id {removeById} is not found in order items.");
    }
}

