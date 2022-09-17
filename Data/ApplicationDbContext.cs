using Microsoft.EntityFrameworkCore;
using WebApi_InMemoryCaching_Sample.Entities;

namespace WebApi_InMemoryCaching_Sample.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Customer> Customers { get; set; }
    }
}