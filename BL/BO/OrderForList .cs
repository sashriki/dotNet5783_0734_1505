
namespace BO
{
    public class OrderForList
    {
        public int orderId { get; set; }    
        public string costumerName { get; set; }  
        public OrderStatus OrderStatus { get; set; }
        public int amountOfItems { get; set; } 
        public double finalPrice { get; set; }
        public override string ToString() => $@"
         Order ID: {orderId} 
         Costumer name: {costumerName}
         Order Status: {OrderStatus}
         Price: {finalPrice}
         Amount in items: {amountOfItems}";

    }
}
