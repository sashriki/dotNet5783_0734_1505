using DO;
namespace Dal;

internal static class DataSource
{
    //lists
    static internal List<Product> products = new List<Product>();
    static internal List<Order> orders = new List<Order>();
    static internal List<OrderItem> orderItems = new List<OrderItem>();

    internal static class Config
    {
        internal static int productIndex = 0;
        internal static int orderItemIndex = 0;
        internal static int orderIndex = 0;

    }

    //randoms
    static readonly Random random = new Random(); // for the random numbers
    const int InitialNumOfOrders = 100;
    const int InitialNumOfOrderItems = 20;
    const int InitialNumOfproducts = 200;

    static DataSource()
    {
        Init();
    }

    private static void Init()
    {
        InitProduct();
        InitOrder();
        InitOrderItem();
    }
    private static void InitProduct()
    {
        Array values = Enum.GetValues(typeof(Category));
        Category randomCategory;
        for (int i = 0; i < products.Count(); i++)
        {
            randomCategory = (Category)values.GetValue(random.Next(values.Length));
            products[i] = new Product()
            {
                ProductId = random.Next(1000000000),
                ProductName = "product" + i,
                ProductCategory = randomCategory,
                AmmountInStock = random.Next(100),
                ProductPrice=random.Next(500) + random.NextDouble()
            };
        }
    }
    private static void InitOrder() 
    {
    
    }
    private static void InitOrderItem() 
    {
    
    }


}