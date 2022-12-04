namespace DalApi;
using DO;
public interface Iorderitem : Icrud<OrderItem>
{
    public OrderItem GetbyIdOfProductAndOrder(int idOrder, int idProduct);
}
