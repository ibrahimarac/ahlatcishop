using Ahlatci.Shop.Application.Models.Dtos.Orders;
using Ahlatci.Shop.Application.Models.Dtos.Products;

namespace Ahlatci.Shop.Application.Models.Dtos.OrderDetails
{
    public class OrderDetailDto
    {
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public ProductDto Product { get; set; }
    }
}
