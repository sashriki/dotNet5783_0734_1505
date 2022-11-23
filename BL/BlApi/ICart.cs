 namespace BlApi;

public interface  ICart
{
    public BO.Cart AddProductToCart(BO.Cart newCart ,int IdorderItem);
    public BO.Cart UpdateAmount(BO.Cart newCart, int IdorderItem, int amount);
    public void OrderConfirmation(BO.Cart newCart);
}
