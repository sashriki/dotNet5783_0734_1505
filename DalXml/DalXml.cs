namespace Dal;
using DalApi;

sealed internal class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }
    public IProduct Product { get; } = new dalProduct();
    public IOrder Order { get; } = new dalOrder();
    public IOrderItem OrderItem { get; } = new dalOrderItem();

}
