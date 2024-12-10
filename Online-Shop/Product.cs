namespace OnlineShop
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; } // Links to ProductCategory
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public int Quantity { get; set; } // For basket quantity

        public Product(int productID, string name, string description, int categoryId, double price, int stockQuantity)
        {
            ProductID = productID;
            Name = name;
            Description = description;
            CategoryID = categoryId;
            Price = price;
            StockQuantity = stockQuantity;
            Quantity = 0;
        }

        public Product(int productID, string description, int categoryID, double price, int quantity)
        {
            ProductID = productID;
            Description = description;
            CategoryID = categoryID;
            Price = price;
            Quantity = quantity;
        }
    }
}
