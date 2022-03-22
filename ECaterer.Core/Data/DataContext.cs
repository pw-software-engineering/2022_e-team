using ECaterer.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECaterer.Core
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
       
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Complaint> Complaints { get; set; }
        public virtual DbSet<DeliveryDetails> DeliveryDetails { get; set; }
        public virtual DbSet<Diet> Diets { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Allergent> Allergents { get; set; }
        public virtual DbSet<ComplaintStatusEnum> ComplaintStatusEnum { get; set; }

        public virtual DbSet<OrderStatusEnum> OrderStatusEnum { get; set; }

    }
}
