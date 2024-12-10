using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop
{
    public class Order
    {
        public int OrderID { get; }
        public int CustomerID { get; }
        public DateTime OrderDate { get; }
        public List<Product> OrderProducts { get; }

        public Order(int customerId, DateTime orderDate, List<Product> products)
        {
            OrderID = new Random().Next(1, 10000); // Unique Order ID
            CustomerID = customerId;
            OrderDate = orderDate;
            OrderProducts = products;
        }

        public double CalculateTotal()
        {
            return OrderProducts.Sum(p => p.Price * p.Quantity);
        }
        public string GetProductDetails()
        {
            return string.Join("\n", OrderProducts.Select(p => $"- {p.Description} x{p.Quantity} @ {p.Price:C} each"));
        }
    }
}


