namespace BlApi;
public interface IProduct
{
    public IEnumerable<BO.ProductForList> getAllProducts();
    public BO.Product getByIdToMannage(int id);
    public BO.ProductItem getByIdToCostumer(int id, BO.orderItem orderItem);
    public void addProduct(BO.Product product);
    public void removeProduct(int id);
    public void updateProduct(BO.Product product);
}
