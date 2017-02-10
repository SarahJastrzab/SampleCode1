using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PizzaSite.Models;
  
namespace PizzaSite.DAL
{
    public class PizzeriaInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<PizzeriaContext>
    {
         protected override void Seed(PizzeriaContext context)
        {

            var sizes = new List<Size>
             {
                 new Size{SizeID= 1, SizeName="Small", SizePrice=7.45, Diameter= "4in",},
                 new Size{SizeID= 2, SizeName="Medium", SizePrice=10.45, Diameter= "6in",},
                 new Size{SizeID= 3, SizeName="Large", SizePrice=13.45, Diameter= "8in",}
             };

            sizes.ForEach(s => context.Sizes.Add(s));
            context.SaveChanges();
           

            var deliveries = new List<Delivery>
            {
                new Delivery{DeliveryID = 1, TypeOfDelivery = "Pickup",DeliveryPrice=0.0,},
                new Delivery{DeliveryID = 2, TypeOfDelivery="Delivery", DeliveryPrice=1.00}
            };
            deliveries.ForEach(s => context.Deliveries.Add(s));
            context.SaveChanges();

          
             var pizzas = new List<Pizza>
             {
                 new Pizza{PizzaID = 1, NumOrdered= 1,PizzaPrice=1.00}
             };

              pizzas.ForEach(s => context.Pizzas.Add(s));
             context.SaveChanges();


             var toppings = new List<Topping>
             {
                 new Topping{ToppingID = 1, ToppingName="Cheese",},
                 new Topping{ToppingID = 2, ToppingName="Pepperoni",},
                 new Topping{ToppingID = 3, ToppingName="Onions",},
                 new Topping{ToppingID = 4, ToppingName="Bell Peppers",},
                 new Topping{ToppingID = 5, ToppingName="Hot Peppers",},
                 new Topping{ToppingID = 6, ToppingName="Ham",},
                 new Topping{ToppingID = 7, ToppingName="Pineapple",},
                 new Topping{ToppingID = 8, ToppingName="Olives",},
                 new Topping{ToppingID = 9, ToppingName="Spinach",},
                 new Topping{ToppingID = 10, ToppingName="Garlic"}
             };
             toppings.ForEach(s => context.Toppings.Add(s));
             context.SaveChanges();

         }
            
         
    }
}