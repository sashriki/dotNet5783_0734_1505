using DalApi;
using DO;

namespace Dal;

internal class DalProduct : IProduct
{
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="NewProduct"></param>
    /// <returns></returns>
    public int Add(Product NewProduct)
    {
        int x = DataSource.products.FindIndex(x => x?.ProductId == NewProduct.ProductId);
        if (x != -1)
            throw new NotfoundException("Product");
        DataSource.products.Add(NewProduct);
        return NewProduct.ProductId;
    }

    /// <summary>
    /// Returning the list of products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? condition = null)
    {
        IEnumerable<Product?> productReturn;
        if (condition == null)
        {
            productReturn = new List<Product?>();
            for (int i = 0; i < DataSource.products.Count(); i++)   //warning??
                productReturn.Append(DataSource.products[i]);
            return productReturn;
        }
        return productReturn = from item in DataSource.products
                               where condition(item) == true
                               select item;
    }

    /// <summary>
    /// Product return by product ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product? GetById(int idProduct)
    {
        Product? product=new Product();
        int x = DataSource.products.FindIndex(x => x?.ProductId == idProduct);
        if (x == -1)
            throw new NotfoundException("Product");
        product=DataSource.products[x];
        return product;
    }

    /// <summary>
    /// Product update
    /// </summary>
    /// <param name="UpdatedProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product UpdatedProduct)
    {
        int x = DataSource.products.FindIndex(x => x?.ProductId == UpdatedProduct.ProductId);
        if (x == -1)
            throw new NotfoundException("Product");
        DataSource.products.Insert(x + 1, UpdatedProduct);
    }

    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        int x = DataSource.products.FindIndex(x => x?.ProductId == removeById);
        if (x == -1)
            throw new NotfoundException("Product");
        DataSource.products.RemoveAt(x);
    }
    public Product? GetByCondition(Func<Product?, bool>? condition)
    {
        Product? NewProduct = DataSource.products.Find(x => condition(x));
        if (NewProduct == null)
            throw new NotfoundException("Product");
        return NewProduct;
    }
}
