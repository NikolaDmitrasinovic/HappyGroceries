namespace Receipt.Infrastructure.Configurations;

public class PurchaseReceiptConfiguration : IEntityTypeConfiguration<PurchaseReceipt>
{
    public void Configure(EntityTypeBuilder<PurchaseReceipt> builder)
    {
        builder.ToTable("purchase_receipts");

        builder.HasKey(pr => pr.Id);

        builder.Property(pr => pr.PurchaseDate)
            .IsRequired();

        builder.Property(pr => pr.Status)
            .IsRequired();

        builder.Property(pr => pr.TotalAmount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(pr => pr.Location)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(pr => pr.Lines)
            .WithOne()
            .HasForeignKey(rl => rl.ReceiptId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(PurchaseReceipt.Lines))!
            .SetField("_lines");

        builder.Metadata
            .FindNavigation(nameof(PurchaseReceipt.Lines))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
