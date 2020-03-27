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
    public class AssetsController : Controller
    {
        private InventoryEntities db = new InventoryEntities();

        // GET: Assets
        //public ActionResult Index()
        //{

        //    return View(db.Assets.ToList());
        //}

        public ActionResult Index(string sortOrder, string search)
        {
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "test" : "";
            ViewBag.Ast_IdSortParm = sortOrder == "Ast_Id" ? "Ast_Id_desc" : "Ast_Id";
            ViewBag.Ser_NumSortParm = sortOrder == "Ser_Num" ? "Ser_Num_desc" : "Ser_Num";
            ViewBag.Mod_NumSortParm = sortOrder == "Mod_Num" ? "Mod_Num_desc" : "Mod_Num";
            ViewBag.Ast_TypeSortParm = sortOrder == "Ast_Type" ? "Ast_Type_desc" : "Ast_Type";
            ViewBag.CostSortParm = sortOrder == "Cost" ? "Cost_desc" : "Cost";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewBag.Created_BySortParm = sortOrder == "Created_By" ? "Created_By_desc" : "Created_By";
            ViewBag.Updated_BySortParm = sortOrder == "Updated_By" ? "Updated_By_desc" : "Updated_By";
            ViewBag.Created_AtSortParm = sortOrder == "Created_At" ? "Created_At_desc" : "Created_At";
            ViewBag.Updated_AtSortParm = sortOrder == "Updated_At" ? "Updated_At_desc" : "Updated_At";
            ViewBag.Is_ActiveSortParm = sortOrder == "Is_Active" ? "Is_Active_desc" : "Is_Active";
            var Assets = from s in db.Assets
                         select s;
            switch (sortOrder)
            {
                case "Ast_Id_desc":
                    Assets = Assets.OrderByDescending(s => s.Ast_Id);
                    break;
                case "Ser_Num":
                    Assets = Assets.OrderBy(s => s.Ser_Num);
                    break;
                case "Ser_Num_desc":
                    Assets = Assets.OrderByDescending(s => s.Ser_Num);
                    break;
                case "Mod_Num":
                    Assets = Assets.OrderBy(s => s.Mod_Num);
                    break;
                case "Mod_Num_desc":
                    Assets = Assets.OrderByDescending(s => s.Mod_Num);
                    break;
                case "Ast_Type":
                    Assets = Assets.OrderBy(s => s.Ast_Type);
                    break;
                case "Ast_Type_desc":
                    Assets = Assets.OrderByDescending(s => s.Ast_Type);
                    break;
                case "Cost":
                    Assets = Assets.OrderBy(s => s.Cost);
                    break;
                case "Cost_desc":
                    Assets = Assets.OrderByDescending(s => s.Cost);
                    break;
                case "Date":
                    Assets = Assets.OrderBy(s => s.Date);
                    break;
                case "Date_desc":
                    Assets = Assets.OrderByDescending(s => s.Date);
                    break;
                case "Created_By":
                    Assets = Assets.OrderBy(s => s.Created_By);
                    break;
                case "Created_By_desc":
                    Assets = Assets.OrderByDescending(s => s.Created_By);
                    break;
                case "Updated_By":
                    Assets = Assets.OrderBy(s => s.Updated_By);
                    break;
                case "Updated_By_desc":
                    Assets = Assets.OrderByDescending(s => s.Updated_By);
                    break;
                case "Created_At":
                    Assets = Assets.OrderBy(s => s.Created_At);
                    break;
                case "Created_At_desc":
                    Assets = Assets.OrderByDescending(s => s.Created_At);
                    break;
                case "Updated_At":
                    Assets = Assets.OrderBy(s => s.Updated_At);
                    break;
                case "Updated_At_desc":
                    Assets = Assets.OrderByDescending(s => s.Updated_At);
                    break;
                case "Is_Active":
                    Assets = Assets.OrderBy(s => s.Is_Active);
                    break;
                case "Is_Active_desc":
                    Assets = Assets.OrderByDescending(s => s.Is_Active);
                    break;

                default:
                    Assets = Assets.OrderBy(s => s.Ast_Id);
                    break;
            }
            return View(Assets.ToList());
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
        public ActionResult Create([Bind(Include = "Ast_Id,Ser_Num,Mod_Num,Ast_Type,Cost,Date,Created_By,Updated_By,Created_At,Updated_At,Is_Active")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                db.Assets.Add(asset);
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
