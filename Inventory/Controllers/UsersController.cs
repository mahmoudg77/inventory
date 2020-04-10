using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class UsersController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Users
        [RoleFilter(new int[] { 1})]
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Department);
            return View(users.ToList());
        }

        public ActionResult GetData()
        {
            using (InventoryEntities db = new InventoryEntities())
            {
                List<User> usrList = db.Users.ToList<User>();
                return Json(new { data = usrList }, JsonRequestBehavior.AllowGet);
            }
        }


        // GET: Users/Details/5
        [RoleFilter(new int[] { 1 })]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [RoleFilter(new int[] { 1 })]
        public ActionResult Create()
        {
            ViewBag.Dep_Id = new SelectList(db.Departments, "Dep_Id", "Dep_Nam");

            return View(new User());
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(new int[] { 1 })]
        public ActionResult Create([Bind(Include = "Usr_Id,Usr_Type,F_Name,L_Name,Pho_Num,Dep_Id,Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                user.Created_At = DateTime.Now;
                user.Created_By = UserSession.User.F_Name + " " + UserSession.User.L_Name;
               
                user.Status = true;
                try
                {
                db.SaveChanges();

                }
                catch (Exception ex )
                {

                    throw ex;
                }
                return RedirectToAction("Index");
            }

            ViewBag.Dep_Id = new SelectList(db.Departments, "Dep_Id", "Dep_Nam", user.Dep_Id);
            return View(user);
        }

        // GET: Users/Edit/5
        [RoleFilter(new int[] { 1 })]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.Dep_Id = new SelectList(db.Departments, "Dep_Id", "Dep_Nam", user.Dep_Id);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleFilter(new int[] { 1 })]
        public ActionResult Edit([Bind(Include = "Usr_Id,Usr_Type,F_Name,L_Name,Pho_Num,Dep_Id,Status")] User user)
        {
            if (ModelState.IsValid)
            {

                var dbUser=db.Users.Find(user.Usr_Id);//.State = EntityState.Modified;
                dbUser.Updated_At = DateTime.Now;
                dbUser.Updated_By = UserSession.User.F_Name + " " + UserSession.User.L_Name;

                dbUser.Usr_Id = user.Usr_Id;
                dbUser.Usr_Type = user.Usr_Type;
                dbUser.F_Name = user.F_Name;
                dbUser.L_Name = user.L_Name;
                dbUser.Pho_Num = user.Pho_Num;
                dbUser.Dep_Id = user.Dep_Id;
                dbUser.Status = user.Status;

                try
                {
                    db.SaveChanges();

                }
                catch (Exception ex)
                {

                    throw ex;
                }

                return RedirectToAction("Index");
            }
            ViewBag.Dep_Id = new SelectList(db.Departments, "Dep_Id", "Dep_Nam", user.Dep_Id);
            return View(user);
        }

        // GET: Users/Delete/5
        [RoleFilter(new int[] { 1 })]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleFilter(new int[] { 1 })]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        [HttpGet]
        public ActionResult Login(string next="/")
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [GRecaptcha]
        [ValidateAntiForgeryToken]
        public ActionResult postLogin(Requests.LoginRequest request, string next = "/")
        {
           if(!ModelState.IsValid)
            {
                ViewBag.error = ModelState.First().Value.Errors.Last().ErrorMessage;
                return View();
            }
            if (!Regex.IsMatch(request.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                ViewBag.error = "Invalid Email Format !";
                return View();
            }
            //var user = db.Users.Where(a => a.Email == request.Email).FirstOrDefault();
            //if (user == null || user.Password != request.Password)
            //{
            //    ViewBag.error = "Invalid User Data !";
            //    return View();
            //}



            //Session.RemoveAll();

            //Session.Add("USER_ID", user.Usr_Id);
            var user = UserSession.Login(request.Email, request.Password,request.Remember).Result;
            if (user == null)
            {
                ViewBag.error = "Invalid User Data !";
                return View();
            }


            return Redirect(next);
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost, ActionName("ForgetPassword")]
        [ValidateAntiForgeryToken]
        public ActionResult postForgetPassword()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            UserSession.Logout();
            return Redirect("/Users/Login");
        }

    }
}
