using Microsoft.EntityFrameworkCore;
using System;
using static SE_pranworks.Models.Modelecom;

namespace SE_pranworks.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Customer) 
                .WithMany(c => c.Orders)  
                .HasForeignKey(o => o.CustomerId); 

            
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Order) 
                .WithMany(o => o.Products) 
                .HasForeignKey(p => p.OrderId); 
        }

    }
}
