using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KontaktiMVC;

namespace KontaktiMVC.Controllers
{
    public class BrojController : Controller
    {
        private KontaktiEntities db = new KontaktiEntities();

        // GET: Broj
        public ActionResult Index()
        {
            var broj = db.Broj.Include(b => b.Osoba);
            return View(broj.ToList());
        }

        // GET: Broj/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broj broj = db.Broj.Find(id);
            if (broj == null)
            {
                return HttpNotFound();
            }
            return View(broj);
        }

        // GET: Broj/Create
        public ActionResult Create()
        {
            ViewBag.id_osoba = new SelectList(db.Osoba, "id", "ime");
            return View();
        }

        // POST: Broj/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "broj1,tip,opis,id_osoba")] Broj broj)
        {
            if (ModelState.IsValid)
            {
                db.Broj.Add(broj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_osoba = new SelectList(db.Osoba, "id", "ime", broj.id_osoba);
            return View(broj);
        }

        // GET: Broj/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broj broj = db.Broj.Find(id);
            if (broj == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_osoba = new SelectList(db.Osoba, "id", "ime", broj.id_osoba);
            return View(broj);
        }

        // POST: Broj/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "broj1,tip,opis,id_osoba")] Broj broj)
        {
            if (ModelState.IsValid)
            {
                db.Entry(broj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_osoba = new SelectList(db.Osoba, "id", "ime", broj.id_osoba);
            return View(broj);
        }

        // GET: Broj/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Broj broj = db.Broj.Find(id);
            if (broj == null)
            {
                return HttpNotFound();
            }
            return View(broj);
        }

        // POST: Broj/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Broj broj = db.Broj.Find(id);
            db.Broj.Remove(broj);
            db.SaveChanges();
            return RedirectToAction("Index","Osoba");
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
