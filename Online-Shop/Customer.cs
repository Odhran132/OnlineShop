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
                Console.WriteLine("You have no past orders.");
                return;
            }

            Console.WriteLine("Your Orders:");
            foreach (var order in customerOrders)
            {
                Console.WriteLine($"Order ID: {order.OrderID}, Date: {order.OrderDate}, Total: {order.CalculateTotal():C}");
            }
        }
    }
}


