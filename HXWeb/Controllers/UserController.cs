using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HXWeb.Models;

namespace HXWeb.Controllers
{
    public class UserController : Controller
    {
        //用户列表
        public ActionResult Index()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Users.ToList();
                ViewBag.Users = result;
            }
            return View();
        }
        //电话通知
        public ActionResult date4(string ID)
        {
            int userdateid = Convert.ToInt32(ID);
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Users.Find(userdateid);
                ViewBag.Users = result;
            }
            return View();
        }
        //预约时间
        public ActionResult date()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var a = db.Users.Find(Convert.ToInt32(Session["UserID"]));
                var b = db.Hospital.Where(o => o.Address == a.AddressID).ToList();
                ViewBag.User = a;
                ViewBag.Hospital = b;
                ViewBag.address = db.Address.Find(a.AddressID).Address1;
            }
                return View();
        }
        [HttpPost]
        public ActionResult date(int Hospital,DateTime Date)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var a=db.Users.Find(Convert.ToInt32(Session["UserID"]));
                a.Hospital = Hospital;
                a.Date = Date;
                db.SaveChanges();
                return RedirectToAction("date1", "User");
            }
        }
        //用户预约详情
        public ActionResult date3(string id)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                Users users = db.Users.Find(Convert.ToInt32(id));
                ViewBag.user123 = users;
                ViewBag.hospital = db.Hospital.ToList();
                ViewBag.address = db.Address.ToList();
            }
            return View();
        }
        //用户确认记录日期
        public ActionResult date1()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var user =db.Users.Find(Convert.ToInt32(Session["UserID"])) ;
                ViewBag.address = db.Address.Find(user.AddressID).Address1;
                ViewBag.user = user;
            }
            return View();
        }

        //用户录入列表
        public ActionResult date2()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Users.ToList();
                ViewBag.User = result;
                var result2 = db.Address.ToList();
                ViewBag.address = result2;
                var result3 = db.Hospital.ToList();
                ViewBag.hospital = result3;
            }
            return View();
        }
        [HttpPost]
        public ActionResult date2(int? address)
        {
            if (address==null)
            {
                return RedirectToAction("date2","User");
            }
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Users.Where(o=>o.AddressID==address).ToList();
                ViewBag.User = result;
                var result2 = db.Address.ToList();
                ViewBag.address = result2;
                var result3 = db.Hospital.ToList();
                ViewBag.hospital = result3;
            }
            return View();
        }
        //用户登记信息修改


        public ActionResult Eidt(string UserID)
        {
            int userID = Convert.ToInt32(UserID);
            
                using (HXDBEntities db = new HXDBEntities())
                {
                    var result = db.Users.Find(Convert.ToInt32(Session["UserID"]));
                    var result2 = db.Address.ToList();
                    ViewBag.User = result;
                    ViewBag.Address = result2;
                }
            return View();
        }
        [HttpPost]
        public ActionResult Eidt(Users user)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                try
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string aaa = e.Message;
                }
            }
            if (Session["Manager"]!=null)
            {
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("date", "User");
        }
     
        //跳转
        public string date5()
        {
            return "<script>alert('完成操作');window.location.href='date6'</script>";
        }
        //跳转首页
        public ActionResult date6()
        {
            return RedirectToAction("Index","Home");
        }
        //删除用户信息
        public ActionResult Delete(int UserID)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                try
                {
                    var result = db.Users.Find(UserID);
                    ViewBag.User = result;
                }
                catch (Exception e)
                {
                    string aaa = e.Message;
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Users User)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                try
                {
                    db.Entry(User).State = EntityState.Deleted;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string aaa = e.Message;
                }
            }
            return RedirectToAction("Index", "User");
        }
    }
}