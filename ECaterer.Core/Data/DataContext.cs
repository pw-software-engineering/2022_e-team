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
            var address = new Address()
            {
                AddressId = "f83c4738-ba84-4617-b0e3-6743ce3a39b0",
                Street = "Waryńskiego",
                City = "Warszawa",
                BuildingNumber = "12",
                ApartmentNumber = "228",
                PostCode = "00-631"

            };
            modelBuilder.Entity<Address>().HasData(address);
            modelBuilder.Entity<Client>().HasData(
                new Client()
                {
                    ClientId = "ffa4ebd8-39fb-46bd-a7bd-c2466c53030c",
                    Email = "klient@klient.pl",
                    Name = "Jan",
                    LastName = "Kowalski",
                    AddressId = "f83c4738-ba84-4617-b0e3-6743ce3a39b0",
                    PhoneNumber = "+48-322-228-444"
                });

            modelBuilder.Entity<OrderDiet>().HasKey(sc => new { sc.OrderId, sc.DietId });
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
        public virtual DbSet<OrderDiet> OrdersDiets { get; set; }

    }
}
