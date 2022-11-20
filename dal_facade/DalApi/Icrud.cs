using DO;
namespace DalApi;

public interface Icrud<T> 
{
    public int Add(T objToAdd);  
    public void Delete(int objToDelete);
    public void Update(T objToUpdate);
    public T GetById(int objToGet);
    public IEnumerable<T> GetAll();
}
