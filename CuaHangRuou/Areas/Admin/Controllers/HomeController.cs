using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangRuou.Areas.Admin.Models;

namespace CuaHangRuou.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        DBRuou db = new DBRuou();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var bang = db.DONHANGs.Join(db.CHITIETGIOHANGs,
                dh => dh.MaGH,
                ct => ct.MaGH,
                (dh, ct) => new
                {
                    TinhTrang = dh.TinhTrang,
                    MaSP = ct.MaSP,
                    SoLuongDat = ct.SoLuongDat
                });
            ViewBag.dt = bang.Join(db.SANPHAMs,
                b=>b.MaSP,
                sp=>sp.MaSP,
                (b, sp) =>new
                {
                    soLuong=b.SoLuongDat,
                    gia=sp.Gia
                }).Sum(p=>(p.soLuong*p.gia));
            ViewBag.soluong = db.SANPHAMs.Sum(x => x.SoLuong);
            ViewBag.daban = bang.Sum(p=>p.SoLuongDat);
            ViewBag.kh = db.KHACHHANGs.Count();
            return View();
        }
    }
}