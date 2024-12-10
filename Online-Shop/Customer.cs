using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop
{
    public class Customer : User
    {
        public string Status { get; set; }
        public string Role { get; set; }

        public Customer(int id, string userName, string password, string email, string phoneNumber, string addressStreet, string addressCity, string status, string role)
            : base(id, userName, password, email, phoneNumber, addressStreet, addressCity)
        {
            Status = status;
            Role = role;
        }

        public void ViewOrderHistory(List<Order> orders)
        {
            var customerOrders = orders.Where(o => o.CustomerID == ID).ToList();

            if (!customerOrders.Any())
            {
                Console.WriteLine("No order history found.");
                return;
            }

            Console.WriteLine("Order History:");
            foreach (var order in customerOrders)
            {
                Console.WriteLine($"Order ID: {order.OrderID}");
                Console.WriteLine($"Date: {order.OrderDate}");
                Console.WriteLine("Products:");
                Console.WriteLine(order.GetProductDetails());
                Console.WriteLine($"Total: {order.CalculateTotal():C}");
                Console.WriteLine("-----------------------------------");
            }
        }
    }
}


