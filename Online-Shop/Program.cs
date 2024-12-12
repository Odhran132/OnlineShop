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
            products.Add(new Product(5, "Blender", "NutriBullet Pro", 2, 89.99, 15)); // Category: Household
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
                        ManageCustomers();
                        break;
                    case "3":
                        ManageProductCategories();
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
                        Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Description: {product.Description}, Price: {product.Price:C}, Stock: {product.StockQuantity}");
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
                        Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Description: {product.Description}, Price: {product.Price:C}, Stock: {product.StockQuantity}");
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
                        Console.WriteLine($"You are editing: {productToUpdate.Name}");

                        // Allow the user to select what to update
                        Console.WriteLine("What would you like to update?");
                        Console.WriteLine("1. Name");
                        Console.WriteLine("2. Description");
                        Console.WriteLine("3. Price");
                        Console.WriteLine("4. Stock Quantity");
                        Console.WriteLine("5. Cancel");

                        string updateChoice = Console.ReadLine();

                        switch (updateChoice)
                        {
                            case "1":
                                Console.Write("Enter new Name: ");
                                productToUpdate.Name = Console.ReadLine();
                                Console.WriteLine("Product Name updated.");
                                break;

                            case "2":

                                Console.Write("Enter new Description: ");
                                productToUpdate.Name = Console.ReadLine();
                                Console.WriteLine("Product Description updated.");
                                break;

                            case "3":
                                Console.Write("Enter new price: ");
                                if (double.TryParse(Console.ReadLine(), out double newPrice))
                                {
                                    productToUpdate.Price = newPrice;
                                    Console.WriteLine("Product Price updated.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid Price input.");
                                }
                                break;

                            case "4":
                                Console.Write("Enter new Stock quantity: ");
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

                            case "5":
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
                Console.WriteLine("ID | Name | Description | Price | Stock Quantity | Total Value");
                Console.WriteLine("--------------------------------------------------");

                decimal totalValue = 0; 

                foreach (var product in products)
                {
                    // Calculate the total value for this product (Price * StockQuantity)
                    decimal productTotalValue = (decimal)product.Price * product.StockQuantity;  // Cast product.Price to decimal
                    totalValue += productTotalValue; 

                    // Display product details along with the total value for this product
                    Console.WriteLine($"{product.ProductID} |{product.Name} | {product.Description} | {product.Price:C} | {product.StockQuantity} | {productTotalValue:C}");
                }

                // Display the total value of all products
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine($"Total Value of All Products: {totalValue:C}");
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



        static void ManageCustomers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Customer Management");

                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Edit Customer");
                Console.WriteLine("3. Remove Customer");
                Console.WriteLine("4. View All Customers");
                Console.WriteLine("Press 'F' to return to the previous menu.");

                string choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        EditCustomer();
                        break;
                    case "3":
                        RemoveCustomer();
                        break;
                    case "4":
                        ViewAllCustomers();
                        break;
                    case "F":
                        return; // Return to the previous menu
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }


        static void AddCustomer()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add Customer");

                // Ask for the details of the new customer
                Console.Write("Enter Customer Username (or press 'F' to return): ");
                string userName = Console.ReadLine();
                if (userName.ToUpper() == "F") return;

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

                Console.Write("Enter Status (Active/Inactive): ");
                string status = Console.ReadLine();

                Console.Write("Enter Role (Admin/Customer): ");
                string role = Console.ReadLine();

                // Generate a new Customer ID
                int customerId = customers.Count + 1;

                // Create the new customer
                Customer newCustomer = new Customer(customerId, userName, password, email, phoneNumber, addressStreet, addressCity, status, role);

                // Add the customer to the list
                customers.Add(newCustomer);

                Console.WriteLine("Customer added successfully!");

                // Wait for user to continue
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
        }


        static void EditCustomer()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Edit Customer");

                // Display all customers
                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                }
                else
                {
                    Console.WriteLine("ID | Username | Email | Phone");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.ID} | {customer.UserName} | {customer.Email} | {customer.PhoneNumber}");
                    }
                }

                // Ask the user for the Customer ID to edit
                Console.Write("Enter the Customer ID to edit (or press 'F' to return): ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "F") return;

                if (int.TryParse(input, out int customerId))
                {
                    var customer = customers.FirstOrDefault(c => c.ID == customerId);
                    if (customer != null)
                    {
                        // Allow the user to edit customer details
                        Console.Write("Enter new Username (or press Enter to keep the current): ");
                        string newUserName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newUserName)) customer.UserName = newUserName;

                        Console.Write("Enter new Email (or press Enter to keep the current): ");
                        string newEmail = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newEmail)) customer.Email = newEmail;

                        Console.Write("Enter new Phone Number (or press Enter to keep the current): ");
                        string newPhoneNumber = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newPhoneNumber)) customer.PhoneNumber = newPhoneNumber;

                        Console.Write("Enter new Street Address (or press Enter to keep the current): ");
                        string newStreet = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newStreet)) customer.AddressStreet = newStreet;

                        Console.Write("Enter new City (or press Enter to keep the current): ");
                        string newCity = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newCity)) customer.AddressCity = newCity;

                        Console.Write("Enter new Status (Active/Inactive): ");
                        string newStatus = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newStatus)) customer.Status = newStatus;

                        Console.Write("Enter new Role (Admin/Customer): ");
                        string newRole = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newRole)) customer.Role = newRole;

                        Console.WriteLine("Customer details updated successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Customer not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Customer ID.");
                }

                // Wait for user to continue
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
        }


        static void RemoveCustomer()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Remove Customer");

                // Display all customers
                if (customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                }
                else
                {
                    Console.WriteLine("ID | Username | Email | Phone");
                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"{customer.ID} | {customer.UserName} | {customer.Email} | {customer.PhoneNumber}");
                    }
                }

                // Ask the user for the Customer ID to remove
                Console.Write("Enter the Customer ID to remove (or press 'F' to return): ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "F") return;

                if (int.TryParse(input, out int customerId))
                {
                    var customerToRemove = customers.FirstOrDefault(c => c.ID == customerId);
                    if (customerToRemove != null)
                    {
                        customers.Remove(customerToRemove);
                        Console.WriteLine("Customer removed successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Customer not found.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Customer ID.");
                }

                // Wait for user to continue
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
        }


        static void ViewAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("View All Customers");

            if (customers.Count == 0)
            {
                Console.WriteLine("No customers found.");
            }
            else
            {
                Console.WriteLine("ID | Username | Email | Phone");
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.ID} | {customer.UserName} | {customer.Email} | {customer.PhoneNumber}");
                }
            }

            // Option to return to the previous menu
            Console.WriteLine("\nPress 'F' to return to the previous menu.");
            string input = Console.ReadLine();

            if (input.ToUpper() == "F")
            {
                return;
            }

            // Wait for user to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }






        static void ManageProductCategories()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Manage Product Categories");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. View All Categories");
                Console.WriteLine("2. Add New Category");
                Console.WriteLine("3. Update Category");
                Console.WriteLine("F. Return to Previous Menu");
                Console.Write("Choose an option: ");

                string input = Console.ReadLine().ToUpper();

                switch (input)
                {
                    case "1":
                        ViewAllCategories();
                        break;
                    case "2":
                        AddProductCategory();
                        break;
                    case "3":
                        UpdateProductCategory();
                        break;
                    case "F":
                        return; // Return to the previous menu
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
        

        static void AddProductCategory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Add New Product Category");
                Console.WriteLine("--------------------------");

                Console.Write("Enter the category name (or press 'F' to cancel): ");
                string categoryName = Console.ReadLine();

                if (string.Equals(categoryName, "F", StringComparison.OrdinalIgnoreCase))
                {
                    return; // Return to the previous menu
                }

                if (string.IsNullOrWhiteSpace(categoryName))
                {
                    Console.WriteLine("Category name cannot be empty. Please try again.");
                    continue;
                }

                // Generate a new Category ID (based on existing categories)
                int newCategoryId = categories.Count + 1;

                // Add the new category to the list
                categories.Add(new ProductCategory(newCategoryId, categoryName));
                Console.WriteLine($"Product category '{categoryName}' added successfully.");

                // Pause and ask if user wants to add another category
                Console.WriteLine("\nPress any key to add another category or 'F' to return to the previous menu.");
                string input = Console.ReadLine();

                if (string.Equals(input, "F", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
        }


        static void UpdateProductCategory()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Update Product Category");
                Console.WriteLine("-------------------------");

                if (categories.Count == 0)
                {
                    Console.WriteLine("No categories available.");
                }
                else
                {
                    foreach (var category in categories)
                    {
                        Console.WriteLine($"Category ID: {category.CategoryID}, Name: {category.CategoryName}");
                    }
                }

                Console.Write("Enter the Category ID to update (or press 'F' to cancel): ");
                string input = Console.ReadLine();

                if (string.Equals(input, "F", StringComparison.OrdinalIgnoreCase))
                {
                    return; // Return to the previous menu
                }

                if (int.TryParse(input, out int categoryId))
                {
                    var category = categories.FirstOrDefault(c => c.CategoryID == categoryId);
                    if (category != null)
                    {
                        Console.WriteLine($"Updating category: {category.CategoryName}");
                        Console.Write("Enter the new name for the category (or press 'F' to cancel): ");
                        string newCategoryName = Console.ReadLine();

                        if (string.Equals(newCategoryName, "F", StringComparison.OrdinalIgnoreCase))
                        {
                            return; // Return to the previous menu
                        }

                        if (string.IsNullOrWhiteSpace(newCategoryName))
                        {
                            Console.WriteLine("Category name cannot be empty. Please try again.");
                            continue;
                        }

                        // Update the category name
                        category.CategoryName = newCategoryName;
                        Console.WriteLine($"Category '{category.CategoryName}' updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Category not found. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid Category ID.");
                }

                Console.WriteLine("\nPress any key to continue or 'F' to return to the Manage Product Categories menu.");
                if (string.Equals(Console.ReadLine(), "F", StringComparison.OrdinalIgnoreCase))
                {
                    return;
                }
            }
        }


        static void ViewAllCategories()
        {
            Console.Clear();
            Console.WriteLine("View All Product Categories");
            Console.WriteLine("----------------------------");

            if (categories.Count == 0)
            {
                Console.WriteLine("No categories available.");
            }
            else
            {
                foreach (var category in categories)
                {
                    Console.WriteLine($"Category ID: {category.CategoryID}, Name: {category.CategoryName}");
                }
            }

            Console.WriteLine("\nPress 'F' to return to the Manage Product Categories menu.");
            if (string.Equals(Console.ReadLine(), "F", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
        }




        static void SaveProductsToCSV()
        {
            string fileName = "products.csv";
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);  // This saves the file in the current directory

            try
            {
                // Writing to the CSV file
                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("ProductID,Description,CategoryID,Price,StockQuantity");
                    foreach (var product in products)
                    {
                        writer.WriteLine($"{product.ProductID},{product.Description},{product.CategoryID},{product.Price},{product.StockQuantity}");
                    }
                }

                // Clear the console and display the confirmation message on a new screen
                Console.Clear();
                Console.WriteLine($"Products have been successfully saved to {filePath}");
                Console.WriteLine("\nPress 'F' to return to the previous menu.");

                // Wait for user input to return to the previous menu
                string input = Console.ReadLine();
                if (string.Equals(input, "F", StringComparison.OrdinalIgnoreCase))
                {
                    // You can call the method for the previous screen/menu here, for example:
                    ManageProducts();  // Assuming ManageProducts() is the previous menu method
                }
                else
                {
                    // Optionally, you can show an invalid input message or wait for correct input
                    Console.WriteLine("Invalid input. Press 'F' to return to the previous menu.");
                    Console.ReadKey();  // Pause before returning
                }
            }
            catch (Exception ex)
            {
                // Handle any potential errors during file write
                Console.Clear(); // Clear the console on error as well
                Console.WriteLine("An error occurred while saving the products: " + ex.Message);
                Console.WriteLine("\nPress 'F' to return to the previous menu.");
                Console.ReadKey();  // Wait for the user to press 'F' to go back
            }
        }



    }

}

