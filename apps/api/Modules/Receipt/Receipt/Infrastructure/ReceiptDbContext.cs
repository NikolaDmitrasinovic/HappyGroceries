namespace Receipt.Infrastructure;

public class ReceiptDbContext(DbContextOptions<ReceiptDbContext> options) : DbContext(options)
{
    public DbSet<PurchaseReceipt> PurchaseReceipts => Set<PurchaseReceipt>();
    public DbSet<ReceiptLine> ReceiptLines => Set<ReceiptLine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("receipt");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
