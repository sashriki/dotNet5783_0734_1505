using BlApi;
using BlImplementation;
using BO;
using System;
using DalApi;
using DO;


namespace BlTest
{
    internal class Program
    {
        private static BlApi.IBl? bl = BlApi.Factory.Get();

        private static Cart cart = new Cart
        {
            CustomerName = null,
            CustomerAdress = null,
            CustomerEmail = null,
            OrderItems = new List<BO.OrderItem>(),
            TotalPrice = 0
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
                    Console.WriteLine(ex);   
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
            {
                BO.Product productBO = new BO.Product();
                Console.WriteLine("Enter product ID\n");
                productBO = bl.Product.getByIdToMannage(int.Parse(Console.ReadLine()));
                Console.WriteLine(productBO);
                return;
            }
            public static void getByIdToCostumer()
            {
                BO.ProductItem productBO = new BO.ProductItem();
                Console.WriteLine("Enter product ID\n");
                productBO= bl.Product.getByIdToCostumer(int.Parse(Console.ReadLine()),cart);
                Console.WriteLine(productBO);
                return;
            }
            public static void addProduct()
            {
                BO.Product NewProduct = new BO.Product();
                Console.WriteLine("enter a product name");
                NewProduct.ProductName = Console.ReadLine();
                Console.WriteLine("enter a product number");
                NewProduct.ProductId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the amount of products in stock");
                NewProduct.AmmountInStock = int.Parse(Console.ReadLine());
                Console.WriteLine("enter a product Price");
                NewProduct.ProductPrice = int.Parse(Console.ReadLine());
                Console.WriteLine("choose a product category:\n" +
                    "0: Clothing\r\n" +
                    "1: Shoes\r\n" +
                    "2: home workout\r\n" +
                    "3: gym equipment\r\n" +
                    "4: Accessories\r\n");
                NewProduct.ProductCategory = (BO.Category)int.Parse(Console.ReadLine());
                try
                {
                    bl.Product.addProduct(NewProduct);
                    Console.WriteLine("The operation was performed successfully!\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);  //warning!
                }
            }
            public static void removeProduct()
            {
                Console.WriteLine("Enter product ID");
                int ID = int.Parse(Console.ReadLine());
                try
                {
                    bl.Product.removeProduct(ID);
                    Console.WriteLine("The operation was performed successfully!\n");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());   
                }
            }
            public static void updateProduct()
            {
                BO.Product NewProduct = new BO.Product();
                Console.WriteLine("Enter a product name");
                NewProduct.ProductName = Console.ReadLine();
                Console.WriteLine("Enter a product number");
                NewProduct.ProductId = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the amount of products in stock");
                NewProduct.AmmountInStock = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a product Price");
                NewProduct.ProductPrice = double.Parse(Console.ReadLine());
                //Console.WriteLine("Enter a product category");
                Console.WriteLine("choose a product category:\n" +
                    "0: Clothing\r\n" +
                    "1: Shoes\r\n" +
                    "2: home workout\r\n" +
                    "3: gym equipment\r\n" +
                    "4: Accessories");
                NewProduct.ProductCategory = (BO.Category)int.Parse(Console.ReadLine());
                try
                {
                    bl.Product.updateProduct(NewProduct);
                    Console.WriteLine("The operation was performed successfully!\n");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);  //warning!
                }
            }
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
                            OrderFunctions.GetAllToManager();//V
                            break;
                        case "b":   //To display an order by ID
                            OrderFunctions.GetOrderByID();//v
                            break;
                        case "c":   //Update order shipping for the manager
                            OrderFunctions.ShippingUpdateToManager();//v
                            break;
                        case "d":   //Update order delivery for manager
                            OrderFunctions.supplyUpdateToManager();
                            break;
                        case "e":   //To track an order   
                            OrderFunctions.OrderTracking();//v
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
                Console.WriteLine("Enter order ID");
                ordBO = bl.Order.GetOrderByID(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordBO);
            }
            public static void ShippingUpdateToManager()
            {
                BO.Order ordBO = new BO.Order();
                Console.WriteLine("Enter order ID\n");
                ordBO = bl.Order.ShippingUpdateToManager(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordBO);
            }
            public static void supplyUpdateToManager()
            {
                BO.Order ordBO = new BO.Order();
                Console.WriteLine("Enter order ID\n");
                ordBO = bl.Order.supplyUpdateToManager(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordBO);
            }
            public static void OrderTracking()
            {
                BO.OrderTracking ordTraBO = new BO.OrderTracking();
                Console.WriteLine("Enter order ID\n");
                ordTraBO = bl.Order.OrderTracking(int.Parse(Console.ReadLine()));
                Console.WriteLine(ordTraBO);
            }
            public static void UpdateToManager()
            {
                int IdProduct,Amount;
                BO.Order newOrd=new BO.Order();
                Console.WriteLine("Enter order ID");
                newOrd = bl.Order.GetOrderByID(int.Parse(Console.ReadLine()));
                Console.WriteLine("Enter product ID");
                IdProduct = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter a quantity for a product\n");
                Amount= int.Parse(Console.ReadLine());
                bl.Order.UpdateToManager(newOrd, IdProduct, Amount);
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
                "d: To print the entire shopping cart\r\n" +
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
                        case "d":
                            Console.WriteLine(cart);
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
                cart = bl.Cart.AddProductToCart(cart, IdOrdItem);
                Console.WriteLine("The operation was performed successfully!\n");
            }
            public static void UpdateAmount()
            {
                int IdOrdItem, amount;
                Console.WriteLine("Enter a product ID number:\n");
                IdOrdItem = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter a quantity for the requested product:\n");
                amount = Convert.ToInt32(Console.ReadLine());
                cart = bl.Cart.UpdateAmount(cart, IdOrdItem, amount);
                Console.WriteLine("The operation was performed successfully!\n");
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
                bl.Cart.OrderConfirmation(cart);
                Console.WriteLine("The operation was performed successfully!\n");
            }
        }
    }
}