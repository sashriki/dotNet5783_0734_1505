using DO;
using DalApi;
using System.Linq.Expressions;

namespace Dal
{
    public class Program
    {
        //private static DalProduct product = new DalProduct();
        //private static DalOrderItem dalOrderItem = new DalOrderItem();
        //private static DalOrder order = new DalOrder();
        private static IDal Object = new DalList(); //??????????
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine(
                "For products press 1\r\n" +
                "For orders press 2\r\n" +
                "For items on order press 3\r\n" +
                "To exit press 0\n");
                int choice = Console.Read();
                switch (choice)
                {
                    case '1'://to perform operations for a product
                        OptionsProduct();
                        break;
                    case '2'://to perform actions for an order
                        OptionsOrder();
                        break;
                    case '3'://To perform actions for items on the order
                        OptionsOrderItem();
                        break;
                    case '0'://to end the program
                        Console.WriteLine("bye bye");
                        return;
                    default://for incorrect input
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }

            }
    
            return;
        }  //main menu

        public static void OptionsOrderItem() //Secondary menu - order item.
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: Adding a product\r\n" +
                "b: Single product display\r\n" +
                "c: Display of all products\r\n" +
                "d: Product update\r\n" +
                "e: Deleting a product\n" +
                "f: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                switch (action)
                {
                    case "a"://To add an ordered item to the list
                        OrderItemFunctions.Add();
                        break;
                    case "b"://To print an item in an order according to an ID card
                        OrderItemFunctions.DisplayById();
                        break;
                    case "c"://To print all items in orders
                        OrderItemFunctions.DisplayAll();
                        break;
                    case "d"://To update an item in the order
                        OrderItemFunctions.Update();
                        break;
                    case "e"://To delete an item in the order
                        OrderItemFunctions.Delete();
                        break;
                    case "f"://to finish
                        return;
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadLine();
            }
        }
        public static class OrderItemFunctions   //functions - order item
        {
            public static void Add()
            {//To add an ordered item to the list
                OrderItem newOrderItem = new OrderItem();
                Console.WriteLine("enter an order number");
                newOrderItem.OrderId = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a product number");
                newOrderItem.ProductId = int.Parse(Console.ReadLine());
                Console.WriteLine("enter an ammount number");
                newOrderItem.Amount = int.Parse(Console.ReadLine());
                newOrderItem.Price = Object.IProduct.GetById(newOrderItem.ProductId).ProductPrice * newOrderItem.Amount;
                Object.Iorderitem.Add(newOrderItem);
            }
            public static void DisplayById()
            {//To print an item in an order according to an ID card
                Console.WriteLine("enter an order number\n");
                int IdOrder = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a procuct number\n");
                int IdProduct = int.Parse(Console.ReadLine());
                Console.WriteLine(Object.Iorderitem.GetbyIdOfProductAndOrder(IdOrder, IdProduct)); //print only the ID
            }
            public static void DisplayAll() /////???????????????????????????????????????????????????????
            {//To print all items in orders 
                //List<OrderItem> ordItm = Object.Iorderitem.GetAll();
                IEnumerable<OrderItem> ordItm = Object.Iorderitem.GetAll();
                for (int i = 0; i < ordItm.Count(); i++)
                    Console.WriteLine(ordItm.);
            }
            public static void Update()
            {//To update an item in the order
                Console.WriteLine("enter a order item number");
                int IdOrderItem = int.Parse(Console.ReadLine());
                OrderItem ItemToUpdate = Object.Iorderitem.GetById(IdOrderItem);
                Console.WriteLine("Enter a product number you want to update\n");
                ItemToUpdate.ProductId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter an order number you want to update\n");
                ItemToUpdate.OrderId = int.Parse(Console.ReadLine());
                Console.WriteLine("enter the ammount of  the product you want to update\n");
                ItemToUpdate.Amount = int.Parse(Console.ReadLine());
                ItemToUpdate.Price = Object.IProduct.GetById(ItemToUpdate.ProductId).ProductPrice * ItemToUpdate.Amount;
                Object.Iorderitem.Update(ItemToUpdate);
            }
            public static void Delete()
            {//To delete an item in the order
                Console.WriteLine("enter a order item number");
                int IdOrderItem = int.Parse(Console.ReadLine());
                Object.Iorderitem.Delete(IdOrderItem);
            }
        }

        public static void OptionsOrder()  //Secondary menu - order
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: Adding an order\r\n" +
                "b: Single order display\r\n" +
                "c: Display of all orders\r\n" +
                "d: order update\r\n" +
                "e: Deleting an order\n" +
                "f: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                switch (action)
                {
                    case "a"://To add an order to the list
                        OrderFunctions.Add();
                        break;
                    case "b"://To print an order by order ID number
                        OrderFunctions.DisplayById();
                        break;
                    case "c"://To print all orders in the list
                        OrderFunctions.DisplayAll();
                        break;
                    case "d"://To update an order
                        OrderFunctions.Update();
                        break;
                    case "e"://To delete an order
                        OrderFunctions.Delete();
                        break;
                    case "f"://to finish
                        return; 
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadLine();
            }
        }
        public static class OrderFunctions   //functions - order
        {
            public static void Add()
            {//To add an order to the list
                Order NewOrder = new Order();
                Console.WriteLine("enter customer name\n");
                NewOrder.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter the customer's email address\n");
                NewOrder.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter customer address\n");
                NewOrder.CustomerAdress = Console.ReadLine();
                NewOrder.OrderDate=DateTime.Now;
                NewOrder.DeliveryDate=DateTime.MinValue;
                NewOrder.ShipDate = DateTime.MinValue;
                order.AddOrder(NewOrder); 
            }

            public static void DisplayById()
            {//To print an order by order ID number
                Console.WriteLine("enter an order number\n");
                int IdOrder = int.Parse(Console.ReadLine());
                Console.WriteLine(order.GetOrderById(IdOrder));
            }
            public static void DisplayAll()
            {//To print all orders in the list
                List<Order> ord = order.GetOrders();
                for (int i = 0; i < ord.Count; i++)
                    Console.WriteLine(ord[i]);
            }

            public static void Update()
            {//To update an order
                Order OrderToUpdate = new Order();
                Console.WriteLine("enter an order number");
                OrderToUpdate.OrderId = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a customer name\n");
                OrderToUpdate.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter a customer email address\n");
                OrderToUpdate.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter a customer address\n");
                OrderToUpdate.CustomerAdress = Console.ReadLine();
                order.UpdateOrder(OrderToUpdate);
            }

            public static void Delete()
            {//To delete an order
                Console.WriteLine("enter a order number\n");
                int IdOrder = int.Parse(Console.ReadLine());
                order.DeleteOrder(IdOrder);
            }
        }

        public static void OptionsProduct()  //Secondary menu - product
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: Adding a product\r\n" +
                "b: Single product display\r\n" +
                "c: Display of all products\r\n" +
                "d: Product update\r\n" +
                "e: Deleting a product\n" +
                                "f: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                switch (action)
                {
                    case "a"://To add a product to the list
                        ProductFunctions.Add();
                        break;
                    case "b"://To print a product by product ID number
                        ProductFunctions.DisplayById();
                        break;
                    case "c"://To print all products
                        ProductFunctions.DisplayAll();
                        break;
                    case "d"://for product update
                        ProductFunctions.Update();
                        break;
                    case "e"://To delete a product
                        ProductFunctions.Delete();
                        break;
                    case "f"://to finish
                        return;
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadLine();
            }

        }
        public static class ProductFunctions   //functions - product
        {
            public static void Add()
            {//To add a product to the list
                Product NewProduct = new Product();
                Console.WriteLine("enter a product name\n");
                NewProduct.ProductName =  Console.ReadLine();
                Console.WriteLine("Enter the amount of products in stock\n");
                NewProduct.AmmountInStock = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a product price\n");
                NewProduct.ProductPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a product category\n");
                NewProduct.ProductCategory = (Category)int.Parse(Console.ReadLine());
                product.AddProduct(NewProduct);
            }
            public static void DisplayById()
            {//To print a product by product ID number
                Console.WriteLine("enter a product number\n");
                int IdProduct = int.Parse(Console.ReadLine());
                Console.WriteLine(product.GetProductsId(IdProduct));
            }
            public static void DisplayAll()
            {//To print all products
                List<Product> prd = product.GetProducts();
                for (int i = 0; i < prd.Count; i++)
                    Console.WriteLine(prd[i]);
            }
            public static void Update()
            {//for product update
                Product NewProduct = new Product();
                Console.WriteLine("enter a product name\n");
                NewProduct.ProductName = Console.ReadLine();
                Console.WriteLine("enter a product number\n");
                NewProduct.ProductId = int.Parse (Console.ReadLine());
                Console.WriteLine("Enter the amount of products in stock\n");
                NewProduct.AmmountInStock = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a product price\n");
                NewProduct.ProductPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a product category\n");
                NewProduct.ProductCategory = (Category)int.Parse(Console.ReadLine());
                product.UpdateProduct(NewProduct);
            }
            public static void Delete()
            {//To delete a product
                Console.WriteLine("enter a product number\n");
                int IdProduct = int.Parse(Console.ReadLine());
                product.DeleteProduct(IdProduct);
            }
        }
      
    }
}


