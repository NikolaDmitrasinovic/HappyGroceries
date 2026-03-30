namespace Receipt.Infrastructure;

public class PurchaseReceiptDbContext(DbContextOptions<PurchaseReceiptDbContext> options) : DbContext(options)
{
    public DbSet<PurchaseReceipt> PurchaseReceipts => Set<PurchaseReceipt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("receipt");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
