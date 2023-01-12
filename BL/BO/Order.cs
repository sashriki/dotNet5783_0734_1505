namespace BO;
public class Order
{
    public int OrderId { get; set; } //ID number of the order
    public string? CustomerName { get; set; } //customer name
    public string? CustomerAdress { get; set; } //Customer's residential address
    public string? CustomerEmail { get; set; } //Customer email address
    public DateTime? OrderDate { get; set; }  //Date the order was placed
    public DateTime? ShipDate { get; set; } //Date the order was sent
    public DateTime? DeliveryDate { get; set; } //The date of receipt of the order
    public OrderStatus? OrderStatus { get; set; }
    public IEnumerable<OrderItem?> OrderItems { get; set; }
    public float FinalPrice { get; set; }
    public override string ToString()
    {
        string s = $@"
       Order ID: {OrderId}
       Customer Name: {CustomerName}
       Customer Adress: {CustomerAdress}
       Customer Email: {CustomerEmail}
       Order Status:{OrderStatus}
       Order Date: {OrderDate}
       Ship Date: {ShipDate}
       Delivery Date: {DeliveryDate}
       Final Price: {FinalPrice}";
       foreach (var item in OrderItems)
           s = s + item + '\n';
       return s;
    }

}


