using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Domain.Entities
{
    public class OrderDetail : AuditableEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int? DiscountId { get; set; }
        public decimal LastPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
