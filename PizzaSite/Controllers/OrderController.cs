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
    public class OrderController : Controller
    {
        PizzeriaContext db = new PizzeriaContext();

        // GET: Order
        public ActionResult Index(int? id, int? pizzaID, int? toppingID)
        {
          
            
                var viewModel = new OrderIndexData();


                if (id != null)
                {
                    //db.Configuration.LazyLoadingEnabled = false;
                    //ar order = db.Orders.Find(id); 
                    //var selectedOrder = viewModel.Orders.Where()
                    //var order = (from o in db.Orders
                                 //where o.OrderID == id
                                 //select o).FirstOrDefault<Order>();

                    //db.Entry(order).Collection(o => o.Pizzas).Load();

                    //var pizza = (from o in db.Pizzas
                                 //where o.PizzaID == id
                                 //select o).FirstOrDefault<Pizza>();

                    

                

               
               // var selectedOrder = viewModel.Orders.Where(x => x.OrderID == id).Single();
                ViewBag.Order = id.Value;
                viewModel.Pizzas = viewModel.Orders.Where(
                    i => i.OrderID == id.Value).Single().Pizzas;
            }

            if (pizzaID != null)
            {
                ViewBag.PizzaID = pizzaID.Value;
                viewModel.Toppings = viewModel.Pizzas.Where(
                    i => i.PizzaID == id.Value).Single().Toppings;
            } 
            if (toppingID != null)
            {
                ViewBag.ToppingID = toppingID.Value;

            }
                
                return View();
                //var orders = db.Orders.Include(o => o.Delivery);
                //return View(orders.ToList());
            }
        
        // GET: Order/Details/5
        public ActionResult Details(int? id, int? pizzaID, int? toppingID)
        {
          var viewModel = new OrderIndexData();
          viewModel.Orders = db.Orders
            .Where(i => i.OrderID == id.Value)
            .Include(i => i.Pizzas.Select(c => c.Size))
            .Include(i => i.Delivery);

        if (id != null)
        {
          
            ViewBag.OrderID = id.Value;
            viewModel.Pizzas = viewModel.Orders.Where(
            i => i.OrderID == id.Value).Single().Pizzas;
        }

    if (pizzaID != null)
    {
        ViewBag.PizzaID = pizzaID.Value;
        viewModel.Toppings = db.Pizzas.Where(d => d.PizzaID == pizzaID).FirstOrDefault().Toppings;
    }
    if (toppingID != null)
    {
        ViewBag.ToppingID = toppingID.Value;

    }
    return View(viewModel);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var order = new Order();

            //TempData["orderID"] = order.OrderID;
            ViewBag.DeliveryID = new SelectList(db.Deliveries, "DeliveryID", "TypeOfDelivery");
            PopulateDeliveriesDropDownList();
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,DeliveryID,ClientFirstName,ClientLastName,ClientPhone,Zip,StreetNumber,StreetName,Building,AptNumber, Subtotal,Tax,FinalTotal, TimeToWait")] Order order)
        {
          
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.Orders.Add(order);
                    int Query = order.OrderID;
                    Session["orderID"] = Query;
                    db.SaveChanges();
                    
                    //TempData["orderID"] = Query; 
                    return RedirectToAction("../Pizza/Create");
                }
            }catch(RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDeliveriesDropDownList(order.DeliveryID);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            PopulateDeliveriesDropDownList(order.DeliveryID);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderToUpdate = db.Orders.Find(id);
            if (TryUpdateModel(orderToUpdate, "",
                new string[] { "ClientFirstName", "TimeToWait", "ClientLastName","ClientPhone","Zip","StreetNumber","StreetName","Building","AptNumber", "DeliveryID", "Subtotal", "Tax", "FinalTotal" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateDeliveriesDropDownList(orderToUpdate.OrderID);
            return View(orderToUpdate);
        }

        private void PopulateDeliveriesDropDownList(object selectedDelivery = null)
        {
            var DeliveriesQuery = from d in db.Deliveries
                             orderby d.TypeOfDelivery
                             select d;
            ViewBag.DeliveryID = new SelectList(DeliveriesQuery, "DeliveryID", "TypeOfDelivery", selectedDelivery);
        } 
        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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
