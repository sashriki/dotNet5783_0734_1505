using DO;

namespace Dal;

public struct DalOrderItem
{
    //create
    int AddOrderItem(OrderItem NewOrderItem)
    {
        DataSource.orderItems.Add(NewOrderItem);
        return NewOrderItem.OrderItemId;
    }

    //request all
    public List<OrderItem> GetOrderItem()
    {
        List<OrderItem> orderItemReturnList = new List<OrderItem>();
        for (int i = 0; i < DataSource.orderItems.Count(); i++)   //warning??
            orderItemReturnList[i] = DataSource.orderItems[i];
        return orderItemReturnList;
    }

    //Request By Id
    public OrderItem GetOrderItemId(int idOrderItem)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.orderItems[i].OrderItemId == idOrderItem)
                return DataSource.orderItems[i];

        throw new Exception($"order items id {idOrderItem} is not found in order items.");
    }

    //update
    void UpdateOrderItem(OrderItem UpdatedOrderItem)
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
    void DeleteOrderItem(int removeById)
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

