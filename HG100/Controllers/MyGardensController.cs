using HG100.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HG100.Controllers
{
    public class MyGardensController : Controller
    {
        private HomeGrowingEntities db = new HomeGrowingEntities();

        // GET: MyGardens
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var myGarden = db.MyGardens.Find(userId);
            return View(myGarden);
        }
        public ActionResult PartialGardenPlants(string id, string plantselected)
        {
            var userId = User.Identity.GetUserId();
            var myGarden = db.MyGardens.Find(userId);
            IEnumerable<Plant> plants;
            if (plantselected == "selected")
            {
                plants = myGarden.SelectedPlants.ToList();
            }
            else
            {
                plants = myGarden.FavortiePlants.ToList();
            }
            ViewBag.PlantSelected = plantselected; 
            return PartialView(plants);

        }
        [HttpPost]
        [Authorize]
        public ActionResult RemovePlant(int? id)
        {
            var userId = User.Identity.GetUserId();
            var Garden = db.MyGardens.Find(userId);
            var plant = db.Plants.Find(id);
            Garden.SelectedPlants.Remove(plant);
            db.Entry(Garden).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        //GET: MyGardens/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyGarden myGarden = db.MyGardens.Find(id);
            if (myGarden == null)
            {
                return HttpNotFound();
            }
            return View(myGarden);
        }

        // GET: MyGardens/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.PlantsId = new SelectList(db.Plants, "Id", "Type");
            return View();
        }

        // POST: MyGardens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,PlantsId")] MyGarden myGarden)
        {
            if (ModelState.IsValid)
            {
                db.MyGardens.Add(myGarden);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myGarden);
        }

        // GET: MyGardens/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyGarden myGarden = db.MyGardens.Find(id);
            if (myGarden == null)
            {
                return HttpNotFound();
            }

            return View(myGarden);
        }

        // POST: MyGardens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,PlantsId")] MyGarden myGarden)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myGarden).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myGarden);
        }
        // GET: MyGardens/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyGarden myGarden = db.MyGardens.Find(id);
            if (myGarden == null)
            {
                return HttpNotFound();
            }
            return View(myGarden);
        }

        // POST: MyGardens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MyGarden myGarden = db.MyGardens.Find(id);
            db.MyGardens.Remove(myGarden);
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
