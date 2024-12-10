using OnlineShop;
using System;
using System.Collections.Generic;
using System.Linq;

public class ShoppingBasket
{
    public List<Product> Products { get; } = new List<Product>();

    // Add a product to the basket
    public void AddProduct(Product product)
    {
        var existingProduct = Products.FirstOrDefault(p => p.ProductID == product.ProductID);

        // Check if the product is already in the basket
        if (existingProduct != null)
        {
            // Check if the quantity added doesn't exceed stock
            if (existingProduct.Quantity + product.Quantity <= product.StockQuantity)
            {
                existingProduct.Quantity += product.Quantity;
                Console.WriteLine($"Product {product.Description} quantity updated in the basket.");
            }
            else
            {
                Console.WriteLine($"Cannot add {product.Quantity} of {product.Description}. Not enough stock.");
            }
        }
        else
        {
            // Check if the product quantity doesn't exceed stock when first adding it
            if (product.Quantity <= product.StockQuantity)
            {
                Products.Add(product);
                Console.WriteLine($"Product {product.Description} added to the basket.");
            }
            else
            {
                Console.WriteLine($"Cannot add {product.Quantity} of {product.Description}. Not enough stock.");
            }
        }
    }

    // Remove a product from the basket
    public void RemoveProduct(Product product)
    {
        Products.Remove(product);
        Console.WriteLine($"Product {product.Description} removed from the basket.");
    }

    // Method to view the basket
    public void ViewBasket()
    {
        if (Products.Count == 0)
        {
            Console.WriteLine("Your basket is empty.");
            return;
        }

        Console.WriteLine("Your Basket:");
        Console.WriteLine("ID | Name | Quantity | Price | Total");
        Console.WriteLine("-------------------------------------------");

        double grandTotal = 0; // To calculate the total price of all items

        foreach (var item in Products)
        {
            double totalPrice = item.Quantity * item.Price;
            grandTotal += totalPrice;

            // Display product details
            Console.WriteLine($"{item.ProductID} | {item.Description} | {item.Quantity} | {item.Price:C} | {totalPrice:C}");
        }

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"Grand Total: {grandTotal:C}");
    }

    // Clear all products in the basket
    public void ClearBasket()
    {
        Products.Clear();
        Console.WriteLine("Basket cleared.");
    }
}
