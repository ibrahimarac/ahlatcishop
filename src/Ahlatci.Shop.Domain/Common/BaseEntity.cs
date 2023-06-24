namespace Ahlatci.Shop.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
