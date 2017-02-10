using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PizzaSite.Models
{
    public class Pizza
    {

        
        public int PizzaID { get; set; }

        [ForeignKey("Size")]
        [Required(ErrorMessage = "Size needed")]
        public int SizeID { get; set; }

        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required(
        ErrorMessage = "You must enter a number to order")]
        [RegularExpression("^[0-9]+$",
        ErrorMessage = "The Number you would like to order must be a number")]
        [Range(typeof(int), "1", "100")]
        public int NumOrdered { get; set; }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public double PizzaPrice
        {
            get{
                if (Size != null)
                {
                    return Size.SizePrice * NumOrdered;
                }
                else
                {
                    return NumOrdered;
                }
            }
            set
            {
                if (Size != null)
                {
                    NumOrdered = Convert.ToInt32(PizzaPrice / Size.SizePrice);
                }
            }

        }

        //public virtual PizzaOrder PizzaOrder { get; set; }
        //public virtual PizzaTopping PizzaTopping { get; set; }
        public virtual Size Size { get; set; }
        public virtual ICollection<Topping> Toppings { get; set; }
        //public virtual ICollection<Size> Sizes { get; set; }
        public virtual Order Order { get; set; }
    }
}