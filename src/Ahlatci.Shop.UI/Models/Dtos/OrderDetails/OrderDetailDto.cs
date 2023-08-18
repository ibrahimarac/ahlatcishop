using Ahlatci.Shop.UI.Models.Dtos.Products;

namespace Ahlatci.Shop.UI.Models.Dtos.OrderDetails
{
    public class OrderDetailDto
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public CategoryDto Category { get; set; }
        public ProductDto Product { get; set; }
    }
}
