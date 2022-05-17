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
    public class TAIKHOANsController : Controller
    {
        private DBRuou1 db = new DBRuou1();

        // GET: TAIKHOANs
        public ActionResult Index()
        {
            var tAIKHOANs = db.TAIKHOANs.Include(t => t.KHACHHANG);
            return View(tAIKHOANs.ToList());
        }


        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "MaTK,TenDN,MatKhau,PhanQuyen,BiKhoa,MaKh,KHACHHANG")] TAIKHOAN tAIKHOAN, string HoTen, string SDT, string DiaChi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    KHACHHANG kh = new KHACHHANG { 
                        HoTen=tAIKHOAN.KHACHHANG.HoTen,
                        DiaChi=tAIKHOAN.KHACHHANG.DiaChi,
                        SDT=tAIKHOAN.KHACHHANG.SDT
                    };
                    new KHACHHANGsController().Create2(kh);


                    tAIKHOAN.MaKh = db.KHACHHANGs.ToList().LastOrDefault().MaKh;
                    tAIKHOAN.BiKhoa = false;
                    tAIKHOAN.PhanQuyen = "customer";
                    db.TAIKHOANs.Add(tAIKHOAN);

                    db.SaveChanges();
                    return RedirectToAction("Login", "taikhoans");
                }
                ViewBag.err = "Lỗi đăng ký, vui lòng thử lại sau";
                return View();
            }
            catch (Exception)
            {

                ViewBag.err = "Lỗi đăng ký, vui lòng thử lại sau";
                return View();
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


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(string tendn, string matkhau)
        {
            if (!String.IsNullOrWhiteSpace(tendn) && !String.IsNullOrWhiteSpace(matkhau))
            {
                var user = db.TAIKHOANs.Where(u => u.TenDN.Equals(tendn) && u.MatKhau.Equals(matkhau)).ToList();
                if (user.Count() > 0 && (bool)!user.FirstOrDefault().BiKhoa)
                {
                    Session["TenDN"] = user.FirstOrDefault().TenDN;
                    Session["MatKhau"] = user.FirstOrDefault().MatKhau;
                    Session["idAcc"] = user.FirstOrDefault().MaTK;
                    Session["idKH"] = user.FirstOrDefault().MaKh;
                    Session["KHACHHANG"] = user.FirstOrDefault().KHACHHANG;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công, vui lòng kiểm tra lại tên đăng nhập và mật khẩu!";
                }
            }
            else
            {
                if (String.IsNullOrWhiteSpace(tendn))
                {
                    ViewBag.userErrorMsg = "Tên đăng nhập không được để trống!";
                }
                if (String.IsNullOrWhiteSpace(matkhau))
                {
                    ViewBag.passwordErrorMsg = "Mật khẩu không được để trống!";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "TAIKHOANs");
        }
        public ActionResult DoiMatKhau()
        {
            if (Session["idAcc"] != null)
            {
                int id = Convert.ToInt32(Session["idAcc"].ToString());
                var user = db.TAIKHOANs.Where(u => u.MaTK.Equals(id)).ToList();
                return View(user.FirstOrDefault());
            }
            else
            {
                return RedirectToAction("login", "taikhoans", new { area = "" });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoiMatKhau([Bind(Include = "MaTK,TenDN,MatKhau,PhanQuyen,BiKhoa,MaKh")] TAIKHOAN tAIKHOAN)
        {
            string old = Request.Form["old"];
            string new1 = Request.Form["new1"];
            string new2 = Request.Form["MatKhau"];
            //Xử lý nhập trống
            if (String.IsNullOrWhiteSpace(old))
            {
                ViewBag.oldErrorMsg = "Mật khẩu cũ không được để trống!";
            }
            if (String.IsNullOrWhiteSpace(new1))
            {
                ViewBag.new1ErrorMsg = "Mật khẩu mới không được để trống!";
            }
            if (String.IsNullOrWhiteSpace(new2))
            {
                ViewBag.MatKhauErrorMsg = "Bạn phải nhập lại mật khẩu mới!";
            }

            string currentPass = (string)Session["MatKhau"];

            bool flag = false;
            if (old.Equals(currentPass) && new1.Equals(new2))
            {
                flag = true;
            }
            else
            {
                if (!old.Equals(currentPass))
                {
                    ViewBag.oldErrorMsg = "Mật khẩu cũ không chính xác!";
                }
                if (!new1.Equals(new2))
                {
                    ViewBag.MatKhauErrorMsg = "Nhập lại mật khẩu mới không chính xác!";
                }
            }
            if (flag && ModelState.IsValid)
            {
                tAIKHOAN.MatKhau = new2;
                db.Entry(tAIKHOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            //ViewBag.err = "Đã xảy ra lỗi khi đổi mật khẩu, vui lòng thử lại sau";
            return View(tAIKHOAN);
        }
        public ActionResult SuaTaiKhoan()
        {
            int id;
            if (Session["idAcc"] != null)
            {
                id = (int)Session["idAcc"];

                TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
                if (tAIKHOAN == null)
                {
                    return HttpNotFound();
                }
                ViewBag.MaKh = new SelectList(db.KHACHHANGs, "MaKh", "HoTen", tAIKHOAN.MaKh);
                return View(tAIKHOAN);
            }
            else
            {
                return RedirectToAction("login", "taikhoans");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaTaiKhoan([Bind(Include = "MaTK,TenDN,MatKhau,PhanQuyen,BiKhoa,MaKh,KHACHHANG")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                KHACHHANG k = new KHACHHANG
                {
                    MaKh = tAIKHOAN.MaKh,
                    HoTen = tAIKHOAN.KHACHHANG.HoTen,
                    SDT = tAIKHOAN.KHACHHANG.SDT,
                    DiaChi = tAIKHOAN.KHACHHANG.DiaChi
                };
                new KHACHHANGsController().Edit2(k);
                tAIKHOAN.KHACHHANG = null;
                db.Entry(tAIKHOAN).State = EntityState.Modified;
                db.SaveChanges();
                Session["TenDN"] = tAIKHOAN.TenDN;
                return RedirectToAction("Index","home");
            }
            ViewBag.MaKh = new SelectList(db.KHACHHANGs, "MaKh", "HoTen", tAIKHOAN.MaKh);
            return View(tAIKHOAN);
        }
    }
}

