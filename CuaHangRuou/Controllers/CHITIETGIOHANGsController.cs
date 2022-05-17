using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuaHangRuou.Models;

namespace CuaHangRuou.Controllers
{
    public class CHITIETGIOHANGsController : Controller
    {
        private DBRuou1 db = new DBRuou1();

        // GET: CHITIETGIOHANGs
        public ActionResult Index()
        {
            var cHITIETGIOHANGs = db.CHITIETGIOHANGs.Include(c => c.GIOHANG).Include(c => c.SANPHAM);
            return View(cHITIETGIOHANGs.ToList());
        }

        // GET: CHITIETGIOHANGs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETGIOHANG cHITIETGIOHANG = db.CHITIETGIOHANGs.Find(id);
            if (cHITIETGIOHANG == null)
            {
                return HttpNotFound();
            }
            return View(cHITIETGIOHANG);
        }

        // GET: CHITIETGIOHANGs/Create
        public ActionResult Create()
        {
            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH");
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP");
            return View();
        }

        // POST: CHITIETGIOHANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGH,MaSP,SoLuongDat")] CHITIETGIOHANG cHITIETGIOHANG)
        {
            if (ModelState.IsValid)
            {
                db.CHITIETGIOHANGs.Add(cHITIETGIOHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH", cHITIETGIOHANG.MaGH);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cHITIETGIOHANG.MaSP);
            return View(cHITIETGIOHANG);
        }

        // GET: CHITIETGIOHANGs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETGIOHANG cHITIETGIOHANG = db.CHITIETGIOHANGs.Find(id);
            if (cHITIETGIOHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH", cHITIETGIOHANG.MaGH);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cHITIETGIOHANG.MaSP);
            return View(cHITIETGIOHANG);
        }

        // POST: CHITIETGIOHANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGH,MaSP,SoLuongDat")] CHITIETGIOHANG cHITIETGIOHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHITIETGIOHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGH = new SelectList(db.GIOHANGs, "MaGH", "MaGH", cHITIETGIOHANG.MaGH);
            ViewBag.MaSP = new SelectList(db.SANPHAMs, "MaSP", "TenSP", cHITIETGIOHANG.MaSP);
            return View(cHITIETGIOHANG);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult GioHang(int? id)
        {
            
                if (id == null)
                {
                    return RedirectToAction("Login", "TAIKHOANs");
                }
                var cHITIETGIOHANG = db.CHITIETGIOHANGs.Where(x => x.GIOHANG.MaKh == id);
                return View(cHITIETGIOHANG.ToList());
        }

        public ActionResult Delete(int magh, int masp)
        {
            if (magh == null && masp == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHITIETGIOHANG cHITIETGIOHANG = db.CHITIETGIOHANGs.Where(sp => sp.MaSP == masp && sp.MaGH == magh).FirstOrDefault();

            if (cHITIETGIOHANG == null)
            {
                return HttpNotFound();
            }
            db.CHITIETGIOHANGs.Remove(cHITIETGIOHANG);
            db.SaveChanges();
            return RedirectToAction("GioHang", "CHITIETGIOHANGs", new { id = (int)Session["idKH"] });
        }

        public ActionResult Update(int idGH, int idSP, int sl)
        {
            CHITIETGIOHANG ctGioHang = new CHITIETGIOHANG();
            ctGioHang.MaGH = idGH;
            ctGioHang.MaSP = idSP;
            ctGioHang.SoLuongDat = sl;
            db.Entry(ctGioHang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GioHang", "CHITIETGIOHANGs", db.CHITIETGIOHANGs.ToList());
        }
    }
}
