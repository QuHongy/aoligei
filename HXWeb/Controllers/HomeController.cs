using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HXWeb.Models;

namespace ExamWeb.Controllers
{
    public class HomeController : Controller
    {
        //首页以及退出登录功能
        public ActionResult Index()
        {
            //退出登录
            string url = Request.RawUrl;
            if (url.Contains("?exit"))
            {
                Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        //管理员登录
        [HttpPost]
        public ActionResult LoginManager(Manager manager, string Login_Code)
        {
            //模型验证
            if (!ModelState.IsValid)
            {
                return View();
            }

            //验证用户验证码
            string code = Session["sn"].ToString().ToLower();
            if (Login_Code.ToLower() == code)
            {
                using (HXDBEntities db = new HXDBEntities())
                {
                    var result = db.Manager.Where(t => t.ManagerLoginName == manager.ManagerLoginName && t.ManagerPwd == manager.ManagerPwd).ToList();
                    if (result.Count == 1)
                    {
                        Session.Clear();
                        Session["Manager"] = result[0].ManagerName;// 保持管理员登录状态
                        return RedirectToAction("Index");
                    }
                }
            }
            return Content("<script>alert('用户名或密码或验证码有误');location.href='/Home/LoginManager'</script>");
        }
        public ActionResult LoginManager()
        {
            return View();
        }

        //用户自助取号
        public ActionResult LoginUser()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                Session["User"]="用户取号";
                var a = new Users();
                db.SaveChanges();
                Session["UserID"] = a.UserID;
                Users u = new Users();
                u.UserSex = 1;
                db.Users.Add(u);
                db.SaveChanges();
                Session["UserID"] = u.UserID;
                return View(); ;
            }
        }

    }
}