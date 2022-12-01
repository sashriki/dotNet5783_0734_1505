
namespace BO
{
    public class ProductItem
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public float price { get; set; }
        public Category Category { get; set; }
        public bool inStock { get; set; }
        public int ammountInCart { get; set; }
    }
}
