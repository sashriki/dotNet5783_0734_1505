
namespace BO
{
    public class ProductForList
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; }

        public float Price { get; set; }

        public Category? Category { get; set; }

        public override string ToString() => $@" 
        Product Id: {ProductId}
        Product name: {ProductName}
        Price: {Price}
        Category: {Category}";
    }
}
