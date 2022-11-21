namespace BO
{
    public class OrderTracking
    {
        public int orderId { get; set; }    
        public OrderStatus OrderStatus { get; set; }
        public List<Tuple<DateTime,OrderStatus>> packageProgress { get; set; }
    }
}
