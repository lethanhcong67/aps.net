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
    public class TAIKHOANsController : Controller
    {
        private DBRuou db = new DBRuou();

        // GET: Admin/TAIKHOANs
        public ActionResult QuanLyTaiKhoan(string sortOrder, string searchString, string currentFilter, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var tAIKHOANs = db.TAIKHOANs.Select(p => p);

            if (!String.IsNullOrEmpty(searchString))
            {
                tAIKHOANs = tAIKHOANs.Where(p => p.TenDN.Contains(searchString));
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var tAIKHOANs = db.TAIKHOANs.Include(t => t.KHACHHANG);
            return View(tAIKHOANs.OrderBy(p => p.MaTK).ToPagedList(pageNumber, pageSize));
            //return View(tAIKHOANs.ToList());
        }

        // GET: Admin/TAIKHOANs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // GET: Admin/TAIKHOANs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKh = new SelectList(db.KHACHHANGs, "MaKh", "HoTen", tAIKHOAN.MaKh);
            return View(tAIKHOAN);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTK,TenDN,MatKhau,PhanQuyen,BiKhoa,MaKh")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAIKHOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("QuanLyTaiKhoan");
            }
            else
            {
                return View();
            }
        }

        // GET: Admin/TAIKHOANs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: Admin/TAIKHOANs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            db.TAIKHOANs.Remove(tAIKHOAN);
            db.SaveChanges();
            return RedirectToAction("QuanLyTaiKhoan");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DangNhap(string tendn, string matkhau)
        {
            if (!String.IsNullOrWhiteSpace(tendn) && !String.IsNullOrWhiteSpace(matkhau))
            {
                var user = db.TAIKHOANs.Where(u => u.TenDN.Equals(tendn) && u.MatKhau.Equals(matkhau)).ToList();
                bool flag = false;
                if (user.FirstOrDefault() != null)
                {
                    flag = user.FirstOrDefault().PhanQuyen == "admin" ? true : false;
                }

                if (user.Count() > 0 && flag && (bool)!user.FirstOrDefault().BiKhoa)
                {
                    Session["TenDN"] = user.FirstOrDefault().TenDN;
                    Session["MatKhau"] = user.FirstOrDefault().MatKhau;
                    Session["idAcc"] = user.FirstOrDefault().MaTK;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.error = "Đăng nhập không thành công!";
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
        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("DangNhap", "TAIKHOANs");
        }
    }
}
