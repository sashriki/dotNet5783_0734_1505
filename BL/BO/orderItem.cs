
namespace BO
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double PriceOfProduct { get; set; }
        public int AmountOfProduct { get; set; }  //how many I want to order from this product
        public double FinalPriceOfProduct { get; set; }
         public override string ToString() => $@"
       Order Item Id: {OrderItemId} 
       Product Id: {ProductId}
       Product name: {ProductName}
       Amount from this produst: {AmountOfProduct}
       The Price of the product: {FinalPriceOfProduct}
       The total Price: {FinalPriceOfProduct* AmountOfProduct}";
    }
}
