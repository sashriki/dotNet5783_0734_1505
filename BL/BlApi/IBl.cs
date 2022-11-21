namespace BlApi;
public interface IBl
{
    public ICart cart { get; }
    public IOrder order { get; }
    public IProduct product { get; }
}
