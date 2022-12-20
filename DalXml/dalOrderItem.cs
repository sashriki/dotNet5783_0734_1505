using DalApi;
namespace Dal;
using DO;
using System.IO;
using System.Linq;

internal class dalOrderItem : IOrderItem
{
    string path = "xmlOrderItem.xml";
    public int Add(OrderItem objToAdd)
    {
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);

        if (OrderItemLst.Exists(x => x.OrderItemId == objToAdd.OrderItemId))
            throw new DuplicationException("Order Item");

        OrderItemLst.Add(objToAdd);

        ToolsXML.SaveListToXMLSerializer(OrderItemLst, path);

        return objToAdd.OrderId;
    }
    public void Delete(int objToDelete)
    {
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        
        int x = OrderItemLst.FindIndex(x => x.OrderItemId == objToDelete);
        
        if (x != -1)
            throw new NotfoundException("Order Item");
        
        OrderItemLst.RemoveAt(x);
        
        ToolsXML.SaveListToXMLSerializer(OrderItemLst, path);
    }
    public void Update(OrderItem objToUpdate)
    {
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        
        int x = OrderItemLst.FindIndex(x => x.OrderItemId == objToUpdate.OrderItemId);
        
        if (x != -1)       
            throw new NotfoundException("Order Item");
               
        OrderItemLst[x] = objToUpdate;
        
        ToolsXML.SaveListToXMLSerializer(OrderItemLst, path);
        return;
    }
    public OrderItem GetById(int objToGet)
    {
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        
        int x = OrderItemLst.FindIndex(x => x.OrderItemId == objToGet);
        
        if (x != -1)
            throw new NotfoundException("Order Item");
        
        return OrderItemLst[x];
    }
    public OrderItem GetByCondition(Func<OrderItem?, bool>? condition)
    {
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);
        
        int x = OrderItemLst.FindIndex(x => condition(x) == true);
        
        if (x != -1)
            throw new NotfoundException("Order Item");
        
        return OrderItemLst[x];
    }
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? condition = null)
    {
        List<DO.OrderItem?> OrderList = ToolsXML.LoadListFromXMLSerializer<DO.OrderItem?>(path);

        if (condition == null)
            return OrderList.AsEnumerable().OrderByDescending(p => p?.OrderItemId);

        return OrderList.Where(condition).OrderByDescending(p => p?.OrderItemId);
    }
    public OrderItem GetbyIdOfProductAndOrder(int idOrder, int idProduct)
    {
        List<OrderItem> OrderItemLst = ToolsXML.LoadListFromXMLSerializer<OrderItem>(path);

        int x = OrderItemLst.FindIndex(x => x.OrderId == idOrder && x.ProductId == idProduct);

        if (x != -1)
            throw new NotfoundException("Order Item");

        return OrderItemLst[x];
    }
}
