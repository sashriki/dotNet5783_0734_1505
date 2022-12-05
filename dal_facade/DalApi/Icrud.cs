using DO;

namespace DalApi;

public interface Icrud<T> where T : struct
{
    public int Add(T objToAdd);
    public void Delete(int objToDelete);
    public void Update(T objToUpdate);
    public T GetById(int objToGet);
    public T GetByCondition(Func<T?, bool>? condition);
    public IEnumerable<T?> GetAll(Func<T?,bool>? condition = null);

}
