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
    public class CommentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Comments
        public ActionResult Index()
        {
            var comments = db.comments.Include(c => c.fourms);
            return View(comments.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.FourmsID = new SelectList(db.fourms, "FourmsID", "title");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create(string commenttext)
        {
            if(!string.IsNullOrEmpty(commenttext) && Session["user"]!=null && Session["fourms"]!=null)
            {
                Comments comments = new Comments();
                comments.commenttext = commenttext;
                comments.uname = Session["uname"].ToString();
                comments.FourmsID = int.Parse(Session["FourmsID"].ToString());
                comments.datetime = DateTime.Now;
                db.comments.Add(comments);
                db.SaveChanges();
            }
            int id1 = int.Parse(Session["FourmsID"].ToString());
            string go1 = "Details/" + id1;
            return RedirectToAction(go1,"Fourms");
        }
        /*public ActionResult Create([Bind(Include = "CommentsID,commenttext,uname,FourmsID")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.comments.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FourmsID = new SelectList(db.fourms, "FourmsID", "title", comments.FourmsID);
            return View(comments);
        }*/

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.FourmsID = new SelectList(db.fourms, "FourmsID", "title", comments.FourmsID);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentsID,commenttext,uname,FourmsID,datetime")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                int id1 = int.Parse(Session["FourmsID"].ToString());
                string go1 = "Details/" + id1;
                return RedirectToAction(go1, "Fourms");
            }
            ViewBag.FourmsID = new SelectList(db.fourms, "FourmsID", "title", comments.FourmsID);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.comments.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.comments.Find(id);
            db.comments.Remove(comments);
            db.SaveChanges();
            int id1 = int.Parse(Session["FourmsID"].ToString());
            string go1 = "Details/" + id1;
            return RedirectToAction(go1, "Fourms");
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
