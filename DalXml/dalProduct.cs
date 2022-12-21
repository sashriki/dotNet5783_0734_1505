namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Linq;


class dalProduct : IProduct
{
    string path = @"../xml/Product.xml";


    XElement productsRoot;

    public dalProduct()
    {
        LoadData();
    }

    private void LoadData()
    {
        try
        {
            if (File.Exists(path))
                productsRoot = XElement.Load(path);
            else
            {
                productsRoot = new XElement("products");
                productsRoot.Save(path);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("product File upload problem" + ex.Message);
        }
    }

    public int Add(Product objToAdd)
    {

        XElement Id = new XElement("Id", objToAdd.ProductId);
        XElement Name = new XElement("Name", objToAdd.ProductName);
        XElement Amount = new XElement("Amount", objToAdd.AmountInStock);
        XElement Price = new XElement("Price", objToAdd.ProductPrice);
        XElement Category = new XElement("Category", objToAdd.ProductCategory);
        XElement Product = new XElement("product", Id, Name, Amount, Price, Category);

        productsRoot.Add(Product);
        productsRoot.Save(path);
        return objToAdd.ProductId;
    }

    public void Delete(int objToDelete)
    {
        try
        {
            XElement prod = (from p in productsRoot.Elements()
                            where Convert.ToInt32(p.Element("Id")!.Value) == objToDelete
                            select p).First();
            prod.Remove();
            productsRoot.Save(path);
        }
        catch
        {
            throw new DO.NotfoundException("product");
        }      
    }

    public void Update(Product objToUpdate)
    {
        try
        {
            XElement prod = (from p in productsRoot.Elements()
                             where Convert.ToInt32(p.Element("Id")!.Value) == objToUpdate.ProductId
                             select p).First();
            prod.Element("Id")!.Value = objToUpdate.ProductId.ToString();
            prod.Element("Name")!.Value = objToUpdate.ProductName;
            prod.Element("Amount")!.Value = objToUpdate.AmountInStock.ToString();
            prod.Element("Price")!.Value = objToUpdate.ProductPrice.ToString();
            prod.Element("Category")!.Value = objToUpdate.ProductCategory.ToString();
            productsRoot.Save(path);           
        }
        catch
        {
            throw new DO.NotfoundException("product");
        }
    }
    public Product GetById(int objToGet)
    {
        Product product=new Product();

        product = (from p in productsRoot.Elements()
                                where Convert.ToInt32(p.Element("Id")!.Value)== objToGet
                                select new Product()
                                {
                                    ProductId = Convert.ToInt32(p.Element("Id")!.Value),
                                    ProductName = p.Element("Name")!.Value,
                                    ProductPrice = Convert.ToInt32(p.Element("Price")!.Value),
                                    AmountInStock = Convert.ToInt32(p.Element("Amount")!.Value),
                                    ProductCategory = (Category)Enum.Parse(typeof(Category), p.Element("Category")!.Value)
                                }).First();
        return product;
    }


    public IEnumerable<Product?> GetAll(Func<Product?, bool>? condition = null)
    {
        IEnumerable<Product?> products;
        if (condition is null)
        {
            try
            {
                products = (from p in productsRoot.Elements()
                            select (Product?)new Product()
                            {
                                ProductId = Convert.ToInt32(p.Element("Id")!.Value),
                                ProductName = p.Element("Name")!.Value,
                                ProductPrice = Convert.ToInt32(p.Element("Price")!.Value),
                                AmountInStock = Convert.ToInt32(p.Element("Amount")!.Value),
                                ProductCategory = (Category)Enum.Parse(typeof(Category), p.Element("Category")!.Value)
                            }).ToList();
            }
            catch
            {
                products = null;
            }
        }
        else
        {
            try
            {
                products = (from p in productsRoot.Elements()
                            select (Product?)new Product()
                            {
                                ProductId = Convert.ToInt32(p.Element("Id")!.Value),
                                ProductName = p.Element("Name")!.Value,
                                ProductPrice = float.Parse(p.Element("Price")!.Value),
                                AmountInStock = Convert.ToInt32(p.Element("Amount")!.Value),
                                ProductCategory = (Category)Enum.Parse(typeof(Category), p.Element("Category")!.Value)
                            }).Where(p => condition is null ? true : condition(p));

            }
            catch
            {
                products = null;
            }
        }
        return products;
    }

    public Product GetByCondition(Func<Product?, bool>? condition)
    {
        Product product = new Product();
        try
        {
            product = (from p in productsRoot.Elements()
                       select new Product()
                       {
                           ProductId = Convert.ToInt32(p.Element("Id")!.Value),
                           ProductName = p.Element("Name")!.Value,
                           ProductPrice = float.Parse(p.Element("Price")!.Value),
                           AmountInStock = Convert.ToInt32(p.Element("Amount")!.Value),
                           ProductCategory = (Category)Enum.Parse(typeof(Category), p.Element("Category")!.Value)
                       }).Where(p => condition is null ? true : condition(p)).First();

        }
        catch
        {
            throw new NotfoundException("product");
        }
        return product;
    }

}

