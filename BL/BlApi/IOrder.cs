
namespace BlApi;

public interface IOrder
{
    public int getTheOldOne();
    public IEnumerable<BO.OrderForList?> GetAllToManager();
    public BO.Order GetOrderByID(int IdOrder);
    public BO.Order ShippingUpdateToManager(int IdOrder);
    public BO.Order supplyUpdateToManager(int IdOrder);
    public BO.OrderTracking OrderTracking(int IdOrder);
    public void UpdateToManager(BO.Order updateOrd, int IdProduct, int Amount);
}
