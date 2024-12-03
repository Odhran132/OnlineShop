namespace OnlineShop
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; } // Links to ProductCategory
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public int Quantity { get; set; } // For basket quantity

        public Product(int productID, string description, int categoryId, double price, int stockQuantity)
        {
            ProductID = productID;
            Description = description;
            CategoryID = categoryId;
            Price = price;
            StockQuantity = stockQuantity;
            Quantity = 0;
        }
    }
}



