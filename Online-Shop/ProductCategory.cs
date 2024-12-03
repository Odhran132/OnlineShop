namespace OnlineShop
{
    public class ProductCategory
    {
        public int CategoryID { get; }
        public string CategoryName { get; set; }

        public ProductCategory(int categoryId, string categoryName)
        {
            CategoryID = categoryId;
            CategoryName = categoryName;
        }
    }
}
