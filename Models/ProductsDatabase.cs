namespace DataToPDFExcel.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string BrandID { get; set; }
        public string CategoryID { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductsView
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }

    public class Brands
    {
        public string BrandID { get; set; }
        public string BrandName { get; set; }
    }

    public class Categories
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
