namespace BlApi;
public interface IProduct
{
    public IEnumerable<BO.Product> getAllToManager();
    public IEnumerable<BO.Product> getAllToCostumer();
    public BO.Product getByIdToMannage(int id);
    public BO.Product getByIdToCostumer(int id, BO.orderItem orderItem); 
    public void addProduct (BO.Product product);
    public void removeProduct (int id);
    public void updateProduct (BO.Product product); 
}
