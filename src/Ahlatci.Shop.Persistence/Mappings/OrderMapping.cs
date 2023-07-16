using Ahlatci.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahlatci.Shop.Persistence.Mappings
{
    public class OrderMapping : AuditableEntityMapping<Order>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.CustomerId)
                .HasColumnName("CUSTOMER_ID")
                .HasColumnOrder(2);

            builder.Property(x => x.AddressId)
                .HasColumnName("ADDRESS_ID")
                .HasColumnOrder(3);

            builder.Property(x => x.OrderDate)
                .HasColumnName("ORDER_DATE")
                .HasDefaultValueSql("getdate()")
                .HasColumnOrder(4);

            builder.Property(x => x.Status)
                .HasColumnName("ORDER_STATUS")
                .HasColumnOrder(5);

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("ORDER_CUSTOMER_CUSTOMER_ID")
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.Address)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.AddressId)
                .HasConstraintName("ORDER_ADDRESS_ADDRESS_ID");

            builder.ToTable("ORDERS");
        }
    }
}
