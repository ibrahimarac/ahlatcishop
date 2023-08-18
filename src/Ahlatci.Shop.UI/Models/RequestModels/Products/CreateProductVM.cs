namespace Ahlatci.Shop.UI.Models.RequestModels.Products
{
    public class CreateProductVM
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int? UnitInStock { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}


