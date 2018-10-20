using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class FourmsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Fourms
        public ActionResult Index()
        {
            var fourms = db.fourms.Include(f => f.users);
            fourms = fourms.OrderByDescending(x => x.datetime);
            return View(fourms.ToList());
        }

        // GET: Fourms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fourms fourms = db.fourms.Find(id);
            if (fourms == null)
            {
                return HttpNotFound();
            }
            //fourms.users = (Users)Session["user"];
            var comments = db.comments.Include(f => f.fourms);
            ViewData["comments"] = comments.ToList();
            Session["FourmsID"] = fourms.FourmsID;
            Session["fdatetime"] = fourms.datetime;
            Session["fourms"] = fourms;
            return View(fourms);
        }

        // GET: Fourms/Create
        public ActionResult Create()
        {
            ViewBag.UsersID = new SelectList(db.users, "UsersID", "name");
            return View();
        }

        // POST: Fourms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FourmsID,title,text")] Fourms fourms)
        {
            if (ModelState.IsValid)
            {
                fourms.UsersID = int.Parse(Session["UsersID"].ToString());
                fourms.datetime = DateTime.Now;
                db.fourms.Add(fourms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsersID = new SelectList(db.users, "UsersID", "name", fourms.UsersID);
            return View(fourms);
        }

        // GET: Fourms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fourms fourms = db.fourms.Find(id);
            if (fourms == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsersID = new SelectList(db.users, "UsersID", "name", fourms.UsersID);
            return View(fourms);
        }

        // POST: Fourms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FourmsID,title,text")] Fourms fourms)
        {
            if (ModelState.IsValid)
            {
                fourms.UsersID = int.Parse(Session["UsersID"].ToString());
                fourms.datetime = DateTime.Now;
                db.Entry(fourms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsersID = new SelectList(db.users, "UsersID", "name", fourms.UsersID);
            return View(fourms);
        }

        // GET: Fourms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fourms fourms = db.fourms.Find(id);
            if (fourms == null)
            {
                return HttpNotFound();
            }
            return View(fourms);
        }

        // POST: Fourms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Fourms fourms = db.fourms.Find(id);
            db.fourms.Remove(fourms);
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
