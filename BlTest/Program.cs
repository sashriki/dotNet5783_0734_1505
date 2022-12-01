using BlApi;
using BlImplementation;
using BO;
using DalApi;
using DO;

namespace BlTest
{
    internal class Program
    {
        private static IBl Obj = new Bl();
        private static Cart cart = new Cart
        {
            CustomerName = null,
            CustomerAdress = null,
            CustomerEmail = null,
            orderItems = new List<BO.orderItem>(),
            totalPrice = 0
        };
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine(
                "For products press 1\r\n" +
                "For orders press 2\r\n" +
                "For carts press 3\r\n" +
                "To exit press 0\n");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1"://to perform operations for a product
                        OptionsProduct();
                        break;
                    case "2"://to perform actions for an order
                        OptionsOrder();
                        break;
                    case "3"://perform actions for a shopping basket
                        OptionsCart();
                        break;
                    case "0"://to end the program
                        Console.WriteLine("bye bye");
                        return;
                    default://for incorrect input
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
            }
            return;
        }
        /// <summary>
        /// To perform actions for a product
        /// </summary>
        public static void OptionsProduct()
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: To display all products\r\n" +
                "b: To display a product by ID for the manager\r\n" +
                "c: To display a product by ID for a customer\r\n" +
                "d: To add a product\r\n" +
                "e: Deleting a product\r\n" +
                "f: To update a product\r\n" +
                                "g: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                try
                {
                    switch (action)
                    {
                        case "a":   //To display all products
                            ProductFunctions.getAllProducts();
                            break;
                        case "b":   //To display a product by ID for the manager
                            ProductFunctions.getByIdToMannage();
                            break;
                        case "c":   //To display a product by ID for a customer
                            ProductFunctions.getByIdToCostumer();
                            break;
                        case "d":   //To add a product
                            ProductFunctions.addProduct();
                            break;
                        case "e":   //Deleting a product
                            ProductFunctions.removeProduct();
                            break;
                        case "f":   //To update a product
                            ProductFunctions.updateProduct();
                            break;
                        case "g":   //To exit
                            return;
                        default:
                            Console.WriteLine("You pressed a wrong key, try again\n");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());   
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadLine();
            }
        }
        /// <summary>
        /// Realization of functions for a product
        /// </summary>
        public static class ProductFunctions  
        {
            public static void getAllProducts()
            {
                IEnumerable<BO.ProductForList> productForLists = Obj.Product.getAllProducts();
                foreach (var item in productForLists)
                    Console.WriteLine(item);
                return;
            }
            public static void getByIdToMannage()
            { }
            public static void getByIdToCostumer()
            { }
            public static void addProduct()
            { }
            public static void removeProduct()
            { }
            public static void updateProduct()
            { }
        }
        /// <summary>
        /// To perform actions for a order
        /// </summary>
        public static void OptionsOrder()
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: To display all orders for the manager\r\n" +
                "b: To display an order by ID\r\n" +
                "c: Update order shipping for the manager\r\n" +
                "d: Update order delivery for manager\r\n" +
                "e: To track an order\r\n" +
                "f: To update an order for a manager\r\n"+
                "g: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                try
                {
                    switch (action)
                    {
                        case "a":   // To display all orders for the manager
                            OrderFunctions.GetAllToManager();
                            break;
                        case "b":   //To display an order by ID
                            OrderFunctions.GetOrderByID();
                            break;
                        case "c":   //Update order shipping for the manager
                            OrderFunctions.ShippingUpdateToManager();
                            break;
                        case "d":   //Update order delivery for manager
                            OrderFunctions.supplyUpdateToManager();
                            break;
                        case "e":   //To track an order   
                            OrderFunctions.OrderTracking();
                            break;
                        case "f":   //To update an order for a manager
                            OrderFunctions.UpdateToManager();
                            break;
                        case "g":   //To exit
                            return;
                        default:
                            Console.WriteLine("You pressed a wrong key, try again\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadLine();
            }
        }
        /// <summary>
        /// Implementation of functions for an order
        /// </summary>
        public static class OrderFunctions
        {
            public static void GetAllToManager()
            {
                IEnumerable<BO.OrderForList> Orders = Obj.Order.GetAllToManager();
                foreach (var item in Orders)
                    Console.WriteLine(item);
                return;
            }
            public static void GetOrderByID()
            {
                BO.Order ordBO = new BO.Order();
                Console.WriteLine("Enter order ID\n");
                ordBO = Obj.Order.GetOrderByID(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordBO);
            }
            public static void ShippingUpdateToManager()
            {
                BO.Order ordBO = new BO.Order();
                Console.WriteLine("Enter order ID\n");
                ordBO = Obj.Order.ShippingUpdateToManager(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordBO);
            }
            public static void supplyUpdateToManager()
            {
                BO.Order ordBO = new BO.Order();
                Console.WriteLine("Enter order ID\n");
                ordBO = Obj.Order.supplyUpdateToManager(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordBO);
            }
            public static void OrderTracking()
            {
                BO.OrderTracking ordTraBO = new BO.OrderTracking();
                Console.WriteLine("Enter order ID\n");
                ordTraBO = Obj.Order.OrderTracking(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordTraBO);
            }
            public static void UpdateToManager()
            {
                int IdProduct,Amount;
                BO.Order newOrd=new BO.Order();
                Console.WriteLine("Enter order ID");
                newOrd = Obj.Order.GetOrderByID(int.Parse(Console.ReadLine()));
                Console.WriteLine("Enter product ID");
                IdProduct = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a quantity for a product\n");
                Amount= int.Parse(Console.ReadLine());
                Obj.Order.UpdateToManager(newOrd, IdProduct, Amount);
            }
        }
        /// <summary>
        /// To perform actions for a cart
        /// </summary>
        public static void OptionsCart()
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: To add a product to the shopping cart\r\n" +
                "b: To update the quantity of a product in the shopping basket\r\n" +
                "c: To make an order\r\n" +
                "f: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                try
                {
                    switch (action)
                    {
                        case "a":   //To add a product to the shopping cart
                            CartFunctions.AddProductToCart();
                            break;
                        case "b":   //To update the quantity of a product in the shopping basket
                            CartFunctions.UpdateAmount();
                            break;
                        case "c":   //To make an order
                            CartFunctions.OrderConfirmation();
                            break;
                        case "f":
                            return;
                        default:
                            Console.WriteLine("You pressed a wrong key, try again\n");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                Console.WriteLine("Enter your choice again:\n");
                action = Console.ReadLine();
            }
        }
        /// <summary>
        /// Implementation of functions for a shopping basket
        /// </summary>
        public static class CartFunctions
        {
            public static void AddProductToCart()
            {
                int IdOrdItem;
                Console.WriteLine("Enter a product ID number:\n");
                IdOrdItem = Convert.ToInt32(Console.ReadLine());
                cart = Obj.Cart.AddProductToCart(cart, IdOrdItem);
            }
            public static void UpdateAmount()
            {
                int IdOrdItem, amount;
                Console.WriteLine("Enter a product ID number:\n");
                IdOrdItem = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a quantity for the requested product:\n");
                amount = Convert.ToInt32(Console.ReadLine());
                cart = Obj.Cart.UpdateAmount(cart, IdOrdItem, amount);
            }
            public static void OrderConfirmation()
            {
                Console.WriteLine("Enter your name:");
                cart.CustomerName = Console.ReadLine();
                Console.WriteLine("Enter an email address:");
                cart.CustomerEmail = Console.ReadLine();
                Console.WriteLine("Enter shipping address:");
                cart.CustomerAdress = Console.ReadLine();
                Console.WriteLine("Enter shipping address:");
                Obj.Cart.OrderConfirmation(cart);
            }
        }
    }
}