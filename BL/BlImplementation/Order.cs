using BO;
using System.Data;

internal class Order : BlApi.IOrder
{
    public DalApi.IDal Dal = new Dal.DalList();
    /// <summary>
    /// To return all orders for the manager
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.NoElementsException"></exception>
    public IEnumerable<BO.OrderForList> GetAllToManager()
    {
        IEnumerable<DO.Order> OrderList = Dal.Iorder.GetAll();
        IEnumerable<BO.OrderForList> OrdersList = from item in OrderList
                                                  select DO_orderToBO_OrderForList(item);
        if (!OrdersList.Any())
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
        if (ordDO.ShipDate != null)
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
        if (ordDO.DeliveryDate != null)
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
            OrderId = ordDO.OrderId,
            OrderStatus = getStatus(ordDO),
            PackageProgress = new List<(DateTime?, BO.OrderStatus?)>
            {
                 (ordDO.OrderDate.Value, BO.OrderStatus.Confirmed),
                 (ordDO.ShipDate.Value, BO.OrderStatus.Shipped),
                 (ordDO.DeliveryDate.Value, BO.OrderStatus.DeliveredToCostumer)
            }
        };
    }
    private BO.OrderStatus getStatus(DO.Order ordDO)
    {
        return ordDO.DeliveryDate != null ? BO.OrderStatus.DeliveredToCostumer :
            ordDO.ShipDate != null ? BO.OrderStatus.Shipped : BO.OrderStatus.Confirmed;
    }

    public void UpdateToManager(BO.Order updateOrd ,int IdProduct,int Amount)
    {
        if (updateOrd.ShipDate != DateTime.MinValue)
            throw new InvalidAction("change order");
        BO.OrderItem? ordBO = updateOrd.OrderItems.
            Where(od => od.ProductId == IdProduct).First();
        if(ordBO!=null)
        {            
            if (Amount != 0)
            {
                int dif = ordBO.AmountOfProduct - Amount;
                ordBO.FinalPriceOfProduct += dif * ordBO.PriceOfProduct;
                ordBO.AmountOfProduct = Amount;
                updateOrd.FinalPrice += dif * ordBO.PriceOfProduct;
            }
            else 
                updateOrd.OrderItems.ToList().Remove(ordBO);
        }
        else
        {
            if (Amount == 0)
                throw new InvalidAction("Added 0 products to order");
            DO.Product proDO = new DO.Product();
            try
            {
                proDO = Dal.IProduct.GetById(IdProduct);
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
    public BO.OrderItem DO_orderItemToBO_OrderItem(DO.OrderItem ord)
    {
        BO.OrderItem orderItem = new BO.OrderItem();
        orderItem.OrderItemId = ord.OrderItemId;
        orderItem.ProductId = ord.ProductId;
        orderItem.PriceOfProduct = ord.Price;
        orderItem.AmountOfProduct = ord.Amount;
        orderItem.FinalPriceOfProduct = ord.Price * ord.Amount;
        IEnumerable<DO.Product> products = Dal.IProduct.GetAll();
        IEnumerable<string> productName = from item in products
                                          where item.ProductId == ord.ProductId
                                          select item.ProductName;
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
            ordBO.OrderStatus = BO.OrderStatus.DeliveredToCostumer;
        else if (ordDO.ShipDate != null)
            ordBO.OrderStatus = BO.OrderStatus.Shipped;
        else
            ordBO.OrderStatus = BO.OrderStatus.Confirmed;
        IEnumerable<DO.OrderItem> itemsInOrder = Dal.Iorderitem.GetAll();
        IEnumerable<BO.OrderItem> orderItems = from item in itemsInOrder
                                               where item.OrderId == ordDO.OrderId
                                               select DO_orderItemToBO_OrderItem(item);
        ordBO.OrderItems = orderItems;
        return ordBO;
    }
    public BO.OrderForList DO_orderToBO_OrderForList(DO.Order ord)
    {
        BO.OrderForList orderForList = new BO.OrderForList();
        orderForList.OrderId = ord.OrderId;  //id
        orderForList.CostumerName = ord.CustomerName;  //name
        IEnumerable<DO.OrderItem> OrderItemList = Dal.Iorderitem.GetAll();
        IEnumerable<double> count = from item in OrderItemList
                                    where item.OrderId == ord.OrderId
                                    select item.Price;
        orderForList.AmountOfItems = count.Count();   //amount
        IEnumerable<double> prices = count.ToList();
        foreach (var item in prices)
            orderForList.FinalPrice += item;  //Price
        if (ord.DeliveryDate != null)  //status
            orderForList.OrderStatus = BO.OrderStatus.DeliveredToCostumer;
        else if (ord.ShipDate != null)
            orderForList.OrderStatus = BO.OrderStatus.Shipped;
        else
            orderForList.OrderStatus = BO.OrderStatus.Confirmed;
        return orderForList;
    }

}

