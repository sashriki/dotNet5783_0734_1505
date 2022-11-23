using BlApi;
using BO;
using Dal;
using DalApi;
using DO;
namespace BlImplementation;

internal class Order :IOrder
{
    public IDal Dal = new DalList();
    public IEnumerable<BO.OrderForList> GetAllToManager()
    {
        IEnumerable<DO.Order> OrderList = Dal.Iorder.GetAll();
        IEnumerable<BO.OrderForList> OrdersList= from item in OrderList
                                                 select ConversionFromDoToBo(item);
        return OrdersList;
    }
    public BO.Order GetOrder(int IdOrder)
    {
        DO.Order orderToGet=new DO.Order();
        if (IdOrder < 0)
            throw new InvalidInputBO($"Order id: {IdOrder} invalid input.\n");
        try
        {
            orderToGet = Dal.Iorder.GetById(IdOrder);
        }
        catch(NotfoundException ex)
        {
            Console.WriteLine(ex);
        }
        return ConversionOrder(orderToGet); 
    }
    public BO.Order ShippingUpdateToManager(int IdOrder)
    {
        DO.Order ordDO = new DO.Order();
        BO.Order ordBO = new BO.Order();
        try
        {
            ordDO = Dal.Iorder.GetById(IdOrder);
        }
        catch (NotfoundException ex)
        {
            Console.WriteLine(ex);
        }
        if(ordDO.ShipDate == DateTime.MinValue)
            throw new RepeatedUpdateBO($"Ship Date for Order ID: {IdOrder} already updated.\n")
        ordDO.ShipDate = DateTime.Now;//לבדוק איך לעדכן את התאריך של השילוח
        Dal.Iorder.Update(ordDO);
        return (ConversionOrder(ordDO));
        //לטפל בעניין הסטטוס
    }
    public BO.Order supplyUpdateToManager(int IdOrder)
    {//עדכון אספקת הזמנה
        if (IdOrder < 0)
            throw new InvalidInputBO($"Order id: {IdOrder} invalid input.\n");
        DO.Order ordDO = new DO.Order();
        BO.Order ordBO = new BO.Order();
        try
        {
            ordDO = Dal.Iorder.GetById(IdOrder);
        }
        catch (NotfoundException ex)
        {
            Console.WriteLine(ex);
        }
        if (ordDO.ShipDate != DateTime.MinValue &&
            ordDO.DeliveryDate!= DateTime.MinValue)
            throw new RepeatedUpdateBO($"Order ID: {IdOrder} provided to the customer.\n")
        ordDO.DeliveryDate = DateTime.Now;//לבדוק איך לעדכן את התאריך של השילוח
        Dal.Iorder.Update(ordDO);
        return (ConversionOrder(ordDO));
    }
    public BO.OrderTracking OrderTracking(int IdOrder)
    {//מעקב הזמנה
        if (IdOrder < 0)
            throw new InvalidInputBO($"Order id: {IdOrder} invalid input.\n");
        DO.Order ordDO = new DO.Order();
        BO.Order ordBO = new BO.Order();
        try
        {
            ordDO = Dal.Iorder.GetById(IdOrder);
        }
        catch (NotfoundException ex)
        {
            Console.WriteLine(ex);
        }
        ordBO= ConversionOrder(ordDO);
        BO.OrderTracking Tracking = new OrderTracking();
        Tracking.orderId = ordBO.OrderId;
        Tracking.OrderStatus=ordBO.orderStatus;
        //לבדוק איך מאתחלים את הרשימה המפגרת הזאת
        //Tracking.packageProgress (ordBO.OrderDate,ordBO.orderStatus);
        // confirmed, shipped, deliveredToCostumer 
        return Tracking;
    }
    public void UpdateToManager(BO.Order updateOrd)
    {
        //????????????
    }
    public BO.Order ConversionOrder(DO.Order ordDO)
    {
        BO.Order ordBO = new BO.Order();
        ordBO.OrderId = ordDO.OrderId;
        ordBO.CustomerName = ordDO.CustomerName;
        ordBO.CustomerAdress = ordDO.CustomerAdress;
        ordBO.CustomerEmail = ordDO.CustomerEmail;
        ordBO.ShipDate = ordDO.ShipDate;
        ordBO.OrderDate = ordDO.OrderDate;
        ordBO.DeliveryDate = ordDO.DeliveryDate;
        return ordBO;
    }
    public BO.OrderForList ConversionFromDoToBo(DO.Order ord)
    {
        BO.OrderForList orderForList = new OrderForList();
        orderForList.orderId = ord.OrderId;
        orderForList.costumerName = ord.CustomerName;
        if (ord.OrderDate == DateTime.MinValue)//לבדוק!!!!!!!!!!!!
            orderForList.OrderStatus = OrderStatus.confirmed;
        else if (ord.ShipDate != DateTime.MinValue)
            orderForList.OrderStatus = OrderStatus.shipped;
        else
            orderForList.OrderStatus = OrderStatus.deliveredToCostumer;
        //orderForList.ammountOfItems=ord.
        // orderForList.finalPrice
        return orderForList;
    }
}
