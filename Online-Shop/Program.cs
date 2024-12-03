using System;
using System.Collections.Generic;

namespace OnlineShop
{
    class Program
    {
        private static List<User> users;
        private static List<Product> products;
        private static List<Category> categories;

        static void Main(string[] args)
        {
            InitializeData();
            Run();
        }

        private static void InitializeData()
        {
            users = new List<User>
            {
                new Admin(1, "admin", "password", "admin@example.com", "1234567890", "Admin Street", "Admin City", DateTime.Now),
                new Customer(2, "customer", "password", "customer@example.com", "0987654321", "Customer Street", "Customer City", "active", "Customer")
            };

            categories = new List<Category>
            {
                new Category(1, "Electronics"),
                new Category(2, "Clothing"),
                new Category(3, "Books"),
                new Category(4, "Furniture"),
                new Category(5, "Toys")
            };

            products = new List<Product>
            {
                new Product(1, "Laptop", 999.99m, 10, categories[0]),
                new Product(2, "T-shirt", 19.99m, 50, categories[1]),
                new Product(3, "Novel", 14.99m, 20, categories[2]),
                new Product(4, "Chair", 49.99m, 15, categories[3]),
                new Product(5, "Action Figure", 29.99m, 30, categories[4])
            };
        }

        private static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Online Shop");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Customer");
                Console.WriteLine("3. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    AdminMenu();
                }
                else if (choice == "2")
                {
                    CustomerMenu();
                }
                else if (choice == "3")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                }
            }
        }

        private static void AdminMenu()
        {
            Console.Write("Enter Admin ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            Admin admin = users.Find(u => u is Admin && u.ID == id && u.Password == password) as Admin;

            if (admin == null)
            {
                Console.WriteLine("Invalid credentials. Press Enter to return to the main menu.");
                Console.ReadLine();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Admin Menu");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add Product");
                Console.WriteLine("3. Update Product");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Save Products to CSV");
                Console.WriteLine("6. Logout");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    ViewProducts();
                }
                else if (choice == "2")
                {
                    AddProduct();
                }
                else if (choice == "3")
                {
                    UpdateProduct();
                }
                else if (choice == "4")
                {
                    RemoveProduct();
                }
                else if (choice == "5")
                {
                    SaveProductsToCsv();
                }
                else if (choice == "6")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                }
            }
        }

        private static void CustomerMenu()
        {
            Console.Clear();
            Console.WriteLine("Customer Menu");
            // Add functionality for the Customer menu here
            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine();
        }

        private static void ViewProducts()
        {
            Console.Clear();
            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("Press Enter to return.");
            Console.ReadLine();
        }

        private static void AddProduct()
        {
            Console.Clear();
            Console.WriteLine("Add Product");
            Console.Write("Enter Product Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Stock Quantity: ");
            int stock = int.Parse(Console.ReadLine());
            Console.WriteLine("Select Category:");
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            int categoryIndex = int.Parse(Console.ReadLine()) - 1;

            products.Add(new Product(products.Count + 1, name, price, stock, categories[categoryIndex]));
            Console.WriteLine("Product added successfully. Press Enter to return.");
            Console.ReadLine();
        }

        private static void UpdateProduct()
        {
            // Implementation for updating a product
        }

        private static void RemoveProduct()
        {
            // Implementation for removing a product
        }

        private static void SaveProductsToCsv()
        {
            // Implementation for saving products to a CSV file
        }
    }
}
