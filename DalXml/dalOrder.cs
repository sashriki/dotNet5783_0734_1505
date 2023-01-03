namespace Dal;
using DalApi;
using DO;
using System;
using System.Xml.Linq;

internal class dalOrder : IOrder
{
    string path = "xmlOrder.xml";
    /// <summary>
    /// Add an order to the list
    /// </summary>
    /// <param name="objToAdd"></param>
    /// <returns></returns>
    /// <exception cref="DuplicationException"></exception>
    public int Add(Order objToAdd)
    {
        //Reading the data from the file into a list
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        //Exception if the order exists
        if (OrderLst.Exists(x => x.OrderId == objToAdd.OrderId))
            throw new DuplicationException("Order");
        //Add an order to the list
        OrderLst.Add(objToAdd);
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(OrderLst, path);

        return objToAdd.OrderId;
    }
    /// <summary>
    /// Deleting an order from the list
    /// </summary>
    /// <param name="objToDelete"></param>
    /// <exception cref="NotfoundException"></exception>
    public void Delete(int objToDelete)
    {
        //Reading the data from the file into a list
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        //Finding the index of the requested object
        int x = OrderLst.FindIndex(x=> x.OrderId == objToDelete);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order");
        //Deleting the object
        OrderLst.RemoveAt(x);
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(OrderLst, path);
    }
    /// <summary>
    /// Update Invitation
    /// </summary>
    /// <param name="objToUpdate"></param>
    /// <exception cref="NotfoundException"></exception>
    public void Update(Order objToUpdate) 
    {
        //Reading the data from the file into a list
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        //Finding the index of the requested object
        int x = OrderLst.FindIndex(x => x.OrderId == objToUpdate.OrderId);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order");
        //Update the object
        OrderLst[x] = objToUpdate;
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(OrderLst, path);
        return;
    }

    /// <summary>
    /// The method will return an order by ID
    /// </summary>
    /// <param name="objToGet"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    public Order GetById(int objToGet) 
    {
        //Reading the data from the file into a list
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        //Finding the index of the requested object
        int x = OrderLst.FindIndex(x => x.OrderId == objToGet);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order");
        Order order = OrderLst[x];
        return order;
    }

    /// <summary>
    /// The method will return an order by condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    public Order GetByCondition(Func<Order?, bool>? condition) 
    {
        //Reading the data from the file into a list
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        //Finding the index of the requested object
        int x = OrderLst.FindIndex(x => condition(x) == true);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Order");
        Order order = OrderLst[x];
        return order;
    }

    /// <summary>
    /// The method will return a collection of orders by condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? condition = null) 
    {
        //Reading the data from the file into a list
        List<DO.Order?> OrderList = ToolsXML.LoadListFromXMLSerializer<DO.Order?>(path);
        //When there is no condition we will return the entire list
        if (condition == null)
            return OrderList.AsEnumerable().OrderByDescending(p => p?.OrderId);

        return OrderList.Where(condition).OrderByDescending(p => p?.OrderId);
    }
}
