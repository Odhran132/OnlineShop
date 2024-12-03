using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop
{
    public class ShoppingBasket
    {
        public List<Product> Products { get; } = new List<Product>();

        public void AddProduct(Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existingProduct != null)
            {
                existingProduct.Quantity += product.Quantity;
            }
            else
            {
                Products.Add(product);
            }
            Console.WriteLine($"Product {product.Description} added to the basket.");
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
            Console.WriteLine($"Product {product.Description} removed from the basket.");
        }

        public void ViewBasket()
        {
            if (Products.Count == 0)
            {
                Console.WriteLine("Your basket is empty.");
                return;
            }

            Console.WriteLine("Your Basket:");
            foreach (var item in Products)
            {
                Console.WriteLine($"Product: {item.Description}, Quantity: {item.Quantity}, Total Price: {item.Quantity * item.Price:C}");
            }
        }

        public void ClearBasket()
        {
            Products.Clear();
            Console.WriteLine("Basket cleared.");
        }
    }
}



