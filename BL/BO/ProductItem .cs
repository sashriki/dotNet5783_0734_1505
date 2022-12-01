
using DO;

namespace BO
{
    public class ProductItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public bool InStock { get; set; }
        public int AmmountInCart { get; set; }
        public override string ToString() => $@"
         Product ID: {ProductId} - {ProductName}
         Category: {Category}
         Price: {Price}
         In the stock: {InStock}
         Amount in cart: {AmmountInCart}";
    }
}
