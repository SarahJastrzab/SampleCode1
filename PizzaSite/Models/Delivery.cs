using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaSite.Models
{
    public class Delivery
    {
        public int DeliveryID { get; set; }
        public string TypeOfDelivery { get; set; }
        public double DeliveryPrice { get; set; }

        //public virtual Order Order { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}