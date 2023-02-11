using BO;
using DO;
using System.Data;

internal class Order : BlApi.IOrder
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// To return all orders for the manager
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NoElementsException"></exception>
    public IEnumerable<BO.OrderForList?> GetAllToManager()
    {
        IEnumerable<DO.Order?> OrderList = dal?.Order.GetAll()!;
        IEnumerable<BO.OrderForList> OrdersList = from DO.Order item in OrderList
                                                   select new BO.OrderForList
                                                   {
                                                       OrderId=item.OrderId,
                                                       CostumerName= item.CustomerName,
                                                       OrderStatus= getStatus(item),
                                                       AmountOfItems = dal.OrderItem.GetAll(x=> x?.OrderId == item.OrderId).Sum(x=>x?.Amount) ?? 0,
                                                       FinalPrice= dal.OrderItem.GetAll(x => x?.OrderId == item.OrderId).Sum(x => x?.Price* x?.Amount) ?? 0                                                      
                                                   };
        if (!OrdersList.Any())
            throw new BO.NoElementsException("orders");
        return OrdersList;
    }

    private BO.OrderStatus getStatus(DO.Order ordDO)
    {
        return ordDO.DeliveryDate != null ? BO.OrderStatus.Delivered :
            ordDO.ShipDate != null ? BO.OrderStatus.Shipped : BO.OrderStatus.Confirmed;
    }

    public BO.Order GetOrderByID(int IdOrder)
    {
        DO.Order orderToGet = new DO.Order();
        if (IdOrder < 0)
            throw new BO.InvalidInputBO($"Order id");
        try
        {
            orderToGet = dal.Order.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        return DO_orderToBO_order(orderToGet);
    }

    public int getTheOldOne()
    {
        DO.Order order = new DO.Order();
        IEnumerable<DO.Order?> shippedOrOrdered = dal?.Order.GetAll(x => x?.DeliveryDate == null)!;
        IEnumerable<DO.Order?> orderedOrders = from item in shippedOrOrdered
                                               where item?.ShipDate == null
                                               select item;
        IEnumerable<DO.Order?> shippedOrders = from item in shippedOrOrdered
                                               where item?.ShipDate != null
                                               select item;
        DO.Order? x = orderedOrders.MinBy(x => x?.OrderDate);
        DO.Order? y = shippedOrders.MinBy(x => x?.ShipDate);
        if (x?.OrderDate < y?.ShipDate)
            return (int)(x?.OrderId)!;
        else
            return (int)(y?.OrderId)!;

    }

    public BO.Order ShippingUpdateToManager(int IdOrder)
    {
        if (IdOrder < 0)
            throw new BO.InvalidInputBO($"Order id");
        DO.Order ordDO = new DO.Order();
        try
        {
            ordDO = dal.Order.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        if (ordDO.ShipDate == null)
        {
            ordDO.ShipDate = DateTime.Now;
            dal?.Order.Update(ordDO);
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
            ordDO = dal.Order.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        if(ordDO.ShipDate == null)
        {
            throw new IncorrectSupplyUpdate();
        }
        if (ordDO.DeliveryDate == null)
        {
            ordDO.DeliveryDate = DateTime.Now;
            dal?.Order.Update(ordDO);
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
            ordDO = dal.Order.GetById(IdOrder);
        }
        catch (Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        return new BO.OrderTracking
        {
            OrderId = ordDO.OrderId,
            OrderStatus = getStatus(ordDO),
            PackageProgress = new List<Tuple<DateTime?, BO.OrderStatus?>>
            { 
            new Tuple<DateTime?, OrderStatus?>(ordDO.OrderDate, BO.OrderStatus.Confirmed),
            new Tuple<DateTime?, OrderStatus?>(ordDO.ShipDate, BO.OrderStatus.Shipped),
            new Tuple<DateTime?, OrderStatus?>(ordDO.DeliveryDate, BO.OrderStatus.Delivered)
            }
        };
    }
    public void UpdateToManager(BO.Order updateOrd ,int IdProduct,int Amount)
    {
        if (updateOrd.ShipDate != null)
            throw new InvalidAction("change order");
        BO.OrderItem? ordBO = updateOrd.OrderItems.
            Where(od => od?.ProductId == IdProduct).FirstOrDefault();
        DO.Product product=dal!.Product.GetById(IdProduct);
        if (product.AmountInStock < Amount)
            throw new ItemMissingException(IdProduct.ToString(), product.AmountInStock);
        if (ordBO!=null)
        {
            if (Amount != 0)
            {
                int dif = ordBO.AmountOfProduct - Amount;
                ordBO.FinalPriceOfProduct += dif * ordBO.PriceOfProduct;
                ordBO.AmountOfProduct = Amount;
                updateOrd.FinalPrice += dif * ordBO.PriceOfProduct;
                dal!.OrderItem.Update(new DO.OrderItem  
                {
                    OrderItemId= ordBO.OrderItemId,
                    OrderId= updateOrd.OrderId,
                    ProductId= ordBO.ProductId,
                    Price= ordBO.PriceOfProduct,
                    Amount= ordBO.AmountOfProduct
                });
                dal.Order.Update(new DO.Order
                {
                    OrderId= updateOrd.OrderId,
                    CustomerAdress= updateOrd.CustomerAdress,
                    CustomerEmail= updateOrd.CustomerEmail,
                    CustomerName= updateOrd.CustomerName,
                    DeliveryDate= updateOrd.DeliveryDate,
                    OrderDate= updateOrd.OrderDate, 
                    ShipDate= updateOrd.ShipDate
                });
            }
            else
            {
                updateOrd.OrderItems.ToList().Remove(ordBO);
                dal!.OrderItem.Delete(ordBO.OrderItemId);
            }
        }
        else
        {
            if (Amount == 0)
                throw new InvalidAction("Added 0 products to order");
            DO.Product proDO = new DO.Product();
            try
            {
                proDO = dal.Product.GetById(IdProduct);
            }
            catch(Exception ex)
            {
                throw new BO.BONotfoundException(ex);
            }
            BO.OrderItem newOrdItem = new BO.OrderItem();
            newOrdItem.ProductId = IdProduct;
            newOrdItem.AmountOfProduct = Amount;
            newOrdItem.PriceOfProduct = proDO.ProductPrice;
            newOrdItem.FinalPriceOfProduct = Amount * proDO.ProductPrice;
            newOrdItem.ProductName = proDO.ProductName;
            updateOrd.OrderItems.Append(newOrdItem);           
        }
        return;
    }
    public BO.OrderItem DO_orderItemToBO_OrderItem(DO.OrderItem? ord)
    {
        BO.OrderItem orderItem = new BO.OrderItem();
        orderItem.OrderItemId = ord?.OrderItemId ?? 0;
        orderItem.ProductId = ord?.ProductId ?? 0;
        orderItem.PriceOfProduct = ord?.Price ?? 0;
        orderItem.AmountOfProduct = ord?.Amount ?? 0;
        orderItem.FinalPriceOfProduct = (ord?.Price * ord?.Amount) ?? 0;
        IEnumerable<DO.Product?> products = dal!.Product.GetAll();
        IEnumerable<string> productName = from item in products
                                          where item?.ProductId == ord?.ProductId
                                          select item?.ProductName;
        orderItem.ProductName = productName.ToArray()[0];
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
        if (ordDO.DeliveryDate != null)  //status
            ordBO.OrderStatus = BO.OrderStatus.Delivered;
        else if (ordDO.ShipDate != null)
            ordBO.OrderStatus = BO.OrderStatus.Shipped;
        else
            ordBO.OrderStatus = BO.OrderStatus.Confirmed;
        IEnumerable<DO.OrderItem?> itemsInOrder = dal.OrderItem.GetAll();
        int totalPrice = 0;
        IEnumerable<BO.OrderItem?> orderItems = from item in itemsInOrder
                                                where item?.OrderId == ordDO.OrderId
                                                select DO_orderItemToBO_OrderItem(item);
        ordBO.OrderItems = orderItems;
        foreach (var item in orderItems)
            ordBO.FinalPrice += item!.FinalPriceOfProduct;
        return ordBO;
    }
    //public BO.OrderForList DO_orderToBO_OrderForList(DO.Order? ord)
    //{
    //    BO.OrderForList orderForList = new BO.OrderForList();
    //    orderForList.OrderId = ord?.OrderId ?? 0;  //id
    //    orderForList.CostumerName = ord?.CustomerName;  //name
    //    IEnumerable<DO.OrderItem?> OrderItemList = dal?.OrderItem.GetAll();
    //    IEnumerable<double> count = from item in OrderItemList
    //                                where item?.OrderId == ord?.OrderId
    //                                select item?.Price ?? 0;
    //    orderForList.AmountOfItems = count.Count();   //amount
    //    IEnumerable<double> prices = count.ToList();
    //    foreach (var item in prices)
    //        orderForList.FinalPrice += item;  //Price
    //    if (ord?.DeliveryDate != null)  //status
    //        orderForList.OrderStatus = BO.OrderStatus.Delivered;
    //    else if (ord?.ShipDate != null)
    //        orderForList.OrderStatus = BO.OrderStatus.Shipped;
    //    else
    //        orderForList.OrderStatus = BO.OrderStatus.Confirmed;
    //    return orderForList;
    //}

}

