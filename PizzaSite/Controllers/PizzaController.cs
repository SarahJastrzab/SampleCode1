using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzaSite.DAL;
using PizzaSite.Models;
using System.Data.Entity.Infrastructure;
using PizzaSite.ViewModels;

namespace PizzaSite.Controllers
{
    public class PizzaController : Controller
    {
        private PizzeriaContext db = new PizzeriaContext();

        public ActionResult Index(int? id, int? toppingID)
        {
            var viewModel = new OrderIndexData();

          
            if (id != null)
            {
                ViewBag.PizzaID = id.Value;
                viewModel.Toppings = viewModel.Pizzas.Where(
                    i => i.PizzaID == id.Value).Single().Toppings;
            }

            if (toppingID != null)
            {
                ViewBag.ToppingID = toppingID.Value;
                
            }

            return View(viewModel);
        }

        // GET: PizzaToppings/Details/5Vi
          public ActionResult Details(int? id, int? toppingID)
        {
          var viewModel = new OrderIndexData();
          viewModel.Pizzas = db.Pizzas
            //.Where(i => i.PizzaID == id.Value)
            //.Include(i => i.Toppings.Select(c => c.ToppingID))
            .Include(i => i.Size);

        if (id != null)
        {
          
            ViewBag.PizzaID = id.Value;
            viewModel.Toppings = viewModel.Pizzas.Where(
            i => i.PizzaID == id.Value).Single().Toppings;
        }
        if (toppingID != null)
        {
            ViewBag.ToppingID = toppingID.Value;
        }

    return View(viewModel);
        

        }

        // GET: PizzaToppings/Create
        public ActionResult Create(int? id)
        {

            PizzaViewModel viewModel = new PizzaViewModel();
                //pizza.OrderID = Convert.ToInt32(orderID);
                
                
               
                 //int theId = Convert.ToInt32(TempData["orderID"].ToString());
                 //ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", theId);



           viewModel.SelectedToppings= PopulateToppingData(viewModel);
                PopulateSizesDropDownList();
                PopulateOrdersDropDownList();

                return View(viewModel);
            
        }

        // POST: PizzaToppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PizzaID, SizeID, PizzaPrice, NumOrdered, OrderID,SelectedToppings")] PizzaViewModel pizza, string[] selectedToppings, int? id)
        {
            Pizza aPizzaEntity = new Pizza()
            {
                PizzaID =pizza.PizzaID,
                SizeID = pizza.SizeID,
                NumOrdered = pizza.NumOrdered,
                OrderID = pizza.OrderID
            };
            ICollection<Topping> aTopping = new List<Topping>();

            if (pizza.SelectedToppings != null)
            {
                //pizza.OrderID = Convert.ToInt32(orderID);
                foreach (var topping in pizza.SelectedToppings)
                {
                    if (topping.OnPizza == true)
                    {
                        Topping toppingsToAdd = db.Toppings.Where(d => d.ToppingID == topping.ToppingID).FirstOrDefault();

                        aTopping.Add(toppingsToAdd);

                    }
                }
            }
                
                if (ModelState.IsValid)
                {
                    aPizzaEntity.Toppings = aTopping;
                    db.Pizzas.Add(aPizzaEntity);
                    db.SaveChanges();
                    return RedirectToAction("../Order/Details", new { id = pizza.OrderID });
                }

                //@Html.ActionLink("Select", "Order/Index", new { id = model.OrderID });
                PopulateToppingData(pizza);
                PopulateSizesDropDownList(pizza.SizeID);
                PopulateOrdersDropDownList(pizza.OrderID);
                return View(pizza);
            
        }


        private List<ToppingsOnPizza> PopulateToppingData(PizzaViewModel pizza)
        {
            var allToppings = db.Toppings;
            if (pizza.SelectedToppings != null)
            {
                var pizzaToppings = new HashSet<int>(pizza.SelectedToppings.Select(c => c.ToppingID));
                var viewModel = new List<ToppingsOnPizza>();
                foreach (var topping in allToppings)
                {
                    viewModel.Add(new ToppingsOnPizza
                    {
                        ToppingID = topping.ToppingID,
                        ToppingName = topping.ToppingName,
                        OnPizza = pizzaToppings.Contains(topping.ToppingID)
                    });
                }
               return viewModel;

            }
            else
            {
                var viewModel = new List<ToppingsOnPizza>();
                foreach (var topping in allToppings)
                {
                    viewModel.Add(new ToppingsOnPizza
                    {
                        ToppingID = topping.ToppingID,
                        ToppingName = topping.ToppingName,
                        OnPizza =false
                    });
                }
                return viewModel;

            }
        }

     

        private void PopulateSizesDropDownList(object selectedSize = null)
        {
            var SizesQuery = from d in db.Sizes
                             orderby d.SizeName
                             select d;
            ViewBag.SizeID = new SelectList(SizesQuery, "SizeID", "SizeName", selectedSize);
        }            

        private void PopulateOrdersDropDownList(object selectedOrder = null)
        {
            var OrdersQuery = from d in db.Orders
                             orderby d.OrderID descending
                             select d;
            
            //int theId = Convert.ToInt32(TempData["orderID"].ToString());
            ViewBag.OrderID = new SelectList(OrdersQuery, "OrderID", "OrderID", Session["orderID"]);
           
        }


        // GET: PizzaToppings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: PizzaToppings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizzas
            .Include(i => i.Size)
            .Where(i => i.PizzaID == id)
            .Single();
            int orderID = pizza.OrderID;

            db.Pizzas.Remove(pizza);

            db.SaveChanges();
            return RedirectToAction("../Order/Details", new { id = pizza.OrderID });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}