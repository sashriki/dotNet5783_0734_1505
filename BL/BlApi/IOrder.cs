
namespace BlApi;

public interface IOrder
{
    public IEnumerable<BO.OrderForList> GetAllToManager();
    public BO.Order GetOrderToManager(int IdOrder);
    public BO.Order ShippingUpdateToManager(int IdOrder);
    public BO.OrderTracking supplyUpdateToManager(int IdOrder);
    public BO.OrderTracking OrderTracking(int IdOrder);
    public void UpdateToManager(BO.Order updateOrd);
}
