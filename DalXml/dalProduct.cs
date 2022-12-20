namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;


class dalProduct : IProduct
{
    public int Add(Product objToAdd)
    {
        List<Product> prodLst = XmlTools.LoadListFromXMLSerializer<Product>(path);

        XElement Id = new XElement("Id", objToAdd.ProductId);
        XElement Name = new XElement("Name", objToAdd.ProductName);
        XElement Amount = new XElement("Amount", objToAdd.AmmountInStock);
        XElement Price = new XElement("Price", objToAdd.ProductPrice);
        XElement Category = new XElement("Category", objToAdd.ProductCategory);
        XElement Product = new XElement("priduct", Id, Name, Amount, Price, Category );
       
        ProductPath.Add(Product);
        return 1;
    }
    public void Delete(int objToDelete)
    { 

    }
    public void Update(Product objToUpdate)
    { 

    }
    public Product GetById(int objToGet)
    { }
    public Product GetByCondition(Func<Product?, bool>? condition)
    { }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? condition = null)
    {
        LoadData();
        List<Product> Products;
        try
        {
            Products = (from p in studentRoot.Elements()
                        select new Product()
                        {
                            Id = Convert.ToInt32(p.Element("id").Value),
                            FirstName = p.Element("name").Element("firstName").Value,
                            LastName = p.Element("name").Element("lastName").Value
                        }).ToList();
        }
        catch
        {
            Products = null;
        }
        return Products;
    }
}
