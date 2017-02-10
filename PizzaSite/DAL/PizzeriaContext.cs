using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaSite.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PizzaSite.DAL
{
    public class PizzeriaContext: DbContext
    {
        public PizzeriaContext()
            : base("PizzeriaContext")
        {

        }
        
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Topping> Toppings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Pizza>()
            .HasMany(c => c.Toppings).WithMany(i => i.Pizzas)
            .Map(t => t.MapLeftKey("PizzaID")
                .MapRightKey("ToppingID")
                .ToTable("PizzaTopping"));

        
        }
    }
}