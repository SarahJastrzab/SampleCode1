using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaSite.Models
{
    public class Size
    {
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        public double SizePrice { get; set; }
        public string Diameter { get; set; }

        //public virtual Pizza Pizza { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}