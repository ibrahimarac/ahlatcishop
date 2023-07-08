using Ahlatci.Shop.Domain.Common;

namespace Ahlatci.Shop.Domain.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Customer> Customers { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
