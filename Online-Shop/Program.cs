using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop
{
    class Program
    {
        static List<Admin> admins = new List<Admin>();
        static List<Customer> customers = new List<Customer>();
        static List<Product> products = new List<Product>();
        static List<Order> orders = new List<Order>();
        static Dictionary<int, ShoppingBasket> baskets = new Dictionary<int, ShoppingBasket>();
        static List<ProductCategory> categories = new List<ProductCategory>();


        static void Main(string[] args)
        {
            SeedData();
            MainMenu();
        }

        static void SeedData()
        {
            // Admin and Customer initialization
            admins.Add(new Admin(1, "admin1", "password", "admin1@example.com", "1234567890", "Street 1", "City A", DateTime.Now));
            customers.Add(new Customer(1, "cust1", "password", "cust1@example.com", "1234567890", "Street 2", "City B", "active", "Customer"));

            // Product Categories
            categories.Add(new ProductCategory(1, "Electronics"));
            categories.Add(new ProductCategory(2, "Clothing"));
            categories.Add(new ProductCategory(3, "Home Appliances"));

            // Products
            products.Add(new Product(1, "Laptop", 1, 800.00, 10)); // Category: Electronics
            products.Add(new Product(2, "Smartphone", 1, 500.00, 15)); // Category: Electronics
            products.Add(new Product(3, "T-Shirt", 2, 20.00, 50)); // Category: Clothing

            // Shopping baskets
            foreach (var customer in customers)
            {
                baskets[customer.ID] = new ShoppingBasket();
            }
        }


        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Online Shop");
                Console.WriteLine("1. Admin Menu");
                Console.WriteLine("2. Customer Menu");
                Console.WriteLine("3. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        AdminMenu();
                        break;
                    case "2":
                        CustomerMenu();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        static void AdminMenu()
        {
            Console.Write("Enter Admin Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var admin = admins.FirstOrDefault(a => a.UserName == username && a.Password == password);

            if (admin == null)
            {
                Console.WriteLine("Invalid credentials.");
                return;
            }

            admin.LastLogin = DateTime.Now;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View Products");
                Console.WriteLine("3. Logout");

                switch (Console.ReadLine())
                {
                    case "1": // Add Product
                        Console.Write("Enter Product Name: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Available Categories:");
                        foreach (var category in categories)
                        {
                            Console.WriteLine($"ID: {category.CategoryID}, Name: {category.CategoryName}");
                        }

                        Console.Write("Enter Category ID: ");
                        int categoryId = Convert.ToInt32(Console.ReadLine());
                        if (!categories.Any(c => c.CategoryID == categoryId))
                        {
                            Console.WriteLine("Invalid Category ID. Product not added.");
                            break;
                        }

                        Console.Write("Enter Price: ");
                        double price = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter Stock Quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());

                        int newId = products.Any() ? products.Max(p => p.ProductID) + 1 : 1;
                        products.Add(new Product(newId, name, categoryId, price, quantity));
                        Console.WriteLine("Product added successfully.");
                        break;


                    case "2":
                        BrowseProducts();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void CustomerMenu()
        {
            Console.Write("Enter Customer Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            var customer = customers.FirstOrDefault(c => c.UserName == username && c.Password == password);

            if (customer == null)
            {
                Console.WriteLine("Invalid credentials.");
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Customer Menu");
                Console.WriteLine("1. Browse Products");
                Console.WriteLine("2. View Basket");
                Console.WriteLine("3. Place Order");
                Console.WriteLine("4. View Order History");
                Console.WriteLine("5. Logout");

                switch (Console.ReadLine())
                {
                    case "1":
                        BrowseProducts();
                        Console.Write("Enter Product ID to add to basket: ");
                        int productId = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());

                        var product = products.FirstOrDefault(p => p.ProductID == productId);
                        if (product == null)
                        {
                            Console.WriteLine("Product not found.");
                        }
                        else
                        {
                            baskets[customer.ID].AddProduct(new Product(product.ProductID, product.Description, product.CategoryID, product.Price, quantity));
                        }
                        break;

                    case "2":
                        baskets[customer.ID].ViewBasket();
                        break;

                    case "3":
                        var order = new Order(customer.ID, DateTime.Now, new List<Product>(baskets[customer.ID].Products));
                        orders.Add(order);
                        baskets[customer.ID].ClearBasket();
                        Console.WriteLine($"Order placed successfully! Order ID: {order.OrderID}, Total: {order.CalculateTotal():C}");
                        break;

                    case "4":
                        customer.ViewOrderHistory(orders);
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void BrowseProducts()
        {
            Console.Clear();
            Console.WriteLine("Available Products:");
            foreach (var product in products)
            {
                var category = categories.FirstOrDefault(c => c.CategoryID == product.CategoryID)?.CategoryName ?? "Unknown";
                Console.WriteLine($"ID: {product.ProductID}, Name: {product.Description}, Category: {category}, Price: {product.Price:C}, Stock: {product.StockQuantity}");
            }
        }

    }
}

