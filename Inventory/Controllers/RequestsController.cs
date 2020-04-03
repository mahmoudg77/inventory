using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Inventory.Models;

namespace Inventory.Controllers
{
    public class RequestsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Asset).Include(r => r.User);
            return View(requests.ToList());
        }

        public ActionResult GetData()
        {
            using (InventoryEntities db = new InventoryEntities())
            {
                List<Request> ReqList = db.Requests.Include(r=>r.Asset).Include(a=>a.User).ToList();
                return Json(new { data = ReqList }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.Ast_Id = new SelectList(db.Assets, "Ast_Id", "Ser_Num");
            ViewBag.Usr_Id = new SelectList(db.Users, "Usr_Id", "F_Name");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Req_Id,Usr_Id,Ast_Id,Tra_Id,Created_By,Updated_By,Created_At,Updated_At,Priority")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Ast_Id = new SelectList(db.Assets, "Ast_Id", "Ser_Num", request.Ast_Id);
            ViewBag.Usr_Id = new SelectList(db.Users, "Usr_Id", "F_Name", request.Usr_Id);
            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ast_Id = new SelectList(db.Assets, "Ast_Id", "Ser_Num", request.Ast_Id);
            ViewBag.Usr_Id = new SelectList(db.Users, "Usr_Id", "F_Name", request.Usr_Id);
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Req_Id,Usr_Id,Ast_Id,Tra_Id,Created_By,Updated_By,Created_At,Updated_At,Priority")] Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ast_Id = new SelectList(db.Assets, "Ast_Id", "Ser_Num", request.Ast_Id);
            ViewBag.Usr_Id = new SelectList(db.Users, "Usr_Id", "F_Name", request.Usr_Id);
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.Requests.Find(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Request request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
