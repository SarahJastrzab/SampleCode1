using PizzaSite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaSite.ViewModels
{
    public class PizzaViewModel
    {
        public int PizzaID { get; set; }

     
        [Required(ErrorMessage = "Size needed")]
        public int SizeID { get; set; }

       
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
            get
            {
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
        public virtual Size Size { get; set; }

        public List<ToppingsOnPizza> SelectedToppings { get; set; }
    }
}