using Microsoft.EntityFrameworkCore;

namespace BookStore.Order.Entity
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
