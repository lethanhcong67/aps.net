using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CuaHangRuou.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ChiTietSanPham()
        {
            return View();
        }
        public ActionResult GioHang()
        {
            return View();
        }
        public ActionResult DatHang()
        {
            return View();
        }
        
    }
}