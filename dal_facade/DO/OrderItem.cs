using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct OrderItem
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }

        public override string ToString() => $@"
         Order item ID: {OrderItemId} 
         product ID: {ProductId}
         order ID: {OrderId}
         Price: {Price}
         Amount in stock: {Amount}";


    }
}
