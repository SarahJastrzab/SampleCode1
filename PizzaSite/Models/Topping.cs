using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaSite.Models
{
    public class Topping
    {
        public Topping()
        {

        }
        public int ToppingID { get; set; }
        public string ToppingName { get; set; }

        //public PizzaTopping PizzaTopping { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}