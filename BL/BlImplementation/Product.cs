internal class Product : BlApi.IProduct
{
    private DalApi.IDal Dal = new Dal.DalList();
    public IEnumerable<BO.ProductForList> getAllProducts()
    {
        IEnumerable<DO.Product> DO_products = Dal.IProduct.GetAll();
        IEnumerable<BO.ProductForList> BO_products = from item in DO_products
                                                     select Do_ProductToBo_ProductForList(item);
        if (!BO_products.Any())
            throw new BO.NoElementsException("products");
        return BO_products;
    }
    public BO.Product getByIdToMannage(int id)
    {
        DO.Product DO_product = new DO.Product();
        if (id > 0)
        {
            try
            {
                DO_product = Dal.IProduct.GetById(id);
            }
            catch (Exception ex)
            {
                throw new BO.BONotfoundException(ex);
            }
            return Do_ProductToBo_Product(DO_product);
        }
        else
        {
            throw new BO.InvalidInputBO("product ID");
        }
    }
    public BO.ProductItem getByIdToCostumer(int id, BO.Cart cart)
    {
        DO.Product DO_product = new DO.Product();
        if (id > 0)
        {
            try
            {
                DO_product = Dal.IProduct.GetById(id);
            }
            catch (Exception ex)
            {
                throw new BO.BONotfoundException(ex);
            }
            return Do_ProductToBo_ProductItem(DO_product, cart);
        }
        else
        {
            throw new BO.InvalidInputBO("ID");
        }
    }
    public void addProduct(BO.Product BO_product)
    {
        if (BO_product.ProductId > 0)
            if (BO_product.ProductName != null)
                if (BO_product.ProductPrice > 0)
                    if (BO_product.AmmountInStock > 0)
                    {
                        Dal.IProduct.Add(Bo_ProductToDo_Product(BO_product));
                        return;
                    }
        throw new BO.InvalidInputBO("data");
    }
    public void removeProduct(int id)
    {
        IEnumerable<DO.Product> products = Dal.IProduct.GetAll();
        bool flag = false;
        foreach(var item in products) { if (item.ProductId == id) { flag = true; break; } }
        if (flag)
            try 
            {
                Dal.IProduct.Delete(id);
            }
            catch(Exception ex)
            {
                throw new BO.BONotfoundException(ex);   
            }

        /*IEnumerable<DO.OrderItem> OrderItems = Dal.Iorderitem.GetAll();
        bool flag = false;
        foreach (var item in OrderItems) { if (item.ProductId == id) { flag = true; break; } }
        if (flag)
            try
            {
                Dal.Iorder.Delete(id);
            }
            catch (Exception ex)
            {
                throw new BO.BONotfoundException(ex);
            }*/

    }
    public void updateProduct(BO.Product BO_product)
    {
        if (BO_product.ProductId > 0)
            if (BO_product.ProductName != null)
                if (BO_product.ProductPrice > 0)
                    if (BO_product.AmmountInStock > 0)
                    {
                        Dal.IProduct.Update(Bo_ProductToDo_Product(BO_product));
                        return;
                    }
        throw new BO.InvalidInputBO("data");
    }
    private BO.ProductForList Do_ProductToBo_ProductForList(DO.Product product)
    {
        BO.ProductForList products = new BO.ProductForList();
        products.ProductId = product.ProductId;
        products.ProductName = product.ProductName;
        products.Price = product.ProductPrice;
        products.Category = (BO.Category)product.ProductCategory;
        return products;
    }
    private BO.Product Do_ProductToBo_Product(DO.Product product)
    {
        BO.Product B0_product = new BO.Product();
        B0_product.ProductId = product.ProductId;
        B0_product.ProductName = product.ProductName;
        B0_product.AmmountInStock = product.AmmountInStock;
        B0_product.ProductPrice = product.ProductPrice;
        B0_product.ProductCategory = (BO.Category)product.ProductCategory;
        return B0_product;
    }
    private BO.ProductItem Do_ProductToBo_ProductItem(DO.Product product, BO.Cart cart)
    {
        BO.ProductItem B0_product = new BO.ProductItem();
        B0_product.ProductId = product.ProductId;
        B0_product.ProductName = product.ProductName;
        if (product.AmmountInStock > 0)
            B0_product.InStock = true;
        else
            B0_product.InStock = false;
        B0_product.Price = product.ProductPrice;
        B0_product.Category = (BO.Category)product.ProductCategory;
        BO.OrderItem? x = cart.OrderItems.FirstOrDefault(od => od.ProductId == product.ProductId);
        if (x != null)
            B0_product.AmmountInCart = x.AmountOfProduct;
        else
            B0_product.AmmountInCart = 0;
        return B0_product;
    }
    private DO.Product Bo_ProductToDo_Product(BO.Product product)
    {
        DO.Product DO_product = new DO.Product();
        DO_product.ProductId = product.ProductId;
        DO_product.ProductName = product.ProductName;
        DO_product.ProductPrice = (float)product.ProductPrice;
        DO_product.ProductCategory = (DO.Category)product.ProductCategory;
        DO_product.AmmountInStock = product.AmmountInStock;
        return DO_product;
    }

}
