using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi=>oi.Id);

        builder.Property(oi=>oi.UnitPrice)
            .HasPrecision(18, 2);

        builder.Ignore(oi=>oi.Subtotal);
    }
}