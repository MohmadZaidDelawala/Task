using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Task.Models;

namespace Task.ApplicationContext
{

    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<UserMaster> UserMaster { get; set; }
        public DbSet<TokenMaster> TokenMaster { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TokenMaster -> UserMaster (many-to-one)
            modelBuilder.Entity<TokenMaster>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tokens)
                .HasForeignKey(t => t.EmployeeID)
                .OnDelete(DeleteBehavior.Cascade);

            // ProductMaster -> UserMaster (many-to-one)
            modelBuilder.Entity<ProductMaster>()
                .HasOne(p => p.AddedByUser)
                .WithMany(u => u.ProductsAdded)
                .HasForeignKey(p => p.AddedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
