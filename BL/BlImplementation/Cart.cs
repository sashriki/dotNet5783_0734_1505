using BlApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Cart :ICart
{
    public IDal Dal = new DalList();
    public BO.Cart AddProductToCart(BO.Cart newCart, int IdorderItem) 
    {
        BO.orderItem ordItemBO= new BO.orderItem();
        DO.OrderItem ordItemDO = new DO.OrderItem();
        foreach (var x in newCart.orderItems)
            if (x.orderItemId == IdorderItem)
            {
                ordItemDO= Dal.Iorderitem.GetById(x.orderItemId);
                if (ordItemDO.Amount>0)
                {
                    ordItemDO.Amount--;
                    x.amountOfProduct++;
                    x.finalPriceOfProduct = ordItemDO.Price;
                    newCart.totalPrice += ordItemDO.Price;
                }
                return newCart;
            }
        ordItemDO = Dal.Iorderitem.GetById(IdorderItem);
        if(ordItemDO.Amount>0)
        {
            ordItemDO.Amount--;
            newCart.totalPrice += ordItemDO.Price;
            ordItemBO = ChangingItemTypeInOrder(ordItemDO);
            ordItemBO.priceOfProduct += ordItemDO.Price;
            newCart.orderItems.Append(ordItemBO);
        }
        return newCart;
    }
    public BO.orderItem ChangingItemTypeInOrder(DO.OrderItem ordItemDO)
    {
        BO.orderItem ordItemBO = new BO.orderItem();
        ordItemBO.orderItemId = ordItemDO.OrderItemId;
        ordItemBO.productId=ordItemDO.ProductId;
        //  ordItemBO.productName= ????????
        ordItemBO.priceOfProduct= ordItemDO.Price;
        ordItemBO.amountOfProduct = ordItemDO.Amount;
        //ordItemBO.finalPriceOfProduct= ???????????
        return ordItemBO;
    }
    public BO.Cart UpdateAmount(BO.Cart newCart, int IdorderItem, int amount)
    {
        BO.orderItem ordItemBO = new BO.orderItem();
        DO.OrderItem ordItemDO = new DO.OrderItem();
        foreach (var x in newCart.orderItems)
            if (x.orderItemId == IdorderItem)
            {
                ordItemBO = x;
                if (x.amountOfProduct == amount)
                    return newCart;
            }
        if (ordItemBO.amountOfProduct < amount)
            AddProductToCart(newCart, IdorderItem);//לבדוק מה קורה אם צריך להוסיף כמה איברים ולא אחד
        ordItemDO = Dal.Iorderitem.GetById(IdorderItem);
        if (ordItemBO.amountOfProduct > amount)
        {
            int QuantityDifference = ordItemBO.amountOfProduct - amount;
            ordItemBO.priceOfProduct -= (ordItemDO.Price * QuantityDifference);
            ordItemBO.amountOfProduct -= QuantityDifference;
            newCart.totalPrice-= (ordItemDO.Price * QuantityDifference);
        }
        if (amount == 0)
        {
            newCart.totalPrice -= (ordItemDO.Price * ordItemBO.amountOfProduct);
            //newCart.orderItems צריך למחוק את המור בהזמנה.
        }
        return newCart;
    }
    public void OrderConfirmation(BO.Cart newCart)
    { }
}
