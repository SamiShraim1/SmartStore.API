using Microsoft.EntityFrameworkCore;
using task2API.Data.Models;

namespace task2API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<Product> Products { get; set; }

    }
}
