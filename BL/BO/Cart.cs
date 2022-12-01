
namespace BO;
public class Cart
{
    public string CustomerName { get; set; } //customer name 
    public string CustomerAdress { get; set; } //Customer's residential address
    public string CustomerEmail { get; set; } //Customer email address
    public IEnumerable<BO.orderItem> orderItems { get; set; } //something
    public double totalPrice { get; set; }

    public override string ToString()
    {
        string s = $@"
       Customer Name: {CustomerName}
       Customer Adress: {CustomerAdress}
       Customer Email: {CustomerEmail}
       Total Price: {totalPrice}";
        foreach (var item in orderItems)
            s = s + item + '\n';
        return s;
    }



}
