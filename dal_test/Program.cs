using DO;

namespace Dal
{
    public class Program
    {
        private static DalProduct product = new DalProduct();
        private static DalOrderItem dalOrderItem = new DalOrderItem();
        private static DalOrder order = new DalOrder(); 

        public static void Main()
        {
            Console.WriteLine(
                "For products press 1\r\n" +
                "For orders press 2\r\n" +
                "For items on order press 3\r\n" +
                "To exit press 0\n");
            int choice = Console.Read();
            while (choice != 0)
            {
                switch (choice)
                {
                    case 1:
                        OptionsProduct();
                        break;
                    case 2:
                        OptionsOrder();
                        break;
                    case 3:
                        OptionsOrderItem();
                        break;
                    default:
                        break;
                        Console.WriteLine("You pressed a wrong key, try again\n");
                }
                choice = Console.Read();
            }
            return;
        }  //main menu


        public static void OptionsOrderItem() //Secondary menu - order item.
        {
            char action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: Adding a product\r\n" +
                "b: Single product display\r\n" +
                "c: Display of all products\r\n" +
                "d: Product update\r\n" +
                "e: Deleting a product\n" +
                "f: exit\n");
            action = (char)Console.Read();
            while (action != 'f')
            {
                switch (action)
                {
                    case 'a':
                        OrderItemFunctions.Add();
                        break;
                    case 'b':
                        OrderItemFunctions.DisplayById();
                        break;
                    case 'c':
                        OrderItemFunctions.DisplayAll();
                        break;
                    case 'd':
                        OrderItemFunctions.Update();
                        break;
                    case 'e':
                        OrderItemFunctions.Delete();
                        break;
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadKey();
            }
        }
        public static class OrderItemFunctions   //functions - order item
        {
            public static void Add()
            {
                OrderItem newOrderItem = new OrderItem();
                Console.WriteLine("enter an order number");
                newOrderItem.OrderId = Console.Read();
                Console.WriteLine("enter a product number");
                newOrderItem.ProductId = Console.Read();
                Console.WriteLine("enter an ammount number");
                newOrderItem.Amount = Console.Read();
                newOrderItem.Price = product.GetProductsId(newOrderItem.ProductId).ProductPrice * newOrderItem.Amount;
                dalOrderItem.AddOrderItem(newOrderItem);
            }
            public static void DisplayById()
            {
                Console.WriteLine("enter an order number\n");
                int IdOrder = Console.Read();
                Console.WriteLine("enter a procuct number\n");
                int IdProduct = Console.Read();
                Console.WriteLine(dalOrderItem.GetbyIdOfProductAndOrder(IdOrder, IdProduct)); //print only the ID
            }
            public static void DisplayAll()
            {
                List<OrderItem> ordItm = dalOrderItem.GetOrderItem();
                for (int i = 0; i < ordItm.Count; i++)
                    Console.WriteLine(ordItm[i]);
            }
            public static void Update()
            {
                Console.WriteLine("enter a order item number");
                int IdOrderItem = Console.Read();
                OrderItem ItemToUpdate = dalOrderItem.GetByOrderItemId(IdOrderItem);
                Console.WriteLine("Enter a product number you want to update\n");
                ItemToUpdate.ProductId = Console.Read();
                Console.WriteLine("Enter an order number you want to update\n");
                ItemToUpdate.OrderId = Console.Read();
                Console.WriteLine("enter the ammount of  the product you want to update\n");
                ItemToUpdate.Amount = Console.Read();
                ItemToUpdate.Price = product.GetProductsId(ItemToUpdate.ProductId).ProductPrice * ItemToUpdate.Amount;
                dalOrderItem.UpdateOrderItem(ItemToUpdate);
            }
            public static void Delete()
            {
                Console.WriteLine("enter a order item number");
                int IdOrderItem = Console.Read();
                dalOrderItem.DeleteOrderItem(IdOrderItem);
            }
        }

        public static void OptionsOrder()  //Secondary menu - order
        {
            char action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: Adding an order\r\n" +
                "b: Single order display\r\n" +
                "c: Display of all orders\r\n" +
                "d: order update\r\n" +
                "e: Deleting an order\n" +
                "f: exit\n");
            action = (char)Console.Read();
            while (action != 'f')
            {
                switch (action)
                {
                    case 'a':
                        OrderFunctions.Add();
                        break;
                    case 'b':
                        OrderFunctions.DisplayById();
                        break;
                    case 'c':
                        OrderFunctions.DisplayAll();
                        break;
                    case 'd':
                        OrderFunctions.Update();
                        break;
                    case 'e':
                        OrderFunctions.Delete();
                        break;
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = (char)Console.Read();
            }
        }
        public static class OrderFunctions   //functions - order
        {
            public static void Add()
            {
                Order NewOrder = new Order();
                Console.WriteLine("enter customer name\n");
                NewOrder.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter the customer's email address\n");
                NewOrder.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter customer address\n");
                NewOrder.CustomerAdress = Console.ReadLine();

                order.AddOrder(NewOrder); 
            }
            public static void DisplayById()
            {
                Console.WriteLine("enter an order number\n");
                int IdOrder = Console.Read();
                Console.WriteLine(order.GetOrderById(IdOrder));
            }
            public static void DisplayAll()
            {
                List<Order> ord = order.GetOrders();
                for (int i = 0; i < ord.Count; i++)
                    Console.WriteLine(ord[i]);
            }
            public static void Update()
            {
                Console.WriteLine("enter an order number");
                int IdOrderItem = Console.Read();
                Order OrderToUpdate = order.GetOrderById(IdOrderItem);
                Console.WriteLine("enter a customer name\n");
                OrderToUpdate.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter a customer email address\n");
                OrderToUpdate.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter a customer address\n");
                OrderToUpdate.CustomerAdress = Console.ReadLine();
                order.UpdateOrder(OrderToUpdate);
            }
            public static void Delete()
            {
                Console.WriteLine("enter a order number\n");
                int IdOrder = Console.Read();
                order.DeleteOrder(IdOrder);
            }
        }

        public static void OptionsProduct()  //Secondary menu - product
        {
            char action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: Adding a product\r\n" +
                "b: Single product display\r\n" +
                "c: Display of all products\r\n" +
                "d: Product update\r\n" +
                "e: Deleting a product\n" +
                                "f: exit\n");
            action = (char)Console.Read();
            while (action != 'f')
            {
                switch (action)
                {
                    case 'a':
                        ProductFunctions.Add();
                        break;
                    case 'b':
                        ProductFunctions.DisplayById();
                        break;
                    case 'c':
                        ProductFunctions.DisplayAll();
                        break;
                    case 'd':
                        ProductFunctions.Update();
                        break;
                    case 'e':
                        ProductFunctions.Delete();
                        break;
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = (char)Console.Read();
            }

        }
        public static class ProductFunctions   //functions - product
        {
            public static void Add()
            {
                Product NewProduct = new Product();
                Console.WriteLine("enter a product name\n");
                NewProduct.ProductName = Console.ReadLine();
                Console.WriteLine("enter a product number\n");
                NewProduct.ProductId = Console.Read();
                Console.WriteLine("Enter the amount of products in stock\n");
                NewProduct.AmmountInStock = Console.Read();
                Console.WriteLine("enter a product price\n");
                NewProduct.ProductPrice = Console.Read();
                Console.WriteLine("enter a product category\n");
                NewProduct.ProductCategory = Console.Read();
                product.AddProduct(NewProduct);
            }
            public static void DisplayById()
            {
                Console.WriteLine("enter a product number\n");
                int IdProduct = Console.Read();
                product.GetProductsId(IdProduct);
            }
            public static void DisplayAll()
            {
                List<Product> prd = product.GetProducts();
                for (int i = 0; i < prd.Count; i++)
                    Console.WriteLine(prd[i]);
            }
            public static void Update()
            {
                Product NewProduct = new Product();
                Console.WriteLine("enter a product name\n");
                NewProduct.ProductName = Console.ReadLine();
                Console.WriteLine("enter a product number\n");
                NewProduct.ProductId = Console.Read();
                Console.WriteLine("Enter the amount of products in stock\n");
                NewProduct.AmmountInStock = Console.Read();
                Console.WriteLine("enter a product price\n");
                NewProduct.ProductPrice = Console.Read();
                Console.WriteLine("enter a product category\n");
                NewProduct.ProductCategory = Console.Read();
                products.UpdateProduct(NewProduct);
            }
            public static void Delete()
            {
                Console.WriteLine("enter a product number\n");
                int IdProduct = Console.Read();
                products.DeleteProduct(IdProduct);
            }
        }
    }
}


