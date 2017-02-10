namespace PizzaSite.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PizzaSite.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<PizzaSite.DAL.PizzeriaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PizzaSite.DAL.PizzeriaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //C:\Users\Sarah\Documents\Visual Studio 2013\Projects\PizzaSite\PizzaSite\Controllers\ClientController.cs
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Deliveries.AddOrUpdate(
                new Delivery{DeliveryID = 1, TypeOfDelivery = "Pickup",DeliveryPrice=0.0,},
                new Delivery{DeliveryID = 2, TypeOfDelivery="Delivery", DeliveryPrice=1.00});
           
           
            context.SaveChanges();
            context.Sizes.AddOrUpdate(
                 new Size { SizeID = 1, SizeName = "Small", SizePrice = 11.00, Diameter = "4in", },
                 new Size { SizeID = 2, SizeName = "Medium", SizePrice = 13.00, Diameter = "6in", },
                 new Size { SizeID = 3, SizeName = "Large", SizePrice = 15.00, Diameter = "8in", });

            context.SaveChanges();

            context.Toppings.AddOrUpdate(
                 new Topping { ToppingID = 1, ToppingName = "Cheese", },
                 new Topping { ToppingID = 2, ToppingName = "Pepperoni", },
                 new Topping { ToppingID = 3, ToppingName = "Onions", },
                 new Topping { ToppingID = 4, ToppingName = "Bell Peppers", },
                 new Topping { ToppingID = 5, ToppingName = "Hot Peppers", },
                 new Topping { ToppingID = 6, ToppingName = "Ham", },
                 new Topping { ToppingID = 7, ToppingName = "Pineapple", },
                 new Topping { ToppingID = 8, ToppingName = "Olives", },
                 new Topping { ToppingID = 9, ToppingName = "Spinach", },
                 new Topping { ToppingID = 10, ToppingName = "Garlic" });

            context.SaveChanges();

        }
    }
}
