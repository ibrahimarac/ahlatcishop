namespace Ahlatci.Shop.Application.Models.RequestModels.Products
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int? UnitInStock { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}


