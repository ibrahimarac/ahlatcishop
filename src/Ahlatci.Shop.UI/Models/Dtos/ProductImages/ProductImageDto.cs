namespace Ahlatci.Shop.UI.Models.Dtos.ProductImages
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Path { get; set; }
        public int Order { get; set; }
        public bool? IsThumbnail { get; set; }
    }
}
