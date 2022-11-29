using BlApi;
using Dal;
using DalApi;
using BlImplementation;
using BO;
using System.Security.Cryptography;

namespace BlTest
{
    internal class Program
    {
        private static IBl Obj = new Bl();
        public static void Main()
        {
            while (true)
            {
                Console.WriteLine(
                "For products press 1\r\n" +
                "For orders press 2\r\n" +
                "For carts press 3\r\n" +
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
                        OptionsCart();
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
        } 
        public static void OptionsProduct()  
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: To display all products\r\n" +
                "b: To display a product by ID for the manager\r\n" +
                "c: To display a product by ID for a customer\r\n" +
                "d: To add a product\r\n" +
                "e: Deleting a product\r\n" +
                "f: To update a product\r\n"+
                                "g: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                switch (action)
                {
                    case "a":
                        ProductFunctions.getAllProducts();
                        break;
                    case "b":
                        ProductFunctions.getByIdToMannage();
                        break;
                    case "c":
                        ProductFunctions.getByIdToCostumer();
                        break;
                    case "d":
                        ProductFunctions.addProduct();
                        break;
                    case "e":
                        ProductFunctions.removeProduct();
                        break;
                    case "f":
                        ProductFunctions.updateProduct();
                        break;
                    case "g":
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
        public static void OptionsOrder()
        {
            string action;
            Console.WriteLine("Select the desired option:\r\n" +
                "a: To display all orders for the manager\r\n" +
                "b: To display an order by ID\r\n" +
                "c: Update order shipping for the manager\r\n" +
                "d: Update order delivery for manager\r\n" +
                "e: To track an order\n" +
                "f: exit\n");
            action = Console.ReadLine();
            while (true)
            {
                switch (action)
                {
                    case "a":
                        OrderFunctions.GetAllToManager();
                        break;
                    case "b":
                        OrderFunctions.GetOrderByID();
                        break;
                    case "c":
                        OrderFunctions.ShippingUpdateToManager();
                        break;
                    case "d":
                        OrderFunctions.supplyUpdateToManager();
                        break;
                    case "e":
                        OrderFunctions.OrderTracking();
                        break;
                    case "f":
                        return;
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadLine();
            }
        }
        public static class OrderFunctions   
        {
            public static void GetAllToManager() 
            { }
            public static void GetOrderByID() 
            { }
            public static void ShippingUpdateToManager()
            { }
            public static void supplyUpdateToManager()
            { }
            public static void OrderTracking() 
            { }
        }
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
                switch (action)
                {
                    case "a":
                        CartFunctions.AddProductToCart();
                        break;
                    case "b":
                        CartFunctions.UpdateAmount();
                        break;
                    case "c":
                        CartFunctions.OrderConfirmation();
                        break;
                    case "f":
                        return;
                    default:
                        Console.WriteLine("You pressed a wrong key, try again\n");
                        break;
                }
                Console.WriteLine("enter your choice again\n");
                action = Console.ReadLine();
            }
        }
        public static class CartFunctions  
        {
            public static void AddProductToCart()
            { }
            public static void UpdateAmount()
            { }
            public static void OrderConfirmation()
            { }
        }
    }
}