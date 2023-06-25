using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Domain.Entities
{

    public class Product : AuditableEntity
    {       
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public int UnitInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string ThumbnailImage { get; set; }

        //Navigation Property
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
