namespace Inventory.Data.Configurations;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(50).IsRequired();
        builder.Property(p => p.Category).IsRequired();
        builder.Property(p => p.Description).HasMaxLength(250);
        builder.Property(p => p.ImageFile).HasMaxLength(200);
        builder.Property(p => p.Price);
        builder.Property(p => p.Stock).IsRequired();
        builder.Property(p => p.Threshold).IsRequired();
    }
}
