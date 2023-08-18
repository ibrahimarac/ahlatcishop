namespace Ahlatci.Shop.UI.Models.Dtos.Products
{
    public class ProductWithCategoryDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int UnitInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public CategoryDto Category { get; set; }
    }
}
