﻿using DalApi;
using DO;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

internal class DalProduct :IProduct 
{
    /// <summary>
    /// Adding a product
    /// </summary>
    /// <param name="NewProduct"></param>
    /// <returns></returns>
    public int Add(Product NewProduct)
    {
        NewProduct.ProductId = DataSource.Config.productIndex;
        DataSource.products.Add(NewProduct);
        return NewProduct.ProductId;
    }

    /// <summary>
    /// Returning the list of products
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Product> GetAll()
    {
        List<Product> ProductReturnList = new List<Product>();
        for (int i = 0; i < DataSource.products.Count(); i++)   //warning??
            ProductReturnList.Add(DataSource.products[i]);
        return ProductReturnList;
    }

    /// <summary>
    /// Product return by product ID number
    /// </summary>
    /// <param name="idProduct"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Product GetById(int idProduct)
    {
        int x = DataSource.products.FindIndex(x => x.ProductId == idProduct);
        if (x == -1)
            throw new NotfoundException($"Product ID: {idProduct} is not found in order items.\n");
        else
            return DataSource.products[x];
    }

    /// <summary>
    /// Product update
    /// </summary>
    /// <param name="UpdatedProduct"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Product UpdatedProduct)
    {
        int x = DataSource.products.FindIndex(x => x.ProductId == UpdatedProduct.ProductId);
        if (x == -1)
            throw new NotfoundException($"Product ID: {UpdatedProduct.ProductId} is not found in order items.\n");
        else
            DataSource.products.Insert(x + 1, UpdatedProduct);
    }

    /// <summary>
    /// Product deletion
    /// </summary>
    /// <param name="removeById"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int removeById)
    {
        int x = DataSource.products.FindIndex(x => x.ProductId == removeById);
        if (x == -1)
            throw new NotfoundException($"Product ID: {removeById} is not found in order items.\n");
        else
            DataSource.products.RemoveAt(x);
    }
}
