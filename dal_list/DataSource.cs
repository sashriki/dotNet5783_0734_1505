using DO;
namespace Dal;

internal static class DataSource
{
    //lists
    static public List<Product?> products = new List<Product?>();
    static public List<Order?> orders = new List<Order?>();
    static public List<OrderItem?> orderItems = new List<OrderItem?>();

    //continuous numbers
    internal static class Config
    {
        internal static int productIndex = 124568;
        internal static int orderItemIndex = 200;
        internal static int orderIndex = 1;

    }

    //randoms
    static readonly Random random = new Random(); // for the random numbers

    //consts
    const int InitialNumOfOrders = 100;
    const int InitialNumOfOrderItems = 20;
    const int InitialNumOfproducts = 200;

    //initialize
    static DataSource()
    {
        Init();
    }
    private static void Init()
    {
        InitProduct();
        InitOrder();
        InitOrderItem();

        //ToolsXML.SaveListToXMLSerializer(products, "xmlProduct.xml");
        //ToolsXML.SaveListToXMLSerializer(orders, "xmlOrder.xml");
        //ToolsXML.SaveListToXMLSerializer(orderItems, "xmlOrderItem.xml");
    }
    /// <summary>
    /// Product list initialization
    /// </summary>
    private static void InitProduct()
    {
        for (int i = 0; i < InitialNumOfproducts/*200*/; i++)
        {//The loop will create new products and add to the list
            Product newProduct = new Product(); //Creating an object
            newProduct.ProductId = Config.productIndex++; //ID number by running number
            newProduct.ProductName = "product" + i;
            newProduct.ProductCategory = (Category)(random.Next(0, 5)); ;
            newProduct.ProductPrice = Math.Round(random.Next(500) + random.NextDouble(),1);
            if (random.Next(0, 100) > 5)
            {
                newProduct.AmountInStock = random.Next(20, 100);
            }
            else
            {
                newProduct.AmountInStock = 1;
            }
            products.Add(newProduct);  
        }
    }
    private static void InitOrder()
    {//Initialization for orders
        for (int i = 0; i < InitialNumOfOrders; i++)
        {
            Order newOrder = new Order();
            newOrder.CustomerName = "Costumer " + i;
            newOrder.CustomerAdress = "Adress " + i;
            newOrder.CustomerEmail = "customer" + i + "@gmail.com";
            newOrder.OrderId = Config.orderIndex++;
            newOrder.OrderDate = DateTime.Now - new TimeSpan(random.Next(0, 5), random.Next(0, 28), random.Next(0, 12), random.Next(0, 60), random.Next(0, 60));
            if (random.Next(0, 100) < 80)
            {
                newOrder.ShipDate = newOrder.OrderDate + new TimeSpan(random.Next(0, 7), random.Next(0, 12), random.Next(0, 59), random.Next(0, 59));
                if (random.Next(0, 100) < 60)
                    newOrder.DeliveryDate = newOrder.ShipDate + new TimeSpan(random.Next(0, 14), random.Next(0, 12), random.Next(0, 59), random.Next(0, 59));
                else
                    newOrder.DeliveryDate = null;
            }

            else
            {
                newOrder.ShipDate = null;
                newOrder.DeliveryDate = null;
            }
            orders.Add(newOrder);
        }
    }
    private static void InitOrderItem()
    {
        for (int i = 0; i < 0.2 * InitialNumOfOrders; i++)
        {
            OrderItem newOrderItem = new OrderItem();
            newOrderItem.OrderItemId = Config.orderItemIndex++;
            newOrderItem.OrderId = orders[random.Next(0, InitialNumOfOrders)].Value.OrderId;
            int index = random.Next(0, InitialNumOfproducts);
            newOrderItem.ProductId = products[index].Value.ProductId;
            newOrderItem.Amount = random.Next(0, 5);
            newOrderItem.Price = products[index].Value.ProductPrice;
            orderItems.Add(newOrderItem);
        }
    }
}