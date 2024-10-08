﻿using DalApi;
using DO;
using static Dal.DataSource;
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
        NewProduct.ProductId = NewProduct.ProductId++;
        int x = products.FindIndex(x => x?.ProductId == NewProduct.ProductId);
        if (x != -1)
            throw new DuplicationException("Product");
        products.Add(NewProduct);
        return NewProduct.ProductId;
    }

    /// <summary>
    /// A function that receives a condition for filtering
    /// and returns the list according to the condition, when there is no condition
    /// the function will return the entire list of objects.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? condition = null)
    {
        IEnumerable<Product?> productReturn;
        if (condition == null)
            return productReturn = from item in products
                                   select item;
        return productReturn = from item in products
                               where condition(item) == true
                               select item;
    }

    /// <summary>
    /// Product return by product ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetById(int idProduct)
    {
        Product product=new Product();
        int x = products.FindIndex(x => x?.ProductId == idProduct);
        if (x == -1)
            throw new NotfoundException("Product");
        product= products[x] ?? throw new NotfoundException("Product");
        return product;
    }

    /// <summary>
    /// Product update
    /// </summary>
    /// <param name="UpdatedProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product UpdatedProduct)
    {
        int x = products.FindIndex(x => x?.ProductId == UpdatedProduct.ProductId);
        if (x == -1)
            throw new NotfoundException("Product");
        UpdatedProduct.ProductPrice = (float)Math.Round(UpdatedProduct.ProductPrice,1);
        products[x] = UpdatedProduct;
        
    }

    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        for (int i = 0; i < products.Count(); i++)
        {
            if (products[i]?.ProductId == removeById)
            {
                products.Remove(products[i]);
                return;
            }
        }
        throw new NotfoundException("Product");
    }
    /// <summary>
    /// Returns an object by condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    public Product GetByCondition(Func<Product?, bool>? condition)
    {
        int x= products.FindIndex(x => condition(x));
        if (x == -1)
            throw new NotfoundException("Product");
        Product NewProduct = products[x] ?? throw new NotfoundException("Product");
        return NewProduct;
    }
}
