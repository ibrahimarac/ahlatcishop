using Ahlatci.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ahlatci.Shop.Persistence.Mappings
{
    public class CommentMapping : AuditableEntityMapping<Comment>
    {
        public override void ConfigureDerivedEntityMapping(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.ProductId)
                .HasColumnName("PRODUCT_ID")
                .HasColumnOrder(2);

            builder.Property(x => x.CustomerId)
                .HasColumnName("CUSTOMER_ID")
                .HasColumnOrder(3);

            builder.Property(x => x.Detail)
                .IsRequired()
                .HasColumnName("DETAIL")
                .HasColumnType("nvarchar(max)")
                .HasColumnOrder(4);

            builder.Property(x => x.LikeCount)
                .HasColumnName("LIKE_COUNT")
                .HasColumnOrder(5);

            builder.Property(x => x.LikeCount)
                .HasColumnName("DISLIKE_COUNT")
                .HasColumnOrder(6);

            builder.Property(x => x.IsApproved)
                .HasColumnName("IS_APPROVED")
                .HasDefaultValueSql("0")
                .HasColumnOrder(7);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProductId)
                .HasConstraintName("COMMENT_PRODUCT_PRODUCT_ID");

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("COMMENT_CUSTOMER_CUSTOMER_ID");

            builder.ToTable("COMMENTS");
        }
    }
}
