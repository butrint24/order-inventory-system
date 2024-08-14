using Microsoft.EntityFrameworkCore;

namespace Inventory.Data.Sql;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
