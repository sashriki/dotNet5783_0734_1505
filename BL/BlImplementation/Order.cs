internal class Order : BlApi.IOrder
{
    public DalApi.IDal Dal = new Dal.DalList();
    public IEnumerable<BO.OrderForList> GetAllToManager()
    {
        IEnumerable<DO.Order> OrderList = Dal.Iorder.GetAll();
        IEnumerable<BO.OrderForList> OrdersList = from item in OrderList
                                                  select DO_orderToBO_OrderForList(item);
        if (OrdersList.Any())
            throw new BO.NoElementsException("orders");
        return OrdersList;
    }
    public BO.Order GetOrderByID(int IdOrder)
    {
        DO.Order orderToGet = new DO.Order();
        if (IdOrder < 0)
            throw new BO.InvalidInputBO($"Order id");
        try
        {
            orderToGet = Dal.Iorder.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        return DO_orderToBO_order(orderToGet);
    }
    public BO.Order ShippingUpdateToManager(int IdOrder)
    {
        if(IdOrder<0)
            throw new BO.InvalidInputBO($"Order id");
        DO.Order ordDO = new DO.Order();
        try
        {
            ordDO = Dal.Iorder.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        if (ordDO.ShipDate != DateTime.MinValue)
        {
            ordDO.ShipDate = DateTime.Now;
            Dal.Iorder.Update(ordDO);
        }
        return DO_orderToBO_order(ordDO);
    }
    public BO.Order supplyUpdateToManager(int IdOrder)
    {
        if (IdOrder < 0)
            throw new BO.InvalidInputBO($"Order id");
        DO.Order ordDO = new DO.Order();
        try
        {
            ordDO = Dal.Iorder.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        if (ordDO.DeliveryDate != DateTime.MinValue)
        {
            ordDO.DeliveryDate = DateTime.Now;
            Dal.Iorder.Update(ordDO);
        }
        return (DO_orderToBO_order(ordDO));
    }
    public BO.OrderTracking OrderTracking(int IdOrder)
    {
        if (IdOrder < 0)
            throw new BO.InvalidInputBO($"Order id");
        DO.Order ordDO = new DO.Order();
        try
        {
            ordDO = Dal.Iorder.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex); 
        }
        return new BO.OrderTracking
        {
            orderId = ordDO.OrderId,
            OrderStatus = getStatus(ordDO),
            packageProgress = new List<(DateTime, BO.OrderStatus)>
            {
                 (ordDO.OrderDate.Value, BO.OrderStatus.confirmed),
                 (ordDO.ShipDate.Value, BO.OrderStatus.shipped),
                 (ordDO.DeliveryDate.Value, BO.OrderStatus.deliveredToCostumer)
            }
        }; 
    }
    private BO.OrderStatus getStatus(DO.Order ordDO)
    {
        return ordDO.DeliveryDate != DateTime.MinValue ? BO.OrderStatus.deliveredToCostumer :
            ordDO.ShipDate != DateTime.MinValue ? BO.OrderStatus.shipped : BO.OrderStatus.confirmed;
    }
    //perfect
    public void UpdateToManager(BO.Order updateOrd)
    {
        //בונוס
        return;
    }
    //המרות
    public BO.orderItem DO_orderItemToBO_OrderItem(DO.OrderItem ord)
    {
        BO.orderItem orderItem = new BO.orderItem();
        orderItem.orderItemId = ord.OrderItemId;
        orderItem.productId = ord.ProductId;
        orderItem.priceOfProduct = ord.Price;
        orderItem.amountOfProduct = ord.Amount;
        orderItem.finalPriceOfProduct = ord.Price * ord.Amount;
        IEnumerable<DO.Product> products = Dal.IProduct.GetAll();
        IEnumerable<string> productName = from item in products
                                          where item.ProductId == ord.ProductId
                                          select item.ProductName;
        orderItem.productName = productName.ToArray()[0];
        return orderItem;
    }
    public BO.Order DO_orderToBO_order(DO.Order ordDO)
    {
        BO.Order ordBO = new BO.Order();
        ordBO.OrderId = ordDO.OrderId;
        ordBO.CustomerName = ordDO.CustomerName;
        ordBO.CustomerAdress = ordDO.CustomerAdress;
        ordBO.CustomerEmail = ordDO.CustomerEmail;
        ordBO.ShipDate = ordDO.ShipDate;
        ordBO.OrderDate = ordDO.OrderDate;
        ordBO.DeliveryDate = ordDO.DeliveryDate;
        if (ordDO.DeliveryDate != DateTime.MinValue)  //status
            ordBO.orderStatus = BO.OrderStatus.deliveredToCostumer;
        else if (ordDO.ShipDate != DateTime.MinValue)
            ordBO.orderStatus = BO.OrderStatus.shipped;
        else
            ordBO.orderStatus = BO.OrderStatus.confirmed;
        IEnumerable<DO.OrderItem> itemsInOrder = Dal.Iorderitem.GetAll();
        IEnumerable<BO.orderItem> orderItems = from item in itemsInOrder
                                               where item.OrderId == ordDO.OrderId
                                               select DO_orderItemToBO_OrderItem(item);
        ordBO.orderItems = orderItems;
        return ordBO;
    }
    public BO.OrderForList DO_orderToBO_OrderForList(DO.Order ord)
    {
        BO.OrderForList orderForList = new BO.OrderForList();
        orderForList.orderId = ord.OrderId;  //id
        orderForList.costumerName = ord.CustomerName;  //name
        IEnumerable<DO.OrderItem> OrderItemList = Dal.Iorderitem.GetAll();
        IEnumerable<double> count = from item in OrderItemList
                                    where item.OrderId == ord.OrderId
                                    select item.Price;
        orderForList.amountOfItems = count.Count();   //amount
        List<double> prices = count.ToList();
        foreach (var item in prices) orderForList.finalPrice += item;  //price
        if (ord.DeliveryDate != DateTime.MinValue)  //status
            orderForList.OrderStatus = BO.OrderStatus.deliveredToCostumer;
        else if (ord.ShipDate != DateTime.MinValue)
            orderForList.OrderStatus = BO.OrderStatus.shipped;
        else
            orderForList.OrderStatus = BO.OrderStatus.confirmed;

        return orderForList;
    }

}

