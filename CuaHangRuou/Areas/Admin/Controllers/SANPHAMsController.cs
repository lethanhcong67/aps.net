using System;
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
    public class SANPHAMsController : Controller
    {
        private DBRuou db = new DBRuou();

        // GET: Admin/SANPHAMs
        public ActionResult QuanLySanPham(int? page)
        {
            var supplies = db.SANPHAMs.Select(s => s);

            //Sắp xếp
            supplies = supplies.OrderBy(s => s.MaSP);

            //Kích thước trang
            int pageSize = 5;
            int pageNumber = (page ?? 1); //nếu page = null thì trả về 1


            return View(supplies.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SANPHAMs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Create
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DANHMUCs, "MaDM", "TenDM", db.DANHMUCs);
            return View();
        }

        // POST: Admin/SANPHAMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,Anh,Gia,SoLuong,LoaiSP,ThuongHieu,XuatXu,DungTich,NongDo,TuoiRuou,BoSuuTap,MoTa,MaDM")] SANPHAM sANPHAM)
        {

            if (ModelState.IsValid)
            {
                sANPHAM.Anh = "";
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UpLoadPath = Server.MapPath("~/wwwroot/anh/" + FileName);
                    f.SaveAs(UpLoadPath);
                    sANPHAM.Anh = FileName;
                }
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("QuanLySanPham");
            }
            ViewBag.MaDM = new SelectList(db.DANHMUCs, "MaDM", "TenDM", sANPHAM.MaDM);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDM = new SelectList(db.DANHMUCs, "MaDM", "TenDM", sANPHAM.MaDM);
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,Anh,Gia,SoLuong,LoaiSP,ThuongHieu,XuatXu,DungTich,NongDo,TuoiRuou,BoSuuTap,MoTa,MaDM")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["ImageFile"];
                if (f != null && f.ContentLength > 0)
                {
                    string FileName = System.IO.Path.GetFileName(f.FileName);
                    string UpLoadPath = Server.MapPath("~/wwwroot/anh/" + FileName);
                    f.SaveAs(UpLoadPath);
                    sANPHAM.Anh = FileName;
                }
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLySanPham");
            }
            ViewBag.MaDM = new SelectList(db.DANHMUCs, "MaDM", "TenDM", sANPHAM.MaDM);
            return View(sANPHAM);
        }

        // GET: Admin/SANPHAMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/SANPHAMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {


            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            try
            {
                db.SANPHAMs.Remove(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("QuanLySanPham");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Không xoá được bản ghi này! " + ex.Message;
                return View("Delete", sANPHAM);
            }

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
