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
    public class OsobaController : Controller
    {
        private KontaktiEntities db = new KontaktiEntities();

        // GET: Osoba
        public ActionResult Index(string searchString)
        {
            var listaOsoba = from o in db.Osoba
                             select o;

            foreach (var osoba in listaOsoba)
            {
                foreach (var broj in osoba.Broj.ToList())
                {
                    osoba.BrojeviText += "," + broj.broj1.ToString();
                }
                if (string.IsNullOrWhiteSpace(osoba.BrojeviText)) osoba.BrojeviText = "Nema brojeva.";
                else osoba.BrojeviText = osoba.BrojeviText.Substring(1);
            }
            
            if (!string.IsNullOrEmpty(searchString))
            {
                listaOsoba = listaOsoba.Where(l => l.ime.Contains(searchString) || l.prezime.Contains(searchString));
            }

            return View(listaOsoba.ToList());
        }

        // GET: Osoba/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Osoba.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // GET: Osoba/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Osoba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,ime,prezime,grad,opis,slika")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                db.Osoba.Add(osoba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osoba);
        }

        // GET: Osoba/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Osoba.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // POST: Osoba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,ime,prezime,grad,opis,slika")] Osoba osoba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(osoba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osoba);
        }

        // GET: Osoba/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Osoba.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // POST: Osoba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Osoba osoba = db.Osoba.Find(id);
            db.Osoba.Remove(osoba);
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
