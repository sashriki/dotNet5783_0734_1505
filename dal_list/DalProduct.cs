
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

public class DalProduct
{
    //create
    public int AddProduct(Product NewProduct)
    {
        NewProduct.ProductId = DataSource.Config.productIndex;
        DataSource.products.Add(NewProduct);
        return NewProduct.ProductId;
    }


    //request all
    public List<Product> GetProducts()
    {
        List<Product> ProductReturnList = new List<Product>();
        for (int i = 0; i < DataSource.products.Count(); i++)   //warning??
            ProductReturnList.Add(DataSource.products[i]);
        return ProductReturnList;
    }

    //Request By Id
    public Product GetProductsId(int idProduct)
    {
        int x = DataSource.products.FindIndex(x => x.ProductId == idProduct);
        if (x == -1)
            throw new Exception($"product id {idProduct} is not found in products");
        else
            return DataSource.products[x];
    }

    //update
    public void UpdateProduct(Product UpdatedProduct)
    {
        int x = DataSource.products.FindIndex(x => x.ProductId == UpdatedProduct.ProductId);
        if (x == -1)
            throw new Exception($"product id {UpdatedProduct.ProductId} is not found in product.");
        else
            DataSource.products.Insert(x + 1, UpdatedProduct);
    }

    //delete
    public void DeleteProduct(int removeById)
    {
        int x = DataSource.products.FindIndex(x => x.ProductId == removeById);
        if (x == -1)
            throw new Exception($"product id {removeById} is not found in product.");
        else
            DataSource.products.RemoveAt(x);
    }
}
