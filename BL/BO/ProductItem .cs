
using DO;

namespace BO
{
    public class ProductItem
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public double price { get; set; }
        public Category Category { get; set; }
        public bool inStock { get; set; }
        public int ammountInCart { get; set; }
        public override string ToString() => $@"
         Product ID: {productId} - {productName}
         Category: {Category}
         Price: {price}
         In the stock: {inStock}
         Amount in cart: {ammountInCart}";
    }
}
