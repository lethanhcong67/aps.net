using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CuaHangRuou.Models;
using PagedList;

namespace CuaHangRuou.Controllers
{
    public class SANPHAMsController : Controller
    {
        private DBRuou1 db = new DBRuou1();

        // GET: SANPHAMs
        public ActionResult Index()
        {
            var sANPHAMs = db.SANPHAMs.Include(s => s.DANHMUC);
            return View(sANPHAMs.ToList());
        }

        public ActionResult XemSanPhamTheoDanhMuc(int id, string sanphamString, int? page)
        {
            var sanphams = db.SANPHAMs.Where(p => p.MaDM.Equals(id));
            ViewBag.rs = sanphamString;
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(sanphams.OrderBy(p => p.MaSP).ToPagedList(pageNumber, pageSize));
            
        }

        public ActionResult TimKiemSanPham(string searchString, int? page)
        {
            var sanphams = db.SANPHAMs.Select(p => p);
            ViewBag.rs = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                sanphams = sanphams.Where(p => p.TenSP.Contains(searchString));
                ViewBag.count = sanphams.Count();
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(sanphams.OrderBy(p => p.MaSP).ToPagedList(pageNumber, pageSize));
        }

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

        [HttpPost]
        public ActionResult AddToCart(int masp, int so_luong)
        {
            GIOHANG gh = new GIOHANG();
            gh.MaKh = (int)Session["idKH"];
            db.GIOHANGs.Add(gh);
            CHITIETGIOHANG ctGioHang = new CHITIETGIOHANG();
            ctGioHang.MaGH = gh.MaGH;
            ctGioHang.MaSP = masp;
            ctGioHang.SoLuongDat = so_luong;
            db.CHITIETGIOHANGs.Add(ctGioHang);
            db.SaveChanges();
            return RedirectToAction("GioHang", "CHITIETGIOHANGs", new { id = (int)Session["idKH"] });
        }
        public ActionResult DatHang(List<SANPHAM> sps)
        {
            ViewBag.sl = sps.Count;
            return View(sps);
        }
        
        //[HttpPost]
        //public ActionResult DatHang()
        //{

        //    return View();
        //}

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
