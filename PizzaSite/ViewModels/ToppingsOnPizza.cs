using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaSite.ViewModels
{
    public class ToppingsOnPizza
    {
        public int ToppingID { get; set; }
        public string ToppingName { get; set; }
        public bool OnPizza { get; set; }

    }
}