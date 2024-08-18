using Microsoft.EntityFrameworkCore;
using Order.Data.Contracts;
using OrderModel = Order.Data.Contracts.Order;

namespace Order.Data.Sql;

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
        
        modelBuilder.Entity<Contracts.Order>()
            .Property(o => o.Status)
            .HasConversion(
                v => v.ToString(),
                v => (OrderStatus)Enum.Parse(typeof(OrderStatus), v));
        
        modelBuilder.Entity<Contracts.Order>()
            .Property(o => o.ProductId)
            .IsRequired(); 
    }
}
