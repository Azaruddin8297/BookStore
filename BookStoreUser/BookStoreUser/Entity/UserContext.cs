using Microsoft.EntityFrameworkCore;

namespace BookStore.User.Entity;

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options) 
        {
                    
        }
    public DbSet<UserEntity> Users { get; set; }
    }

