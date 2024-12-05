namespace OnlineShop
{
    public class Product
    {
        public int ID { get; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public ProductCategory Category { get; set; }

        public Product(int id, string name, decimal price, int stock, ProductCategory category)
        {
            ID = id;
            Name = name;
            Price = price;
            Stock = stock;
            Category = category;
        }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Price: {Price}, Stock: {Stock}, Category: {Category.Name}";
        }
    }
}



