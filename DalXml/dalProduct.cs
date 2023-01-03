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
    string path = "xmlProduct.xml";
    //XElement productsRoot;

    //public dalProduct()
    //{
    //    LoadData();
    //}

    //private void LoadData()
    //{
    //    try
    //    {
    //        if (File.Exists(path))
    //            productsRoot = XElement.Load(path);
    //        else
    //        {
    //            productsRoot = new XElement("xmlProduct");
    //            productsRoot.Save(path);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception("product File upload problem" + ex.Message);
    //    }
    //}

    /// <summary>
    /// The method adds a product to the list
    /// </summary>
    /// <param name="objToAdd"></param>
    /// <returns></returns>
    //public int Add(Product objToAdd)
    //{
    //    //Initialize the fields in the data
    //    XElement Id = new XElement("Id", objToAdd.ProductId);
    //    XElement Name = new XElement("Name", objToAdd.ProductName);
    //    XElement Amount = new XElement("Amount", objToAdd.AmountInStock);
    //    XElement Price = new XElement("Price", objToAdd.ProductPrice);
    //    XElement Category = new XElement("Category", objToAdd.ProductCategory);
    //    XElement Product = new XElement("product", Id, Name, Amount, Price, Category);
    //    //Adding the product to the file
    //    productsRoot.Add(Product);
    //    productsRoot.Save(path); //saving the file
    //    return objToAdd.ProductId;
    //}

    public int Add(Product objToAdd)
    {
        //Reading the data from the file into a list
        List<Product> ProductLst = ToolsXML.LoadListFromXMLSerializer<Product>(path);
        //Exception if the order exists
        if (ProductLst.Exists(x => x.ProductId == objToAdd.ProductId))
            throw new DuplicationException("Product");
        //Add an order to the list
        ProductLst.Add(objToAdd);
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(ProductLst, path);

        return objToAdd.ProductId;
    }
    /// <summary>
    /// The method deletes a product from the list
    /// </summary>
    /// <param name="objToDelete"></param>
    /// <exception cref="DO.NotfoundException"></exception>
    //public void Delete(int objToDelete)
    //{
    //    try
    //    {
    //        //Product search by ID
    //        XElement prod = (from p in productsRoot.Elements()
    //                         where Convert.ToInt32(p.Element("Id")!.Value) == objToDelete
    //                         select p).First();
    //        //Product deletion
    //        prod.Remove();
    //        //saving the file
    //        productsRoot.Save(path);
    //    }
    //    catch
    //    {
    //        //When the product does not exist
    //        throw new DO.NotfoundException("product");
    //    }
    //}

    public void Delete(int objToDelete)
    {
        //Reading the data from the file into a list
        List<Product> ProductLst = ToolsXML.LoadListFromXMLSerializer<Product>(path);
        //Finding the index of the requested object
        int x = ProductLst.FindIndex(x => x.ProductId == objToDelete);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Product");
        //Deleting the object
        ProductLst.RemoveAt(x);
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(ProductLst, path);
    }
    /// <summary>
    /// Product update
    /// </summary>
    /// <param name="objToUpdate"></param>
    /// <exception cref="DO.NotfoundException"></exception>
    //public void Update(Product objToUpdate)
    //{
    //    try
    //    {
    //        //Product search by ID in the file
    //        XElement prod = (from p in productsRoot.Elements()
    //                         where Convert.ToInt32(p.Element("Id")!.Value) == objToUpdate.ProductId
    //                         select p).First();
    //        //Update product details
    //        prod.Element("Id")!.Value = objToUpdate.ProductId.ToString();
    //        prod.Element("Name")!.Value = objToUpdate.ProductName;
    //        prod.Element("Amount")!.Value = objToUpdate.AmountInStock.ToString();
    //        prod.Element("Price")!.Value = objToUpdate.ProductPrice.ToString();
    //        prod.Element("Category")!.Value = objToUpdate.ProductCategory.ToString();
    //        //saving the file
    //        productsRoot.Save(path);
    //    }
    //    catch
    //    {
    //        //When the product does not exist
    //        throw new DO.NotfoundException("product");
    //    }
    //}

    public void Update(Product objToUpdate)
    {
        //Reading the data from the file into a list
        List<Product> ProductLst = ToolsXML.LoadListFromXMLSerializer<Product>(path);
        //Finding the index of the requested object
        int x = ProductLst.FindIndex(x => x.ProductId == objToUpdate.ProductId);
        //throw an exception to the close that the object does not exist
        if (x == -1)
            throw new NotfoundException("Product");
        //Update the object
        ProductLst[x] = objToUpdate;
        //Saving the updated file
        ToolsXML.SaveListToXMLSerializer(ProductLst, path);
        return;
    }
    /// <summary>
    /// The method returns a product by ID
    /// </summary>
    /// <param name="objToGet"></param>
    /// <returns></returns>
    //public Product GetById(int objToGet)
    //{
    //    Product product = new Product();
    //    product = (from p in productsRoot.Elements()
    //               where Convert.ToInt32(p.Element("Id")!.Value) == objToGet
    //               select new Product()
    //               {
    //                   ProductId = Convert.ToInt32(p.Element("Id")!.Value),
    //                   ProductName = p.Element("Name")!.Value,
    //                   ProductPrice = Convert.ToInt32(p.Element("Price")!.Value),
    //                   AmountInStock = Convert.ToInt32(p.Element("Amount")!.Value),
    //                   ProductCategory = (Category)Enum.Parse(typeof(Category), p.Element("Category")!.Value)
    //               }).First();
    //    return product;
    //}

    public Product GetById(int objToGet)
    {
        List<Product> ProductLst = ToolsXML.LoadListFromXMLSerializer<Product>(path);
        int x = ProductLst.FindIndex(x => x.ProductId == objToGet);
        if (x == -1)
            throw new NotfoundException("Product");
        Product Product = ProductLst[x];
        return Product;
    }
    /// <summary>
    /// The method returns a list of products according to a condition
    /// , if the method does not accept a condition it will return the entire list
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? condition = null)
    {

        //Reading the data from the file into a list
        List<DO.Product?> ProductList = ToolsXML.LoadListFromXMLSerializer<DO.Product?>(path);
        //When there is no condition we will return the entire list
        if (condition == null)
            return ProductList.AsEnumerable().OrderByDescending(p => p?.ProductId);

        return ProductList.Where(condition).OrderByDescending(p => p?.ProductId);
    }

    /// <summary>
    /// The method will return an object according to a condition
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    //public Product GetByCondition(Func<Product?, bool>? condition)
    //{
    //    Product product = new Product();
    //    try
    //    {
    //        product = (from p in productsRoot.Elements()
    //                   select new Product()
    //                   {
    //                       ProductId = Convert.ToInt32(p.Element("Id")!.Value),
    //                       ProductName = p.Element("Name")!.Value,
    //                       ProductPrice = float.Parse(p.Element("Price")!.Value),
    //                       AmountInStock = Convert.ToInt32(p.Element("Amount")!.Value),
    //                       ProductCategory = (Category)Enum.Parse(typeof(Category), p.Element("Category")!.Value)
    //                   }).Where(p => condition is null ? true : condition(p)).First();

    //    }
    //    catch
    //    {
    //        throw new NotfoundException("product");
    //    }
    //    return product;
    //}
    public Product GetByCondition(Func<Product?, bool>? condition)
    {
        List<Product> ProductLst = ToolsXML.LoadListFromXMLSerializer<Product>(path);
        int x = ProductLst.FindIndex(x => condition(x) == true);
        if (x == -1)
            throw new NotfoundException("Product");
        Product Product = ProductLst[x];
        return Product;
    }
}

