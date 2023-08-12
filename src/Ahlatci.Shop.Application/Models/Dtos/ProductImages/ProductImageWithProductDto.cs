using Ahlatci.Shop.Application.Models.Dtos.Products;

namespace Ahlatci.Shop.Application.Models.Dtos
{
    public class ProductImageWithProductDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
        public int Order { get; set; }
        public bool? IsThumbnail { get; set; }

        public ProductDto Product { get; set; }
    }
}
