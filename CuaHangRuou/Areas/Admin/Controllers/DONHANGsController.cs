using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuaHangRuou.Areas.Admin.Models;
using PagedList;

namespace CuaHangRuou.Areas.Admin.Controllers
{
    public class DONHANGsController : Controller
    {
        private DBRuou db = new DBRuou();

        // GET: Admin/DONHANGs
        public ActionResult QuanLyDonHang(int? page)
        {
            var dONHANGs = db.DONHANGs.Include(d => d.GIOHANG.CHITIETGIOHANGs).OrderBy(p=>p.MaDH);
                 
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(dONHANGs.ToPagedList(pageNumber,pageSize));
        }
        [HttpPost]
        public ActionResult IndexForm(int maGH, string tinhTrang, int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ArrayList listTT = new ArrayList() {
                "Đã giao",
                "Đang giao",
                "Đã hủy",
                "Không nhận hàng"
            };
            var donHang = db.DONHANGs;
            if (Convert.ToInt32(maGH) > 0)
            {
                int indexOfTT = listTT.IndexOf(tinhTrang);
                if (indexOfTT > -1 && indexOfTT < listTT.Count - 1)
                {
                    donHang.Find(maGH).TinhTrang = (string)listTT[indexOfTT + 1];
                }
                else
                {
                    donHang.Find(maGH).TinhTrang = (string)listTT[0];
                }
                db.SaveChanges();
                return View("QuanLyDonHang", donHang.OrderBy(p => p.MaDH).ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View("QuanLyDonHang");
            }

        }
        // GET: Admin/DONHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH");
            return View();
        }

        // POST: Admin/DONHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,GhiChu,GiaoHang,NgayDat,TinhTrang,MaGH")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.DONHANGs.Add(dONHANG);
                db.SaveChanges();
                return RedirectToAction("QuanLyDonHang");
            }

            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH", dONHANG.MaGH);
            return View(dONHANG);
        }

        // GET: Admin/DONHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH", dONHANG.MaGH);
            return View("QuanLyDonHang");
        }

        // POST: Admin/DONHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,GhiChu,GiaoHang,NgayDat,TinhTrang,MaGH")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyDonHang");
            }
            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH", dONHANG.MaGH);
            return View("QuanLyDonHang");
        }

        // GET: Admin/DONHANGs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // POST: Admin/DONHANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DONHANG dONHANG = db.DONHANGs.Find(id);
            db.DONHANGs.Remove(dONHANG);
            db.SaveChanges();
            return RedirectToAction("QuanLyDonHang");
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
