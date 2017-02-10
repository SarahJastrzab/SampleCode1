using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaSite.Models;
namespace PizzaSite.ViewModels
{
    public class OrderIndexData
    {
        public IEnumerable<Pizza> Pizzas { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<Topping> Toppings { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}