using Antlr.Runtime.Misc;
using HG100.Models;
using Microsoft.AspNet.Identity;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HG100.Controllers
{
    public class PlantsController : Controller
    {
        private HomeGrowingEntities db = new HomeGrowingEntities();

        // GET: Plants
        public ActionResult Index()
        {
            return View(db.Plants.ToList());
        }
        public ActionResult Shop(PLantFilterViewModel filterViewModel)
        {
            IEnumerable<Plant> plants = db.Plants;
            if(filterViewModel != null && filterViewModel.zone != 0)
            {
                plants = plants.Where(n => n.Zone == filterViewModel.zone);
                if (! string.IsNullOrWhiteSpace(filterViewModel.PlantType))
                {
                    plants = plants.Where(n => n.Type.StartsWith(filterViewModel.PlantType));
                }
 
            }
            return View(plants.ToList());
        }
        public ActionResult _PlantFilterPartial()
        {
            ViewBag.AllZones = new SelectList(Enumerable.Range(1, 10));
            return PartialView();
        }
        public ActionResult Favorite()
        {
            return RedirectToAction("Shop");
        }
        [HttpPost]
        [Authorize]
        public ActionResult Favorite(int? id)
        {
            var userId = User.Identity.GetUserId();
            var Garden = db.MyGardens.Find(userId);
            var plant = db.Plants.Find(id);
            Garden.FavortiePlants.Add(plant);
            db.Entry(Garden).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Shop");
        }
        [HttpPost]
        [Authorize]
        public ActionResult SelectPlant(int? id)
        {
            var userId = User.Identity.GetUserId();
            var Garden = db.MyGardens.Find(userId);
            var plant = db.Plants.Find(id);
            Garden.SelectedPlants.Add(plant);
            db.Entry(Garden).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Shop");

        }
 

        // GET: Plants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }


        // GET: Plants/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Plants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Type,Shade,Bloom,Zone,Color,Description,PlantName")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                db.Plants.Add(plant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(plant);
        }

        // GET: Plants/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Type,Shade,Bloom,Zone,Color,Description,PlantName")] Plant plant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(plant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(plant);
        }

        // GET: Plants/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plant plant = db.Plants.Find(id);
            if (plant == null)
            {
                return HttpNotFound();
            }
            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Plant plant = db.Plants.Find(id);
            db.Plants.Remove(plant);
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
