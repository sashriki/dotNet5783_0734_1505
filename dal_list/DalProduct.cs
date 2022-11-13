
using DO;

namespace Dal;

internal class DalProduct
{
    //create
    int AddProduct(Product NewProduct)
    {
        DataSource.products.Add(NewProduct);
        return NewProduct.ProductId;
    }


    //request all
    public List<Product> GetProducts()
    {
        List<Product> ProductReturnList = new List<Product>();
        for (int i = 0; i < DataSource.products.Count(); i++)   //warning??
            ProductReturnList[i] = DataSource.products[i];
        return ProductReturnList;
    }

    //Request By Id
    public Product GetProductsId(int idProduct)
    {
        for (int i = 0; i < DataSource.products.Count(); i++)
            if (DataSource.products[i].ProductId == idProduct)
                return DataSource.products[i];

        throw new Exception($"product id {idProduct} is not found in products");
    }

    //update
    void UpdateProduct(Product UpdatedProduct)
    {
        for (int i = 0; i < DataSource.orderItems.Count(); i++)
            if (DataSource.products[i].ProductId == UpdatedProduct.ProductId)
            {
                DataSource.products[i] = UpdatedProduct;
                return;
            }
        throw new Exception($"product id {UpdatedProduct.ProductId} is not found in product.");
    }

    //delete
    void DeleteProduct(int removeById)
    {
        for (int i = 0; i < DataSource.products.Count(); i++)
            if (DataSource.products[i].ProductId == removeById)
            {
                DataSource.products.Remove(DataSource.products[i]);
                return;
            }
        throw new Exception($"product id {removeById} is not found in product.");
    }
}
