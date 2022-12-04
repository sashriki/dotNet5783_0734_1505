using DalApi;
using DO;

namespace BO
{
    public class OrderTracking
    {
        public int orderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<(DateTime, OrderStatus)> packageProgress { get; set; }
        public override string ToString()
        {
         string s = $@"
Order ID: {orderId}
Order status: {OrderStatus}";
            s += '\n';
         foreach (var item in packageProgress)
                s = s + item.Item2+": "+ item.Item1 + '\n';
         return s;
        }

    }
}
