using BO;
using DO;
using System.Data;
internal class Cart : BlApi.ICart
{
    public DalApi.IDal Dal = new Dal.DalList();
    public BO.Cart AddProductToCart(BO.Cart newCart, int IDproduct)
    {
        DO.Product productDO = new DO.Product();   
        try
        {
            productDO=Dal.IProduct.GetById(IDproduct);   
        }
        catch(Exception ex)
        {
            throw new BO.BONotfoundException(ex);
        }
        BO.orderItem ordItemBO = new BO.orderItem();
        BO.orderItem? ord = newCart.orderItems.Where(od => od.productId == IDproduct).First();
        if (ord != null)
        {
            if (productDO.AmmountInStock >= ord.amountOfProduct + 1)
                newCart.orderItems.Where(od => od.productName == productDO.ProductName).First().amountOfProduct++;
            else
                throw new ItemMissingException(productDO.ProductName);
        }
        else
        {
            if (productDO.AmmountInStock < 1)
                throw new ItemMissingException(productDO.ProductName);
            ordItemBO.orderItemId = 0;
            ordItemBO.productId = productDO.ProductId;
            ordItemBO.priceOfProduct = productDO.ProductPrice;
            ordItemBO.amountOfProduct = 1;
            ordItemBO.finalPriceOfProduct = productDO.ProductPrice;
            newCart.orderItems.Append(ordItemBO);
        }
        return newCart;
    }
    public BO.Cart UpdateAmount(BO.Cart newCart, int IDproduct, int amount)
    {
        DO.Product productDO = new DO.Product();
        BO.orderItem? ordBO = newCart.orderItems.Where(od => od.productId == IDproduct).First();
        if (ordBO == null)
            throw new NotfoundException("order item");
        if (ordBO.amountOfProduct == amount)
            return newCart;
        productDO = Dal.IProduct.GetById(IDproduct);
        if (ordBO.amountOfProduct < amount)
        {
            int dif = amount - ordBO.amountOfProduct;
            if (productDO.AmmountInStock >= amount)
            {
                newCart.orderItems.Where(od => od.productId == IDproduct).First().
                    finalPriceOfProduct += dif * ordBO.priceOfProduct;
                newCart.orderItems.Where(od => od.productId == IDproduct).First().
                    amountOfProduct = amount;
                newCart.totalPrice += dif * ordBO.priceOfProduct;
            }
            else
                throw new DataMissingException(ordBO.productName);
            return newCart;
        }
        if (ordBO.amountOfProduct > amount)
        {
            int dif =  ordBO.amountOfProduct- amount;
            newCart.orderItems.Where(od => od.productId == IDproduct).First().
                finalPriceOfProduct -= dif * ordBO.priceOfProduct;
            newCart.orderItems.Where(od => od.productId == IDproduct).First().
                amountOfProduct = amount;
            newCart.totalPrice -= dif * ordBO.priceOfProduct;
        }
        if (amount == 0)
        {
            newCart.totalPrice -= (productDO.ProductPrice * ordBO.amountOfProduct);
            newCart.orderItems = newCart.orderItems.
                Where(x => x.productId != IDproduct);
        }
        return newCart;
    }
    public void OrderConfirmation(BO.Cart newCart)
    {
        try
        {
            check(newCart);
           // List<Exception> listOfException = new List<Exception>();
            DO.Order NewOrderDO = new DO.Order();
            NewOrderDO.OrderDate = DateTime.Now;
            NewOrderDO.ShipDate = DateTime.MinValue;
            NewOrderDO.DeliveryDate = DateTime.MinValue;
            NewOrderDO.CustomerAdress = newCart.CustomerAdress;
            NewOrderDO.CustomerName = newCart.CustomerName;
            NewOrderDO.CustomerEmail = newCart.CustomerEmail;
            int IdOrder = Dal.Iorder.Add(NewOrderDO);
            foreach (var item in newCart.orderItems)
                Dal.Iorderitem.Add(ChangingFromBOToDO(item, IdOrder));
        }
        catch (BO.DataMissingException ex)
        {
            throw ex;
        }
        catch (BO.ItemMissingException ex)
        {
            throw ex;
        }
        catch (BO.InvalidInputBO ex)
        {
            throw ex;
        }
    }

    private void check(BO.Cart newCart)
    {
        DO.Product productDO = new DO.Product();
        foreach (var item in newCart.orderItems)
        {
            try
            {
                productDO = Dal.IProduct.GetById(item.productId);                
            }
            catch (DO.NotfoundException ex)
            {
                throw new BO.BONotfoundException(ex);
            }
            if (item.amountOfProduct <= 0)
                throw new InvalidInputBO(item.productName);
            if (productDO.AmmountInStock < item.amountOfProduct)
                throw new ItemMissingException(item.productName, productDO.AmmountInStock);
        }
        if (newCart.CustomerAdress == "")
            throw new DataMissingException("address");
        if (newCart.CustomerName == "")
            throw new DataMissingException("name");
        if (newCart.CustomerEmail == "")
            throw new DataMissingException("email");
        if (!newCart.CustomerAdress.EndsWith("@gmail.com"))
            throw new DataMissingException("email");
    }

    public DO.OrderItem ChangingFromBOToDO(BO.orderItem NewOrderBO, int IdOrder)
    {
        DO.OrderItem NewOrderDO = new DO.OrderItem();
        NewOrderDO.Price= NewOrderBO.priceOfProduct;
        NewOrderDO.ProductId = NewOrderBO.productId;
        NewOrderDO.Amount = NewOrderBO.amountOfProduct;
        NewOrderDO.OrderId= IdOrder;
        return NewOrderDO;
    }
}
