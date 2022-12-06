
namespace BO;
public class Cart
{
    public string? CustomerName { get; set; } //customer name 
    public string? CustomerAdress { get; set; } //Customer's residential address
    public string? CustomerEmail { get; set; } //Customer email address
    public List<BO.OrderItem?> OrderItems { get; set; } //something
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        string s = $@"
       Customer Name: {CustomerName}
       Customer Adress: {CustomerAdress}
       Customer Email: {CustomerEmail}
       Total Price: {Math.Round(TotalPrice,2)}";
        foreach (var item in OrderItems)
            s = s + item + '\n';
        return s;
    }



}
