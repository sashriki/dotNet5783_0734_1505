using DalApi;
using DO;

namespace BO
{
    public class OrderTracking
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<(DateTime, OrderStatus)> PackageProgress { get; set; }
        public override string ToString()
        {
         string s = $@"
Order ID: {OrderId}
Order status: {OrderStatus}";
            s += '\n';
         foreach (var item in PackageProgress)
                s = s + item.Item2+": "+ item.Item1 + '\n';
         return s;
        }

    }
}
