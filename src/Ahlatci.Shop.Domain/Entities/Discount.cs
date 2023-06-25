namespace Ahlatci.Shop.Domain.Entities
{
    public class Discount
    {
        public decimal Amount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public enum DiscountType
    {
        Percent=1,
        Total=2
    }

}
