using BO;
using DO;
using System.Data;
internal class Cart : BlApi.ICart
{

    DalApi.IDal? dal = DalApi.Factory.Get();
    /// <summary>
    /// To add a product to the shopping cart
    /// </summary>
    /// <param name="newCart"></param>
    /// <param name="IDproduct"></param>
    /// <returns></returns>
    /// <exception cref="BO.BONotfoundException"></exception>
    /// <exception cref="ItemMissingException"></exception>
    public BO.Cart AddProductToCart(BO.Cart newCart, int IDproduct)
    {
        DO.Product productDO = new DO.Product();
        try
        {
            productDO = dal.IProduct.GetById(IDproduct);
        }
        catch (Exception ex)
        {   //If no product is found with the received ID
            throw new BO.BONotfoundException(ex);
        }
        //Creating a new order item object
        BO.OrderItem ordItemBO = new BO.OrderItem();
        //Finding the item by ID from the list of items in the order
        BO.OrderItem? ord = newCart.OrderItems.FirstOrDefault(od => od.ProductId == IDproduct);
        newCart.OrderItems.ToList();
        if (ord != null) //If the item is found   
        {
            //If there are enough items in stock
            if (productDO.AmmountInStock >= ord.AmountOfProduct + 1)
                newCart.OrderItems.Where(od => od.ProductName == productDO.ProductName).First().AmountOfProduct++;
            else
                throw new ItemMissingException(productDO.ProductName);
        }
        else //If the item does not exist in the list
        {
            //If there are enough items in stock
            if (productDO.AmmountInStock < 1)
                throw new ItemMissingException(productDO.ProductName);
            //Adding the item to the list
            ordItemBO.OrderItemId = 0;
            ordItemBO.ProductId = productDO.ProductId;
            ordItemBO.PriceOfProduct = productDO.ProductPrice;
            ordItemBO.AmountOfProduct = 1;
            ordItemBO.FinalPriceOfProduct = productDO.ProductPrice;
            newCart.TotalPrice+= productDO.ProductPrice;
            newCart.OrderItems.Add(ordItemBO);
        }
        return newCart;
    }
    /// <summary>
    /// Update product quantity in shopping basket
    /// </summary>
    /// <param name="newCart"></param>
    /// <param name="IDproduct"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    /// <exception cref="NotfoundException"></exception>
    /// <exception cref="DataMissingException"></exception>
    public BO.Cart UpdateAmount(BO.Cart newCart, int IDproduct, int amount)
    {
        //Creating a new order item object
        DO.Product productDO = new DO.Product();
        //Finding the item by ID from the list of items in the order
        BO.OrderItem? ordBO = newCart.OrderItems.Where(od => od.ProductId == IDproduct).First();
        if (ordBO == null) //If the item is not in the shopping cart
            throw new NotfoundException("order item");
        if (ordBO.AmountOfProduct == amount)//If the quantity is updated
            return newCart;
        //If the item is in the shopping cart and the quantity is not updated
        productDO = dal.IProduct.GetById(IDproduct);
        if (ordBO.AmountOfProduct < amount)//to increase quantity
        {
            int dif = amount - ordBO.AmountOfProduct;
            if (productDO.AmmountInStock >= amount)//If there is enough in stock
            {
                newCart.OrderItems.Where(od => od.ProductId == IDproduct).First().
                    FinalPriceOfProduct += dif * ordBO.PriceOfProduct;
                newCart.OrderItems.Where(od => od.ProductId == IDproduct).First().
                    AmountOfProduct = amount;
                newCart.TotalPrice += dif * ordBO.PriceOfProduct;
            }
            else
                throw new DataMissingException(ordBO.ProductName);
            return newCart;
        }
        if (ordBO.AmountOfProduct > amount)//to reduce quantity
        {
            int dif = ordBO.AmountOfProduct - amount;
            newCart.OrderItems.Where(od => od.ProductId == IDproduct).First().
                FinalPriceOfProduct -= dif * ordBO.PriceOfProduct;
            newCart.OrderItems.Where(od => od.ProductId == IDproduct).First().
                AmountOfProduct = amount;
            newCart.TotalPrice -= dif * ordBO.PriceOfProduct;
        }
        if (amount == 0)//To remove a product from a shopping cart
        {
            newCart.TotalPrice -= (productDO.ProductPrice * ordBO.AmountOfProduct);
            newCart.OrderItems.Remove(ordBO);
            //newCart.OrderItems = newCart.OrderItems.
            //    Where(x => x.ProductId != IDproduct);
        }
        return newCart;
    }
    /// <summary>
    /// to make an order
    /// </summary>
    /// <param name="newCart"></param>
    public void OrderConfirmation(BO.Cart newCart)
    {
        try
        {
            check(newCart);//Input integrity check
            //Creating an order and initializing the fields:
            DO.Order NewOrderDO = new DO.Order();
            NewOrderDO.OrderDate = DateTime.Now;
            NewOrderDO.ShipDate = DateTime.MinValue;
            NewOrderDO.DeliveryDate = DateTime.MinValue;
            NewOrderDO.CustomerAdress = newCart.CustomerAdress;
            NewOrderDO.CustomerName = newCart.CustomerName;
            NewOrderDO.CustomerEmail = newCart.CustomerEmail;
            int IdOrder = dal.Iorder.Add(NewOrderDO);
            foreach (var item in newCart.OrderItems)
                dal?.Iorderitem.Add(ChangingFromBOToDO(item, IdOrder));
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
    /// <summary>
    /// Input integrity check
    /// </summary>
    /// <param name="newCart"></param>
    /// <exception cref="BO.BONotfoundException"></exception>
    /// <exception cref="InvalidInputBO"></exception>
    /// <exception cref="ItemMissingException"></exception>
    /// <exception cref="DataMissingException"></exception>
    private void check(BO.Cart newCart)
    {
        DO.Product productDO = new DO.Product();
        //Testing for product integrity
        foreach (var item in newCart.OrderItems)
        {
            try
            {
                productDO = dal.IProduct.GetById(item.ProductId);
            }
            catch (DO.NotfoundException ex)
            {
                throw new BO.BONotfoundException(ex);
            }
            if (item.AmountOfProduct <= 0)
                throw new InvalidInputBO(item.ProductName);
            if (productDO.AmmountInStock < item.AmountOfProduct)
                throw new ItemMissingException(item.ProductName, productDO.AmmountInStock);
        }
        //Checking for correctness of customer details
        if (newCart.CustomerAdress == "")
            throw new DataMissingException("address");
        if (newCart.CustomerName == "")
            throw new DataMissingException("name");
        if (newCart.CustomerEmail == "")
            throw new DataMissingException("email");
        if (!newCart.CustomerEmail.EndsWith("@gmail.com"))
            throw new InvalidOperationException("email");
    }
    /// <summary>
    /// Test for converting from an Item object in the order of BO to DO
    /// </summary>
    /// <param name="NewOrderBO"></param>
    /// <param name="IdOrder"></param>
    /// <returns></returns>
    public DO.OrderItem ChangingFromBOToDO(BO.OrderItem NewOrderBO, int IdOrder)
    {
        DO.OrderItem NewOrderDO = new DO.OrderItem();
        NewOrderDO.Price = NewOrderBO.PriceOfProduct;
        NewOrderDO.ProductId = NewOrderBO.ProductId;
        NewOrderDO.Amount = NewOrderBO.AmountOfProduct;
        NewOrderDO.OrderId = IdOrder;
        return NewOrderDO;
    }
}
