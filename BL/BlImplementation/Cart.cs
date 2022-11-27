using BlApi;
using Dal;
using DalApi;
using System.Data;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;
//לשים לב- לזרוק שמות או מספרי זהות? אם כן צריך שיהיה מתואם עם שכבת הנתונים
internal class Cart :ICart
{
    public IDal Dal = new DalList();
    public BO.Cart AddProductToCart(BO.Cart newCart, int Iproduct) 
    {//אם המוצר לא קיים או שנגמר במלאי יש לזרוק חריגה
     //מה אם אדם רוצה להוסיף כמה מוצרים ולא אחד
        BO.orderItem ordItemBO= new BO.orderItem();
        DO.Product productDO = new DO.Product();
        foreach (var x in newCart.orderItems)
            if (x.productId == Iproduct)
            {
                productDO = Dal.IProduct.GetById(x.productId);
                if (productDO.AmmountInStock>0)
                {
                    x.amountOfProduct++;
                    x.finalPriceOfProduct += productDO.ProductPrice;
                    newCart.totalPrice += productDO.ProductPrice;
                }
                return newCart;
            }
        productDO = Dal.IProduct.GetById(Iproduct);
        if(productDO.AmmountInStock > 0)
        {
            newCart.totalPrice += productDO.ProductPrice;
            ordItemBO = ChangingItemTypeInOrder(productDO);
            ordItemBO.priceOfProduct += productDO.ProductPrice;
            newCart.orderItems.Append(ordItemBO);
        }
        return newCart;
    }
    public BO.orderItem ChangingItemTypeInOrder(DO.Product productDO)
    {
        BO.orderItem ordItemBO = new BO.orderItem();
        //לברר אם אפשר להשתמש במס רץ של של שכבת הנתונים
        ordItemBO.orderItemId = //ordItemDO.OrderItemId;
        ordItemBO.productId= productDO.ProductId;
        ordItemBO.productName= productDO.ProductName;
        ordItemBO.priceOfProduct= productDO.ProductPrice;
        //לפי מה יודעים כמה הקונה רוצה מהמוצר
        //ordItemBO.amountOfProduct = productDO.AmmountInStock;
        ordItemBO.finalPriceOfProduct = ordItemBO.amountOfProduct * ordItemBO.priceOfProduct;
        return ordItemBO;
    }
    public BO.Cart UpdateAmount(BO.Cart newCart, int Iproduct, int amount)
    {
        BO.orderItem ordItemBO = new BO.orderItem();
        DO.Product productDO = new DO.Product();
        foreach (var x in newCart.orderItems)
            if (x.productId == Iproduct)
            {
                if (x.amountOfProduct == amount)
                    return newCart;
                ordItemBO = x;
                break;
            }
        //חרגיה למקרה שהוא לא מצא את האובייקט
        if (ordItemBO.amountOfProduct < amount)
        {
            for (int i = ordItemBO.amountOfProduct; i < amount; i++)
                AddProductToCart(newCart, Iproduct);
            return newCart;
        }
        productDO = Dal.IProduct.GetById(Iproduct);
        if (ordItemBO.amountOfProduct > amount)
        {
            int QuantityDifference = ordItemBO.amountOfProduct - amount;
            ordItemBO.priceOfProduct -= (productDO.ProductPrice * QuantityDifference);
            ordItemBO.amountOfProduct -= QuantityDifference;
            newCart.totalPrice-= (productDO.ProductPrice * QuantityDifference);
        }
        if (amount == 0)
        {
            newCart.totalPrice -= (productDO.ProductPrice * ordItemBO.amountOfProduct);
            newCart.orderItems = newCart.orderItems.
                Where(x => x.productId != Iproduct);
        }
        return newCart;
    }
    public void OrderConfirmation(BO.Cart newCart)
    {
        List<Exception> listOfException=new List<Exception>();
        DO.Product productDO = new DO.Product();
        if (newCart.CustomerAdress == "")
            throw new DataMissingException(
                "The customer address is missing to complete the operation"); 
        if (newCart.CustomerName == "")
            throw new DataMissingException(
                    "The customer name is missing to complete the operation");
        if(newCart.CustomerEmail=="")
            throw new DataMissingException(
                    "Email address of the customer in the company");
        if(!newCart.CustomerAdress.EndsWith("@gmail.com"))
            throw new InvalidInputBO(
                    "Customer email address is invalid");
        foreach (var item in newCart.orderItems)
        {
            try
            {
                productDO = Dal.IProduct.GetById(item.productId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (item.amountOfProduct <= 0)
                throw new InvalidInputBO("Error! quantity for {item.productName} is invalid");
            if (productDO.AmmountInStock < item.amountOfProduct)
                throw new ItemMissingException("{item.productName} out of stock");
        }
        DO.Order NewOrderDO = new DO.Order();
        NewOrderDO.OrderDate= DateTime.Now;
        NewOrderDO.ShipDate= DateTime.MinValue;
        NewOrderDO.DeliveryDate= DateTime.MinValue;
        NewOrderDO.CustomerAdress= newCart.CustomerAdress;  
        NewOrderDO.CustomerName= newCart.CustomerName;
        NewOrderDO.CustomerEmail= newCart.CustomerEmail;
        int IdOrder=Dal.Iorder.Add(NewOrderDO);
        foreach (var item in newCart.orderItems)
            Dal.Iorderitem.Add(ChangingFromBOToDO(item,IdOrder));
    }
    public DO.OrderItem ChangingFromBOToDO(BO.orderItem NewOrderBO,int ID)
    {
        DO.OrderItem NewOrderDO=new DO.OrderItem();
        return NewOrderDO;
    }
}
