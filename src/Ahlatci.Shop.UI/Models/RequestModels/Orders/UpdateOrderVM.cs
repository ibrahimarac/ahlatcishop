namespace Ahlatci.Shop.UI.Models.RequestModels.Orders
{
    public class UpdateOrderVM
    {
        public int? OrderId { get; set; }
        public OrderStatus? StatusId { get; set; }
        public int AddressId { get; set; }
    }
}
