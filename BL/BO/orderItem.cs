
namespace BO
{
    public class orderItem
    {
        public int orderItemId { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public float priceOfProduct { get; set; }
        public int amountOfProduct { get; set; }  //how many I want to order from this product
        public float finalPriceOfProduct { get; set; }
        public override string ToString() => $@"
         Order Item Id: {orderItemId} 
         Product Id: {productId}
         Product name: {productName}
         Amount  from this produst: {amountOfProduct}
         The price of the product: {finalPriceOfProduct}
         The total price: {finalPriceOfProduct}";
    }
}
