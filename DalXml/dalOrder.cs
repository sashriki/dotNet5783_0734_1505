namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class dalOrder : IOrder
{
    string path = "xmlOrder.xml";
    XElement ordersRoot;
    public int Add(Order objToAdd)
    { 
    
    }
    public void Delete(int objToDelete)
    { }
    public void Update(Order objToUpdate) 
    { }
    public Order GetById(int objToGet) 
    { }
    public Order GetByCondition(Func<Order?, bool>? condition) 
    { }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? condition = null) 
    { }
}
