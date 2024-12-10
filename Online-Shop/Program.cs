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
            categories.Add(new ProductCategory(2, "Household"));
            categories.Add(new ProductCategory(3, "Clothing"));
            categories.Add(new ProductCategory(4, "Books"));
            categories.Add(new ProductCategory(5, "Toys & Games"));

            // Products
            products.Add(new Product(1, "LED TV", "65 inch Samsung LED TV", 1, 459.99, 5)); // Category: Electronics
            products.Add(new Product(2, "Vaccum Cleaner", "Dyson V11 Vacuum Cleaner ", 2, 599.99, 10)); // Category: Household
            products.Add(new Product(3, "Running Shoes","Nike Air Zoom Pegasus", 3, 120.00, 20)); // Category: Clothing
            products.Add(new Product(4, "Software Programming", "C# for beginners", 4, 12.99, 25)); // Category: Books
            products.Add(new Product(5, "Bledner", "NutriBullet Pro", 2, 89.99, 15)); // Category: Household
            products.Add(new Product(6, "Smartphone", "iPhone 13", 5, 999.99, 8)); // Category: Toys & Games

            // Shopping baskets
            foreach (var customer in customers)
            {
                baskets[customer.ID] = new ShoppingBasket();
            }
        }
        static void RegisterCustomer()
        {
            Console.Clear();
            Console.WriteLine("Register New Customer:");

            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            if (customers.Any(c => c.UserName == username))
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter Street Address: ");
            string addressStreet = Console.ReadLine();
            Console.Write("Enter City: ");
            string addressCity = Console.ReadLine();

            int newId = customers.Any() ? customers.Max(c => c.ID) + 1 : 1;
            customers.Add(new Customer(newId, username, password, email, phoneNumber, addressStreet, addressCity, "active", "Customer"));

            Console.WriteLine("Customer registered successfully! Press any key to continue.");
            Console.ReadKey();
        }



        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Online Shop");
                Console.WriteLine("1. Admin Menu");
                Console.WriteLine("2. Customer Menu");
                Console.WriteLine("3. Register New User");
                Console.WriteLine("4. Exit");

                switch (Console.ReadLine())
                {
                    case "1":
                        AdminMenu();
                        break;
                    case "2":
                        CustomerMenu();
                        break;
                    case "3":
                        RegisterCustomer();
                        break;
                    case "4":
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
                Console.WriteLine("Invalid credentials. Press any key to return to the main menu.");
                Console.ReadKey();
                return;
            }

            admin.LastLogin = DateTime.Now;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {admin.UserName}! Last Login: {admin.LastLogin}");
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Manage Products");
                Console.WriteLine("2. Manage Customers");
                Console.WriteLine("3. Manage Product Categories");
                Console.WriteLine("4. Save Products to CSV");
                Console.WriteLine("5. Logout");

                switch (Console.ReadLine())
                {
                    case "1":
                        ManageProducts();
                        break;
                    case "2":
                        //ManageCustomers();
                        break;
                    case "3":
                        //ManageProductCategories();
                        break;
                    case "4":
                        SaveProductsToCSV();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
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

            // Initialize the basket outside the loop
            ShoppingBasket basket = baskets.ContainsKey(customer.ID) ? baskets[customer.ID] : new ShoppingBasket();

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
                        ViewAllProductsCustomer(products, basket);
                        break;

                    case "2":
                        basket.ViewBasket();
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;

                    case "3":
                        if (basket.Products.Count == 0)
                        {
                            Console.WriteLine("Your basket is empty. Add products to your basket before placing an order.");
                            Console.ReadKey();
                        }
                        else
                        {
                            var order = new Order(customer.ID, DateTime.Now, new List<Product>(basket.Products));
                            orders.Add(order);
                            basket.ClearBasket();
                            Console.WriteLine($"Order placed successfully! Order ID: {order.OrderID}, Total: {order.CalculateTotal():C}");
                            Console.WriteLine("Press any key to return to the menu...");
                            Console.ReadKey();
                        }
                        break;

                    case "4":
                        customer.ViewOrderHistory(orders);
                        Console.WriteLine("Press any key to return to the menu...");
                        Console.ReadKey();
                        break;

                    case "5":
                        // Save the basket back to the dictionary
                        if (baskets.ContainsKey(customer.ID))
                        {
                            baskets[customer.ID] = basket;
                        }
                        else
                        {
                            baskets.Add(customer.ID, basket);
                        }
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /*
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
        */

        static void ManageProducts()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Product Management:");
                Console.WriteLine("1. Add New Product");
                Console.WriteLine("2. Remove Product");
                Console.WriteLine("3. Update Product Information");
                Console.WriteLine("4. View All Products");
                Console.WriteLine("5. Return to Admin Menu");

                switch (Console.ReadLine())
                {
                    case "1": // Add product
                        AddProduct();
                        break;
                    case "2": // Remove product
                        RemoveProduct();
                        break;
                    case "3": // Update product
                        UpdateProductInfo();
                        break;
                    case "4": // View products
                        ViewAllProducts();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddProduct()
        {
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
                return;
            }

            Console.Write("Enter Price: ");
            double price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter Stock Quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            int newId = products.Any() ? products.Max(p => p.ProductID) + 1 : 1;
            products.Add(new Product(newId, name, categoryId, price, quantity));
            Console.WriteLine("Product added successfully.");
        }

        static void RemoveProduct()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Product Management - Remove Product");

                // Display all products
                Console.WriteLine("Available Products:");
                if (products.Count == 0)
                {
                    Console.WriteLine("No products available in the system.");
                }
                else
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine($"ID: {product.ProductID}, Name: {product.Description}, Price: {product.Price:C}, Stock: {product.StockQuantity}");
                    }
                }

                Console.WriteLine("\nEnter the Product ID to remove or press 'F' to return to the Manage Products menu.");
                Console.Write("Input: ");

                string input = Console.ReadLine();

                // Check if the user pressed 'F'
                if (string.Equals(input, "F", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    ManageProducts(); // Redirect to Manage Products menu
                    return;
                }

                // Validate if the input is a numeric Product ID
                if (int.TryParse(input, out int productId))
                {
                    var productToRemove = products.FirstOrDefault(p => p.ProductID == productId);
                    if (productToRemove != null)
                    {
                        products.Remove(productToRemove);
                        Console.WriteLine($"Product '{productToRemove.Description}' removed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Product not found. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Product ID or press 'F' to return.");
                }

                // Pause before looping back
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void UpdateProductInfo()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Product Management - Update Product Information");

                // Display all products
                Console.WriteLine("Available Products:");
                if (products.Count == 0)
                {
                    Console.WriteLine("No products available in the system.");
                }
                else
                {
                    foreach (var product in products)
                    {
                        Console.WriteLine($"ID: {product.ProductID}, Name: {product.Description}, Price: {product.Price:C}, Stock: {product.StockQuantity}");
                    }
                }

                Console.WriteLine("\nEnter the Product ID to update or press 'F' to return to the Manage Products menu.");
                Console.Write("Input: ");

                string input = Console.ReadLine();

                // Check if the user pressed 'F'
                if (string.Equals(input, "F", StringComparison.OrdinalIgnoreCase))
                {
                    ManageProducts(); // Redirect to Manage Products menu
                    return;
                }

                // Validate if the input is a numeric Product ID
                if (int.TryParse(input, out int productId))
                {
                    var productToUpdate = products.FirstOrDefault(p => p.ProductID == productId);
                    if (productToUpdate != null)
                    {
                        Console.WriteLine($"You are editing: {productToUpdate.Description}");

                        // Allow the user to select what to update
                        Console.WriteLine("What would you like to update?");
                        Console.WriteLine("1. Description");
                        Console.WriteLine("2. Price");
                        Console.WriteLine("3. Stock Quantity");
                        Console.WriteLine("4. Cancel");

                        string updateChoice = Console.ReadLine();

                        switch (updateChoice)
                        {
                            case "1":
                                Console.Write("Enter new description: ");
                                productToUpdate.Description = Console.ReadLine();
                                Console.WriteLine("Product description updated.");
                                break;

                            case "2":
                                Console.Write("Enter new price: ");
                                if (double.TryParse(Console.ReadLine(), out double newPrice))
                                {
                                    productToUpdate.Price = newPrice;
                                    Console.WriteLine("Product price updated.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid price input.");
                                }
                                break;

                            case "3":
                                Console.Write("Enter new stock quantity: ");
                                if (int.TryParse(Console.ReadLine(), out int newStock))
                                {
                                    productToUpdate.StockQuantity = newStock;
                                    Console.WriteLine("Product stock updated.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid stock quantity input.");
                                }
                                break;

                            case "4":
                                Console.WriteLine("Product update cancelled.");
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Returning to update menu.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Product not found. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Product ID or press 'F' to return.");
                }

                // Pause before looping back
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("Product Management - View All Products");

            // Check if there are any products in the system
            if (products.Count == 0)
            {
                Console.WriteLine("No products available in the system.");
            }
            else
            {
                // Display the products
                Console.WriteLine("ID | Name | Price | Stock Quantity");
                Console.WriteLine("-------------------------------------");

                foreach (var product in products)
                {
                    // Display product details
                    Console.WriteLine($"{product.ProductID} | {product.Description} | {product.Price:C} | {product.StockQuantity}");
                }
            }

            // Prompt to return to the Manage Products menu
            Console.WriteLine("\nPress 'F' to return to the Manage Products menu.");
            string input = Console.ReadLine();

            if (string.Equals(input, "F", StringComparison.OrdinalIgnoreCase))
            {
                ManageProducts(); // Redirect to Manage Products menu
            }
            else
            {
                Console.WriteLine("Invalid input. Press 'F' to return to the Manage Products menu.");
            }

            // Pause to let user read the output
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static void ViewAllProductsCustomer(List<Product> products, ShoppingBasket basket)
        {
            Console.Clear();
            Console.WriteLine("Customer product view - View All Products");

            if (products.Count == 0)
            {
                Console.WriteLine("No products available in the system.");
            }
            else
            {
                Console.WriteLine("ID | Name | Price | Stock Quantity");
                Console.WriteLine("-------------------------------------");

                foreach (var product in products)
                {
                    Console.WriteLine($"{product.ProductID} | {product.Description} | {product.Price:C} | {product.StockQuantity}");
                }

                Console.WriteLine("\nEnter Product ID to add to the basket, or 'F' to exit:");
                string input = Console.ReadLine();

                if (input.ToUpper() == "F")
                {
                    return;
                }

                if (int.TryParse(input, out int productId))
                {
                    var productToAdd = products.FirstOrDefault(p => p.ProductID == productId);

                    if (productToAdd != null && productToAdd.StockQuantity > 0)
                    {
                        basket.AddProduct(productToAdd);
                        productToAdd.StockQuantity--; // Decrement stock quantity
                        //Console.WriteLine($"{productToAdd.Description} has been added to your basket.");
                    }
                    else
                    {
                        Console.WriteLine("Product not available or out of stock.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Product ID.");
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }



        static void SaveProductsToCSV()
        {
            string filePath = "products.csv";

            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("ProductID,Description,CategoryID,Price,StockQuantity");
                foreach (var product in products)
                {
                    writer.WriteLine($"{product.ProductID},{product.Description},{product.CategoryID},{product.Price},{product.StockQuantity}");
                }
            }

            Console.WriteLine($"Products saved to {filePath}.");
        }


    }

}

