namespace Receipt.Infrastructure.Configurations;

public class ReceiptLineConfiguration : IEntityTypeConfiguration<ReceiptLine>
{
    public void Configure(EntityTypeBuilder<ReceiptLine> builder)
    {
        builder.ToTable("receipt_lines");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ReceiptId)
            .IsRequired();

        builder.Property(x => x.ProductName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Ignore(x => x.LineTotal);
    }
}
