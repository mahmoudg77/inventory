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
    [RoleFilter(new int[] { 1, 2 })]
    public class AssetsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Assets
        public ActionResult Index()
        {

            return View();
        }


        public ActionResult GetData()
        {
            using (InventoryEntities db = new InventoryEntities())
            {
                List<Asset> assetList = db.Assets.ToList<Asset>();
                return Json(new { data = assetList }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Assets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Assets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ast_Id,Ser_Num,Mod_Num,Ast_Type,Cost,Is_Active")] Asset asset) 
        {
            if (ModelState.IsValid)
            {
                db.Assets.Add(asset);
                asset.Date = DateTime.Now;
                asset.Created_At = DateTime.Now;
                asset.Created_By = UserSession.User.F_Name + " " + UserSession.User.L_Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(asset);
        }

        // GET: Assets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ast_Id,Ser_Num,Mod_Num,Ast_Type,Cost,Date,Created_By,Updated_By,Created_At,Updated_At,Is_Active")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asset).State = EntityState.Modified;
                asset.Updated_At = DateTime.Now;
                asset.Updated_By = UserSession.User.F_Name + " " + UserSession.User.L_Name;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(asset);
        }

        // GET: Assets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = db.Assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Asset asset = db.Assets.Find(id);
            db.Assets.Remove(asset);
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
