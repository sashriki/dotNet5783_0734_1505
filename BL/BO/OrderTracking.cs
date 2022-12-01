namespace BO
{
    public class OrderTracking
    {
        public int orderId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public List<(DateTime, OrderStatus)> packageProgress { get; set; }
    }
}
