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
    public class UsersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Users
        public ActionResult Index()
        {
            if(Session["user"] != null)
            {
                if(Session["role"].ToString().Equals("admin"))
                {
                    return View(db.users.ToList());
                }
            }
            return RedirectToAction("Index", "Fourms");
        }

        public ActionResult LoggedIn()
        {
            var fourms = db.fourms.Include(f => f.users);
            fourms = fourms.OrderByDescending(x => x.datetime);
            return View(fourms.ToList());
        }

        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["LoginMessage"] = null;
            Session["UserID"] = null;
            Session["uname"] = null;
            Session["fourms"] = null;
            Session["FourmsID"] = null;
            Session["role"] = null;
            var fourms = db.fourms.Include(f => f.users);
            fourms = fourms.OrderByDescending(x => x.datetime);
            return View(fourms.ToList());
        }
        public ActionResult Login()
        {
            Session["LoginMessage"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login([Bind(Include = "uname,password")] Users users)
        {
            var users1 = from c in db.users
                         select c;
            var userh = users1.Where(x => (x.uname.Equals(users.uname) && x.password.Equals(users.password))).FirstOrDefault();

            if (userh != null)
            {
                Session["uname"] = userh.uname;
                Session["user"] = userh;
                Session["LoginMessage"] = null;
                Session["UsersID"] = userh.UsersID;
                Session["role"] = userh.role;
                return RedirectToAction("LoggedIn");
            }

            return View();
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UsersID,name,uname,password,role")] Users users)
        {
            if (ModelState.IsValid)
            {
                bool x = db.users.Any(y => y.uname == users.uname);
                if (x == true)
                {
                    Session["LoginMessage"] = "User Already Exist!! try different username!";
                    return View(users);
                }
                db.users.Add(users);
                db.SaveChanges();
                Session["LoginMessage"] = null;
                return RedirectToAction("Index","Fourms");
            }

            return View(users);
        }

        public ActionResult ViewProfile(string uname)
        {
            if(string.IsNullOrEmpty(uname))
            {
                uname = Session["uname"].ToString();
            }
            var users = db.users.Where(y => y.uname.Equals(uname)).FirstOrDefault();
            if (users == null)
            {
                ViewData["puser"] = "User Deleted";
            }
            else
            {
                ViewData["puser"] = users.name;
            }
            var fourms = db.fourms.Include(f => f.users);
            var pfourms = fourms.Where(f => f.users.uname.Equals(uname));
            ViewData["pfourms"] = pfourms.ToList();
            var comments = db.comments.Include(f => f.fourms);
            var pcomments = comments.Where(f => f.uname.Equals(uname));
            ViewData["pcomments"] = pcomments.ToList();
            return View();
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UsersID,name,uname,password,role")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                var fourms = db.fourms.Include(f => f.users);
                fourms = fourms.OrderByDescending(x => x.datetime);
                return View(fourms.ToList());
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = db.users.Find(id);
            db.users.Remove(users);
            Session["user"] = null;
            Session["LoginMessage"] = null;
            Session["UserID"] = null;
            Session["uname"] = null;
            Session["fourms"] = null;
            Session["FourmsID"] = null;
            Session["role"] = null;
            db.SaveChanges();

            return RedirectToAction("Index", "Fourms");
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
