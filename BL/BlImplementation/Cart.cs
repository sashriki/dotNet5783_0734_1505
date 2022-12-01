using BO;
using DO;
using System.Data;
internal class Cart : BlApi.ICart
{
    public DalApi.IDal Dal = new Dal.DalList();
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
            productDO = Dal.IProduct.GetById(IDproduct);
        }
        catch (Exception ex)
        {   //If no product is found with the received ID
            throw new BO.BONotfoundException(ex);
        }
        //Creating a new order item object
        BO.orderItem ordItemBO = new BO.orderItem();
        //Finding the item by ID from the list of items in the order
        BO.orderItem? ord = newCart.orderItems.FirstOrDefault(od => od.productId == IDproduct);
        newCart.orderItems.ToList();
        if (ord != null) //If the item is found   
        {
            //If there are enough items in stock
            if (productDO.AmmountInStock >= ord.amountOfProduct + 1)
                newCart.orderItems.Where(od => od.productName == productDO.ProductName).First().amountOfProduct++;
            else
                throw new ItemMissingException(productDO.ProductName);
        }
        else //If the item does not exist in the list
        {
            //If there are enough items in stock
            if (productDO.AmmountInStock < 1)
                throw new ItemMissingException(productDO.ProductName);
            //Adding the item to the list
            ordItemBO.orderItemId = 0;
            ordItemBO.productId = productDO.ProductId;
            ordItemBO.priceOfProduct = productDO.ProductPrice;
            ordItemBO.amountOfProduct = 1;
            ordItemBO.finalPriceOfProduct = productDO.ProductPrice;
            newCart.orderItems.Add(ordItemBO);
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
        BO.orderItem? ordBO = newCart.orderItems.Where(od => od.productId == IDproduct).First();
        if (ordBO == null) //If the item is not in the shopping cart
            throw new NotfoundException("order item");
        if (ordBO.amountOfProduct == amount)//If the quantity is updated
            return newCart;
        //If the item is in the shopping cart and the quantity is not updated
        productDO = Dal.IProduct.GetById(IDproduct);
        if (ordBO.amountOfProduct < amount)//to increase quantity
        {
            int dif = amount - ordBO.amountOfProduct;
            if (productDO.AmmountInStock >= amount)//If there is enough in stock
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
        if (ordBO.amountOfProduct > amount)//to reduce quantity
        {
            int dif = ordBO.amountOfProduct - amount;
            newCart.orderItems.Where(od => od.productId == IDproduct).First().
                finalPriceOfProduct -= dif * ordBO.priceOfProduct;
            newCart.orderItems.Where(od => od.productId == IDproduct).First().
                amountOfProduct = amount;
            newCart.totalPrice -= dif * ordBO.priceOfProduct;
        }
        if (amount == 0)//To remove a product from a shopping cart
        {
            newCart.totalPrice -= (productDO.ProductPrice * ordBO.amountOfProduct);
            newCart.orderItems.Remove(ordBO);
            //newCart.orderItems = newCart.orderItems.
            //    Where(x => x.productId != IDproduct);
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
    public DO.OrderItem ChangingFromBOToDO(BO.orderItem NewOrderBO, int IdOrder)
    {
        DO.OrderItem NewOrderDO = new DO.OrderItem();
        NewOrderDO.Price = NewOrderBO.priceOfProduct;
        NewOrderDO.ProductId = NewOrderBO.productId;
        NewOrderDO.Amount = NewOrderBO.amountOfProduct;
        NewOrderDO.OrderId = IdOrder;
        return NewOrderDO;
    }
}
