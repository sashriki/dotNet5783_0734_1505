
namespace BO
{
    public class ProductForList
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public float price { get; set; }
        public Category Category { get; set; }
        public override string ToString() => $@" 
        Product Id: {productId}
        Product name: {productName}
        Price: {price}
        Category: {Category}";
    }
}
