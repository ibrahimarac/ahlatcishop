using Ahlatci.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahlatci.Shop.Persistence.Mappings
{
    public class CustomerMapping : AuditableEntityMapping<Customer>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(x => x.CityId)
                .HasColumnName("CITY_ID")
                .HasColumnOrder(3);

            builder.Property(x => x.IdentityNumber)
                .HasColumnName("IDENTITY_NUMBER")
                .HasColumnType("nchar(11)")
                .IsRequired()
                .HasColumnOrder(4);

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("nvarchar(30)")
                .IsRequired()
                .HasColumnOrder(5);

            builder.Property(x => x.Surname)
                .HasColumnName("SURNAME")
                .HasColumnType("nvarchar(30)")
                .IsRequired()
                .HasColumnOrder(6);

            builder.Property(x => x.Email)
                .HasColumnName("EMAIL")
                .HasColumnType("nvarchar(150)")
                .IsRequired()
                .HasColumnOrder(7);

            builder.Property(x => x.Phone)
                .HasColumnName("PHONE")
                .HasColumnType("nchar(13)")
                .IsRequired()
                .HasColumnOrder(8);

            builder.Property(x => x.Birtdate)
                .HasColumnName("BIRTHDATE")
                .IsRequired()
                .HasColumnOrder(9);

            builder.Property(x => x.Gender)
                .HasColumnName("GENDER")
                .IsRequired()
                .HasColumnOrder(10);

            builder.HasOne(x => x.City)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.CityId)
                .HasConstraintName("CUSTOMER_CITY_CITY_ID");

            builder.ToTable("CUSTOMERS");
        }
    }
}
