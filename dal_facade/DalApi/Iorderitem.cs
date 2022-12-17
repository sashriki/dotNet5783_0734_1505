namespace DalApi;
using DO;
public interface IOrderItem : Icrud<OrderItem>
{
    public OrderItem GetbyIdOfProductAndOrder(int idOrder, int idProduct);
}
