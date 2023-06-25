using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Domain.Entities
{
    public class ProductImage : AuditableEntity
    {
        public int ProductId { get; set; }
        public string FileName { get; set; }
        public int Order { get; set; }
        public bool IsThumbnail { get; set; }

        public Product Product { get; set; }
    }
}
