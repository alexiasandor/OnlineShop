using Laroa.Domain;
using Microsoft.EntityFrameworkCore;

namespace Laroa.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reduceri> Reduceri { get; set; }
        public DbSet<Admin> Admin{ get; set; }
        public DbSet<User> Users { get; set; }
    }
}