using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaSite.Models
{
    public class Order
    {
       
        public int OrderID { get; set; }

       
        [ForeignKey("Delivery")]
        [Required(
       ErrorMessage = "Delivery Type required")]
        public int DeliveryID { get; set; }

        [Required(
        ErrorMessage = "First Name required")]
        [RegularExpression( "^[a-zA-Z0-9]+$",
        ErrorMessage = "First Name may only contain numbers or letters")]
        public string ClientFirstName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$",
        ErrorMessage = "First Name may only contain numbers or letters")]
        public string ClientLastName { get; set; }

         [Required(
        ErrorMessage = "Phone Number required")]
        [RegularExpression("^[0-9 ]+$",
        ErrorMessage = "Phone Number may only contain numbers")]
        public string ClientPhone { get; set; }

        [RegularExpression("^[0-9 ]+$",
        ErrorMessage = "Zip may only contain numbers")]
        public string Zip { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$",
        ErrorMessage = "Street Number may only contain numbers or letters")]
        public string StreetNumber { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$",
        ErrorMessage = "Street Name may only contain numbers or letters")]
        public string StreetName { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$",
        ErrorMessage = "Building may only contain numbers or letters")]
        public string Building { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]+$",
        ErrorMessage = "Apt Number may only contain numbers or letters")]
        public string AptNumber { get; set; }
       // [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public double TimeToWait
        {

            get
            {
                double time = 0;
                if (Pizzas != null)
                {
                    foreach (var pizza in Pizzas)
                    {
                        time = time + 10;
                    }

                }
                return time;
            }
            set
            {
                double time = 0;
                if (Pizzas != null)
                {
                    foreach (var pizza in Pizzas)
                    {
                        time = time + 10;
                    }

                }
            }
        }
        public double Subtotal
        {

            get
            {
                double subtotal = 0;
                if (Pizzas != null)
                {
                    foreach (var pizza in Pizzas)
                    {
                        subtotal = subtotal + pizza.PizzaPrice;
                    }

                    subtotal = subtotal + Delivery.DeliveryPrice;
                }
                return subtotal;
            }
            set
            {
                double subtotal = 0;
                if (Pizzas != null)
                {
                    foreach (var pizza in Pizzas)
                    {
                        subtotal = subtotal + pizza.PizzaPrice;
                    }
                }
                if (Delivery != null)
                {
                    subtotal = subtotal + Delivery.DeliveryPrice;
                }
            }
        }
        public double Tax
        {
            get
            {
                double tax = 1.08;
                return tax;
            }


            set
            {
                double tax = 1.08;
            }
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double FinalTotal
        {

            get
            {
                double finalTotal = 0;
                finalTotal = Subtotal * Tax;
                return finalTotal;
            }
            set { double finalTotal = 0;}
        }

        //public virtual ICollection<Client> Clients { get; set; }

    
        public virtual Delivery Delivery { get; set; }
        
        //public virtual PizzaOrder PizzaOrder { get; set; }
        //public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}