namespace BO;

public class Product
{
    public int ProductId { get; set; } //Product ID number
    public string? ProductName { get; set; } //Product Name
    public int AmmountInStock { get; set; } //Quantity in stock of the product
    public double ProductPrice { get; set; } //product Price
    public Category? ProductCategory { get; set; }  //Product category
    public override string ToString() => $@"
         Product ID: {ProductId} - {ProductName}
         category: {ProductCategory}
         Price: {ProductPrice}
         Amount in stock: {AmmountInStock}";
}
