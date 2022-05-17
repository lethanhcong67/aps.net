using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangRuou.Models;
namespace CuaHangRuou.Controllers
{
    public class HomeController : Controller
    {
        DBRuou1 db = new DBRuou1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult _Navbar()
        {
            ViewBag.catelogyNum = db.DANHMUCs.ToList().Count();
            return PartialView(db.DANHMUCs.ToList().Take(5));
        }
        public PartialViewResult _Other()
        {
            
            return PartialView(db.DANHMUCs.ToList().Skip(5));
        }
    }
}