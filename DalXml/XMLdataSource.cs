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

namespace Dal;

internal static class XMLdataSource
{
    static string ProductPath = @"xmlProduct.xml";
    static string OrderPath = @"xmlOrder.xml";
    static string OrderItemPath = @"xmlOrderItem.xml";
    static string ConfigPath = @"config.xml";

    static IDal dal = Factory.Get();
    static public List<Product?> products = dal.Product.GetAll().ToList();
    static public List<Order?> orders = dal.Order.GetAll().ToList();
    static public List<OrderItem?> orderItems = dal.OrderItem.GetAll().ToList();
    
    static XMLdataSource()
    {
        if(!Directory.Exists(ProductPath))
            Directory.CreateDirectory(ProductPath);
        if (!Directory.Exists(OrderPath))
            Directory.CreateDirectory(OrderPath);
        if (!Directory.Exists(OrderItemPath))
            Directory.CreateDirectory(OrderItemPath);
        if(!Directory.Exists(ConfigPath))
            Directory.CreateDirectory(ConfigPath);
    }
    public static void initialization()
    {
        Config();
        InitializationOfProducts();
        InitializationOfOrder();
        InitializationOfOrderItems();
    }
    public static void Config()
    {
        FileStream fileConfig = new FileStream(ConfigPath, FileMode.Create);
        XElement ConfigRoot = new XElement("Coinfig",
            new XElement("Order Id", 1),
            new XElement("Order Item Id", 200),
            new XElement("Product Id", 124568));
        ConfigRoot.Save(fileConfig);
        fileConfig.Close();
    }
    public static void InitializationOfProducts()
    {
        IEnumerable<Product?> Products = from item in products                 
                                         select InitIDPro(item ?? throw new Exception());
        FileStream filePro = new FileStream(ProductPath, FileMode.Create);
        XmlSerializer x = new XmlSerializer(Products.GetType());
        x.Serialize(filePro, Products);
        filePro.Close();
    }
    public static void InitializationOfOrder()
    {
        IEnumerable<Order?> Orders = from item in orders
                                     select InitIDOrd(item ?? throw new Exception());
        FileStream fileOrd = new FileStream(OrderPath, FileMode.Create);
        XmlSerializer x = new XmlSerializer(Orders.GetType());
        x.Serialize(fileOrd, Orders);
        fileOrd.Close();
    }
    public static void InitializationOfOrderItems()
    {
        IEnumerable<OrderItem?> OrderItems = from item in orderItems
                                         select InitIDOrdItm(item ?? throw new Exception());
        FileStream fileOrdIt = new FileStream(OrderItemPath, FileMode.Create);
        XmlSerializer x = new XmlSerializer(OrderItems.GetType());
        x.Serialize(fileOrdIt, OrderItems);
        fileOrdIt.Close();
    }
    public static Product? InitIDPro(Product s)
    { 
        s.ProductId = Convert.ToInt32(XElement.Load(ConfigPath).Element("Product Id"));
        return s;
    }
    public static Order? InitIDOrd(Order s)
    { 
        s.OrderId = Convert.ToInt32(XElement.Load(ConfigPath).Element("Order Id"));
        return s;
    }
    public static OrderItem? InitIDOrdItm(OrderItem s)
    { 
        s.OrderItemId = Convert.ToInt32(XElement.Load(ConfigPath).Element("Order Item Id"));
        return s;
    }
}
