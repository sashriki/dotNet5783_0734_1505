
namespace BO
{
    public class OrderForList
    {
        public int OrderId { get; set; }
        public string CostumerName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int AmountOfItems { get; set; }
        public double FinalPrice { get; set; }
        public override string ToString() => $@"
         Order ID: {OrderId} 
         Costumer name: {CostumerName}
         Order Status: {OrderStatus}
         Price: {FinalPrice}
         Amount in items: {AmountOfItems}";

    }
}
