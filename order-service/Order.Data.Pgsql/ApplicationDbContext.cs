using Microsoft.EntityFrameworkCore;
using Order.Data.Contracts;
using OrderModel = Order.Data.Contracts.Order;

namespace Order.Data.Pgsql;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<OrderModel> Orders { get; set; }
    
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contracts.Order>()
            .HasOne(x => x.CustomerDetails)
            .WithMany(o => o.Orders)        
            .HasForeignKey(x => x.CustomerId);

        
    }
}
