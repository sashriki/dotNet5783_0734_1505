using DalApi;
namespace Dal;
using DO;

internal class dalOrderItem : IOrderItem
{
    public int Add(OrderItem objToAdd)
    { }
    public void Delete(int objToDelete)
    { }
    public void Update(OrderItem objToUpdate)
    { }
    public OrderItem GetById(int objToGet)
    { }
    public OrderItem GetByCondition(Func<OrderItem?, bool>? condition)
    { }
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? condition = null)
    { }
}
