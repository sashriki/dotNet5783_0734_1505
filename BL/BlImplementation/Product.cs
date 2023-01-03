using DO;

internal class Product : BlApi.IProduct
{
    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// The function accepts a condition and returns a
    /// collection filtered according to the condition, if no condition is
    /// entered the function will return the entire list.
    /// </summary>
    /// <param name="condition"></param>
    /// <returns></returns>
    /// <exception cref="BO.NoElementsException"></exception>
    public IEnumerable<BO.ProductForList> getAllProducts(Func<BO.ProductForList?, bool>? condition = null)
    {
        IEnumerable<DO.Product?> DO_products = dal?.Product.GetAll();
        IEnumerable<BO.ProductForList> BO_productsForList = from item in DO_products
                                                            select Do_ProductToBo_ProductForList(item);
        if (!BO_productsForList.Any())
            throw new BO.NoElementsException("products");
        return condition is null ? BO_productsForList : BO_productsForList.Where(condition);
    }
    private BO.ProductForList BOProductProductForList(BO.Product product)
    {
        BO.ProductForList products = new BO.ProductForList();
        products.ProductId = product.ProductId;
        products.ProductName = product.ProductName;
        products.Price = product.ProductPrice;
        products.Category = (BO.Category)product.ProductCategory;
        return products;
    }
    private BO.Product BOProductToD0ProductNullable(DO.Product? product)
    {
        BO.Product DO_product = new BO.Product();
        DO_product.ProductId = product?.ProductId ?? 0;
        DO_product.ProductName = product?.ProductName;
        DO_product.ProductPrice = (float)product?.ProductPrice;
        DO_product.ProductCategory = (BO.Category)product?.ProductCategory;
        DO_product.AmmountInStock = product?.AmountInStock ?? 0;
        return DO_product;
    }

    public IEnumerable<BO.ProductForList> GetAllByCondition(Func<BO.ProductForList?, bool>? condition, IEnumerable<BO.ProductForList> productForLists)
        => productForLists.Where(condition);

    public BO.Product getByIdToMannage(int id)
    {
        DO.Product DO_product = new DO.Product();
        if (id > 0)
        {
            try
            {
                DO_product = dal.Product.GetById(id);
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
                DO_product = dal.Product.GetById(id);
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
        try
        {
            dal?.Product.GetByCondition(x => (x?.ProductName == BO_product?.ProductName) && (x?.ProductId != BO_product?.ProductId));
        }
        catch
        {
            if (BO_product.ProductId > 0)
                if (BO_product.ProductName != null)
                    if (BO_product.ProductPrice > 0)
                        if (BO_product.AmmountInStock > 0)
                        {
                            dal?.Product.Add(Bo_ProductToDo_Product(BO_product));
                            return;
                        }
        }
        throw new BO.InvalidInputBO("details");
    }
    public void removeProduct(int id)
    {
        IEnumerable<DO.Product?> products = dal?.Product.GetAll();
        bool flag = false;
        foreach (var item in products) { if (item?.ProductId == id) { flag = true; break; } }
        if (flag)
            try
            {
                dal?.Product.Delete(id);
            }
            catch (Exception ex)
            {
                throw new BO.BONotfoundException(ex);
            }
    }
    public void updateProduct(BO.Product BO_product)
    {
        
        try
        {
            dal?.Product.GetByCondition(x => (x?.ProductName == BO_product?.ProductName) && (x?.ProductId != BO_product?.ProductId));

        }
        catch
        {
            if (BO_product.ProductId > 0)
                if (BO_product.ProductName != null)
                    if (BO_product.ProductPrice > 0)
                        if (BO_product.AmmountInStock > 0)
                        {
                            dal?.Product.Update(Bo_ProductToDo_Product(BO_product));
                            return;
                        }
                        else
                        {
                            dal?.Product.Delete(BO_product.ProductId);
                            return;
                        }
        }
       
        throw new BO.InvalidInputBO("details");
    }
    private BO.ProductForList Do_ProductToBo_ProductForList(DO.Product? product)
    {
        BO.ProductForList products = new BO.ProductForList();
        products.ProductId = product?.ProductId ?? 0;
        products.ProductName = product?.ProductName;
        products.Price = product?.ProductPrice ?? 0;
        products.Category = (BO.Category)product?.ProductCategory;
        return products;
    }
    private BO.Product Do_ProductToBo_Product(DO.Product product)
    {
        BO.Product B0_product = new BO.Product();
        B0_product.ProductId = product.ProductId;
        B0_product.ProductName = product.ProductName;
        B0_product.AmmountInStock = product.AmountInStock;
        B0_product.ProductPrice = product.ProductPrice;
        B0_product.ProductCategory = (BO.Category)product.ProductCategory;
        return B0_product;
    }
    private BO.ProductItem Do_ProductToBo_ProductItem(DO.Product product, BO.Cart cart)
    {
        BO.ProductItem B0_product = new BO.ProductItem();
        B0_product.ProductId = product.ProductId;
        B0_product.ProductName = product.ProductName;
        if (product.AmountInStock > 0)
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
        DO_product.AmountInStock = product.AmmountInStock;
        return DO_product;
    }


}
