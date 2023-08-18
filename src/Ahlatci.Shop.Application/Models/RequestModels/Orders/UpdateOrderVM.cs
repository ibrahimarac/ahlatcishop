using Ahlatci.Shop.Domain.Entities;

namespace Ahlatci.Shop.Application.Models.RequestModels.Orders
{
    public class UpdateOrderVM
    {
        public int? OrderId { get; set; }
        public OrderStatus? StatusId { get; set; }
        public int AddressId { get; set; }
    }
}
