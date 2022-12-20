using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
using System.Xml.Serialization;
using System.Data.Common;
using DalApi;
using System.Xml.Linq;
using System.IO;

using DO;
namespace Dal;

internal static class DataSource
{
    //lists
    static public List<Product?> products = new List<Product?>();
    static public List<Order?> orders = new List<Order?>();
    static public List<OrderItem?> orderItems = new List<OrderItem?>();

    //continuous numbers
    internal static class Config
    {
        internal static int productIndex = 124568;
        internal static int orderItemIndex = 200;
        internal static int orderIndex = 1;

    }

    //randoms
    static readonly Random random = new Random(); // for the random numbers

    //consts
    const int InitialNumOfOrders = 100;
    const int InitialNumOfOrderItems = 20;
    const int InitialNumOfproducts = 200;

    //initialize
    static DataSource()
    {
        Init();
    }
    private static void Init()
    {
        InitProduct();
        InitOrder();
        InitOrderItem();
    }
    /// <summary>
    /// Product list initialization
    /// </summary>
    private static void InitProduct()
    {
        for (int i = 0; i < InitialNumOfproducts/*200*/; i++)
        {//The loop will create new products and add to the list
            Product newProduct = new Product(); //Creating an object
            newProduct.ProductId = Config.productIndex++; //ID number by running number
            newProduct.ProductName = "product" + i;
            newProduct.ProductCategory = (Category)(random.Next(0, 5)); ;
            newProduct.ProductPrice = Math.Round(random.Next(500) + random.NextDouble(), 1);
            if (random.Next(0, 100) > 5)
            {
                newProduct.AmmountInStock = random.Next(20, 100);
            }
            else
            {
                newProduct.AmmountInStock = 1;
            }
            products.Add(newProduct);
        }
    }
    private static void InitOrder()
    {//Initialization for orders
        for (int i = 0; i < InitialNumOfOrders; i++)
        {
            Order newOrder = new Order();
            newOrder.CustomerName = "Costumer " + i;
            newOrder.CustomerAdress = "Adress " + i;
            newOrder.CustomerEmail = "customer" + i + "@gmail.com";
            newOrder.OrderId = Config.orderIndex++;
            newOrder.OrderDate = DateTime.Now - new TimeSpan(random.Next(0, 5), random.Next(0, 28), random.Next(0, 12), random.Next(0, 60), random.Next(0, 60));
            if (random.Next(0, 100) < 80)
            {
                newOrder.ShipDate = newOrder.OrderDate + new TimeSpan(random.Next(0, 7), random.Next(0, 12), random.Next(0, 59), random.Next(0, 59));
                if (random.Next(0, 100) < 60)
                    newOrder.DeliveryDate = newOrder.ShipDate + new TimeSpan(random.Next(0, 14), random.Next(0, 12), random.Next(0, 59), random.Next(0, 59));
                else
                    newOrder.DeliveryDate = null;
            }

            else
            {
                newOrder.ShipDate = null;
                newOrder.DeliveryDate = null;
            }
            orders.Add(newOrder);
        }
    }
    private static void InitOrderItem()
    {
        for (int i = 0; i < 0.2 * InitialNumOfOrders; i++)
        {
            OrderItem newOrderItem = new OrderItem();
            newOrderItem.OrderItemId = Config.orderItemIndex++;
            newOrderItem.OrderId = orders[random.Next(0, InitialNumOfOrders)].Value.OrderId;
            int index = random.Next(0, InitialNumOfproducts);
            newOrderItem.ProductId = products[index].Value.ProductId;
            newOrderItem.Amount = random.Next(0, 5);
            newOrderItem.Price = products[index].Value.ProductPrice;
            orderItems.Add(newOrderItem);
        }
    }
}
//לטפל בעדכון מספר רץ
//namespace Dal;

//internal static class XMLdataSource
//{
//    static string ProductPath = @"xmlProduct.xml";
//    static string OrderPath = @"xmlOrder.xml";
//    static string OrderItemPath = @"xmlOrderItem.xml";
//    static string ConfigPath = @"config.xml";

//    static IDal dal = Factory.Get();
//    static public List<Product?> products = dal.Product.GetAll().ToList();
//    static public List<Order?> orders = dal.Order.GetAll().ToList();
//    static public List<OrderItem?> orderItems = dal.OrderItem.GetAll().ToList();

//    static XMLdataSource()
//    {
//        if(!Directory.Exists(ProductPath))
//            Directory.CreateDirectory(ProductPath);
//        if (!Directory.Exists(OrderPath))
//            Directory.CreateDirectory(OrderPath);
//        if (!Directory.Exists(OrderItemPath))
//            Directory.CreateDirectory(OrderItemPath);
//        if(!Directory.Exists(ConfigPath))
//            Directory.CreateDirectory(ConfigPath);
//    }
//    public static void initialization()
//    {
//        Config();
//        InitializationOfProducts();
//        InitializationOfOrder();
//        InitializationOfOrderItems();
//    }
//    public static void Config()
//    {
//        FileStream fileConfig = new FileStream(ConfigPath, FileMode.Create);
//        XElement ConfigRoot = new XElement("Coinfig",
//            new XElement("Order Id", 1),
//            new XElement("Order Item Id", 200),
//            new XElement("Product Id", 124568));
//        ConfigRoot.Save(fileConfig);
//        fileConfig.Close();
//    }
//    public static void InitializationOfProducts()
//    {
//        IEnumerable<Product?> Products = from item in products                 
//                                         select InitIDPro(item ?? throw new Exception());
//        FileStream filePro = new FileStream(ProductPath, FileMode.Create);
//        XmlSerializer x = new XmlSerializer(Products.GetType());
//        x.Serialize(filePro, Products);
//        filePro.Close();
//    }
//    public static void InitializationOfOrder()
//    {
//        IEnumerable<Order?> Orders = from item in orders
//                                     select InitIDOrd(item ?? throw new Exception());
//        FileStream fileOrd = new FileStream(OrderPath, FileMode.Create);
//        XmlSerializer x = new XmlSerializer(Orders.GetType());
//        x.Serialize(fileOrd, Orders);
//        fileOrd.Close();
//    }
//    public static void InitializationOfOrderItems()
//    {
//        IEnumerable<OrderItem?> OrderItems = from item in orderItems
//                                         select InitIDOrdItm(item ?? throw new Exception());
//        FileStream fileOrdIt = new FileStream(OrderItemPath, FileMode.Create);
//        XmlSerializer x = new XmlSerializer(OrderItems.GetType());
//        x.Serialize(fileOrdIt, OrderItems);
//        fileOrdIt.Close();
//    }
//    public static Product? InitIDPro(Product s)
//    {
//        int ID =  Convert.ToInt32(XElement.Load(ConfigPath).Element("Product Id"));
//        s.ProductId =ID;
//        ID++;
//        XElement config = XElement.Load(ConfigPath);
//        config.Element("Order Id").SetValue(ID);
//        config.Save(ConfigPath);
//        return s;
//    }
//    public static Order? InitIDOrd(Order s)
//    { 
//        s.OrderId = Convert.ToInt32(XElement.Load(ConfigPath).Element("Order Id"));
//        return s;
//    }
//    public static OrderItem? InitIDOrdItm(OrderItem s)
//    { 
//        s.OrderItemId = Convert.ToInt32(XElement.Load(ConfigPath).Element("Order Item Id"));
//        return s;
//    }
//}
