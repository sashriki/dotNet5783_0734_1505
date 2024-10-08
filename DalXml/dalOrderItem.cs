﻿using DalApi;
namespace Dal;
using DO;
using System.IO;
using System.Linq;
using System.Xml.Linq;

internal class dalOrderItem : IOrderItem
{
    string path = "xmlOrderItem.xml";
    string configPath = @"..\xml\config.xml";
    string dir = @"..\xml\";
    XElement? ordersItemsRoot;

    public dalOrderItem()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(dir + path))
                ordersItemsRoot = XElement.Load(dir + path);
            else
            {
                ordersItemsRoot = new XElement("ordersItems");
                ordersItemsRoot.Save(dir + path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("product File upload problem" + ex.Message);
        }
    }
    /// <summary>
    /// Adding an ordered item to the list
    /// </summary>
    /// <param name="objToAdd"></param>
    /// <returns></returns>
    /// <exception cref="DuplicationException"></exception>
    public int Add(OrderItem objToAdd)
    {
        XElement configRoot = XElement.Load(configPath);
        int nextSeqNum = Convert.ToInt32(configRoot.Element("orderItemSeq")!.Value);
        nextSeqNum++;
        objToAdd.OrderItemId = nextSeqNum;
        configRoot.Element("orderItemSeq")!.SetValue(nextSeqNum);
        configRoot.Save(configPath);

        //Adding an ordered item to the list
        XElement newOrderItem = ToolsXML.itemToXelement(objToAdd, "OrderItem");

        ordersItemsRoot!.Add(newOrderItem);
        ordersItemsRoot.Save(dir + path);

        return objToAdd.OrderId;
    }

    /// <summary>
    /// Deletion in an ordered item from the list
    /// </summary>
    /// <param name="objToDelete"></param>
    /// <exception cref="NotfoundException"></exception>
    public void Delete(int objToDelete)
    {
        //Reading the data from the file into a list
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        //Finding the index of the requested object
        int x = OrderItemLst.FindIndex(x => x.OrderItemId == objToDelete);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order Item");
        //Deleting the object
        OrderItemLst.RemoveAt(x);
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(OrderItemLst, path);
    }

    /// <summary>
    /// Update an item in the order
    /// </summary>
    /// <param name="objToUpdate"></param>
    /// <exception cref="NotfoundException"></exception>
    public void Update(OrderItem objToUpdate)
    {
        //Reading the data from the file into a list
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        //Finding the index of the requested object
        int x = OrderItemLst.FindIndex(x => x.OrderItemId == objToUpdate.OrderItemId);
        //throw an exception to the close that the object does not exist
        if (x == -1)       
            throw new NotfoundException("Order Item");
        //Update the object     
        OrderItemLst[x] = objToUpdate;
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(OrderItemLst, path);
        return;
    }

    /// <summary>
    /// The method will return an item in the order by ID
    /// </summary>
    /// <param name="objToGet"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    public OrderItem GetById(int objToGet)
    {
        //Reading the data from the file into a list
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        //Finding the index of the requested object
        int x = OrderItemLst.FindIndex(x => x.OrderItemId == objToGet);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order Item");
        OrderItem orderItem= OrderItemLst[x];
        return orderItem;
    }

    /// <summary>
    /// The method will return an item in the order according to a condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    public OrderItem GetByCondition(Func<OrderItem?, bool>? condition)
    {
        //Reading the data from the file into a list
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        //Finding the index of the requested object
        int x = OrderItemLst.FindIndex(x => condition(x) == true);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order Item");

        OrderItem orderItem = OrderItemLst[x];
        return orderItem;
    }

    /// <summary>
    /// The method will return a collection of items ordered by condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? condition = null)
    {
        //Reading the data from the file into a list
        List<DO.OrderItem?> OrderList = ToolsXML.LoadListFromXMLSerializer<DO.OrderItem?>(path);
        //When there is no condition we will return the entire list
        if (condition == null)
            return OrderList.AsEnumerable().OrderByDescending(p => p?.OrderItemId);

        return OrderList.Where(condition).OrderByDescending(p => p?.OrderItemId);
    }

    /// <summary>
    /// The method will return an item in the order by order ID and product ID
    /// </summary>
    /// <param name="idOrder"></param>
    /// <param name="idProduct"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    public OrderItem GetbyIdOfProductAndOrder(int idOrder, int idProduct)
    {
        //Reading the data from the file into a list
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        //Finding the index of the requested object
        int x = OrderItemLst.FindIndex(x => x.OrderId == idOrder && x.ProductId == idProduct);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order Item");

        OrderItem orderItem = OrderItemLst[x];
        return orderItem;
    }
}
