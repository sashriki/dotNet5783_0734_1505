namespace Dal;
using DalApi;
using DO;


class dalProduct : IProduct
{
    public int Add(Product objToAdd)
    { }
    public void Delete(int objToDelete)
    { }
    public void Update(Product objToUpdate)
    { }
    public Product GetById(int objToGet)
    { }
    public Product GetByCondition(Func<Product?, bool>? condition)
    { }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? condition = null)
    { }
}
