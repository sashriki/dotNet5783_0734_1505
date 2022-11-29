namespace BO;
public class Order
{
    public int OrderId { get; set; } //ID number of the order
    public string CustomerName { get; set; } //customer name
    public string CustomerAdress { get; set; } //Customer's residential address
    public string CustomerEmail { get; set; } //Customer email address
    public DateTime?  OrderDate { get; set; }  //Date the order was placed
    public DateTime?  ShipDate { get; set; } //Date the order was sent
    public DateTime? DeliveryDate { get; set; } //The date of receipt of the order
    public OrderStatus orderStatus { get; set; }
    public IEnumerable<orderItem> orderItems { get; set; }
    public float finalPrice { get; set; }
    public override string ToString()
    {
        string s = $@"
       Order ID: {OrderId}
       Customer Name: {CustomerName}
       Customer Adress: {CustomerAdress}
       Customer Email: {CustomerEmail}
       Order Status:{orderStatus}
       Order Date: {OrderDate}
       Ship Date: {ShipDate}
       Delivery Date: {DeliveryDate}
       Final Price: {finalPrice}";
        foreach (var item in orderItems)
            s = s + item + '\n';
        return s;
    }

}


