using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.Books.Entity;

public class BooksContext : DbContext
{
    public BooksContext(DbContextOptions<BooksContext> options) : base(options)
    {

    }
    public DbSet<BooksEntity> Books { get; set; }
}
