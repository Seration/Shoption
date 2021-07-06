using System;
using Microsoft.EntityFrameworkCore;

namespace Shoption.Services.Order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Shoption.Services.Order.Domain.Order> Orders { get; set; }
        public DbSet<Shoption.Services.Order.Domain.OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoption.Services.Order.Domain.Order>().ToTable("Orders", DEFAULT_SCHEMA);
            modelBuilder.Entity<Shoption.Services.Order.Domain.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);

            modelBuilder.Entity<Shoption.Services.Order.Domain.OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Shoption.Services.Order.Domain.Order>().OwnsOne(o => o.Address).WithOwner();

            base.OnModelCreating(modelBuilder);
        }
    }
}
