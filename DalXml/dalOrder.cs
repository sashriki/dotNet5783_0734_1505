namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class dalOrder : IOrder
{
    string path = "xmlOrder.xml";
    public int Add(Order objToAdd)
    {
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);

        if (OrderLst.Exists(x => x.OrderId == objToAdd.OrderId))
            throw new DuplicationException("Order");

        OrderLst.Add(objToAdd);

        ToolsXML.SaveListToXMLSerializer(OrderLst, path);

        return objToAdd.OrderId;
    }

    public void Delete(int objToDelete)
    {
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        int x= OrderLst.FindIndex(x=> x.OrderId == objToDelete);
        if (x != -1)
            throw new NotfoundException("Order");
        OrderLst.RemoveAt(x);
        ToolsXML.SaveListToXMLSerializer(OrderLst, path);
    }
    public void Update(Order objToUpdate) 
    {
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        int x = OrderLst.FindIndex(x => x.OrderId == objToUpdate.OrderId);
        if (x != -1)
            throw new NotfoundException("Order");
        OrderLst[x] = objToUpdate;
        ToolsXML.SaveListToXMLSerializer(OrderLst, path);
        return;
    }
    public Order GetById(int objToGet) 
    {
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        int x = OrderLst.FindIndex(x => x.OrderId == objToGet);
        if (x != -1)
            throw new NotfoundException("Order");
        Order order = OrderLst[x];
        return order;
    }
    public Order GetByCondition(Func<Order?, bool>? condition) 
    {
        List<Order> OrderLst = ToolsXML.LoadListFromXMLSerializer<Order>(path);
        int x = OrderLst.FindIndex(x => condition(x) == true);
        if (x != -1)
            throw new NotfoundException("Order");
        Order order = OrderLst[x];
        return order;
    }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? condition = null) 
    {
        List<DO.Order?> OrderList = ToolsXML.LoadListFromXMLSerializer<DO.Order?>(path);

        if (condition == null)
            return OrderList.AsEnumerable().OrderByDescending(p => p?.OrderId);

        return OrderList.Where(condition).OrderByDescending(p => p?.OrderId);
    }
}
