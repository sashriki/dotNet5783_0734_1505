using DalApi;
namespace Dal
{
    internal sealed class DalList : IDal
    {      
        private DalList()
        { 
            Order = new DalOrder();
            OrderItem = new DalOrderItem();
            Product = new DalProduct();
        }
        public static IDal Instance { get; } = new DalList();
        public IOrder Order { get; }
        public IOrderItem OrderItem { get; }   
        public IProduct Product { get; }
    }
}