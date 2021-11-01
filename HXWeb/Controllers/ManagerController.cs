using HXWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HXWeb.Controllers
{
    public class ManagerController : Controller
    {
        //管理员列表
        public ActionResult Index()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Manager.ToList();
                ViewBag.Manager = result;
            }
            return View();
        }

        //新增管理员
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Manager manager)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                try
                {
                    var result = db.Manager.Add(manager);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Index");
        }

        //修改管理员信息
        [HttpPost]
        public ActionResult Eidt(Manager manager)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                try
                {
                    db.Entry(manager).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string aaa = e.Message;
                }
            }
            return RedirectToAction("Index", "Manager");
        }

        public ActionResult Eidt(int? managerID)
        {
            if (managerID == null)
            {
                return RedirectToAction("Index", "Manager");
            }
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Manager.Find(managerID);
                ViewBag.Manager = result;
            }
            return View();
        }

        //管理员信息详情
        public ActionResult Detial(int managerID)
        {
            if (managerID == 0)
            {
                return RedirectToAction("Index", "Manager");
            }
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Manager.Find(managerID);
                ViewBag.Manager = result;
            }
            return View();
        }

        //接种时间管理
        public ActionResult date3(int ID)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Hospital.Find(ID);
                ViewBag.hospital = result;
                ViewBag.ID = result.ID;
            }
            return View();
        }
        [HttpPost]
        public void date3(int ID,DateTime StartDate,DateTime EndDate)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var result = db.Hospital.Find(ID);
                result.StartDate = StartDate;
                result.EndDate = EndDate;
                int rows= db.SaveChanges();
                if (rows>0)
                {
                    Response.Write("<script> alert('变更成功'); window.location.href = 'date2' </script>");
                }
                else
                {
                    Response.Write("<script> alert('变更失败'); window.location.href = 'date2' </script>");
                }
            }
            
        }
        //新建接种时间
        public ActionResult date()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var hospital = db.Hospital.Where(o => o.StartDate == null).ToList();
                ViewBag.hospital = hospital;
            }
            return View();
        }
        [HttpPost]
        public void date(int ID,DateTime StartDate ,DateTime EndDate)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                try
                {
                    if (StartDate>EndDate)
                    {
                        Response.Write("<script>alert('结束时间必须大于开始时间');window.location.href='date'</script>");
                    }
                    else
                    {
                        var result = db.Hospital.Find(ID);
                        result.StartDate = StartDate;
                        result.EndDate = EndDate;
                        int rows= db.SaveChanges();
                        if (rows>0)
                        {
                            Response.Write("<script>alert('发布成功');window.location.href='date'</script>");
                        }
                    }
                   
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        }
        //编辑接种时间
        public ActionResult date2()
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                var a = db.Hospital.Where(o => o.StartDate != null).ToList();
                ViewBag.a = a;
            }
            return View();
        }
        //删除管理员
        public void Delete(int? managerID)
        {
            using (HXDBEntities db = new HXDBEntities())
            {
                try
                {
                    var result = db.Manager.Find(managerID);
                    db.Manager.Remove(result);
                    int rows= db.SaveChanges();
                    if (rows>0)
                    {
                        Response.Write("<script>alert('删除成功');window.location.href='Index'</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('删除失败');window.location.href='Index'</script>");
                    }
                }
                catch (Exception e)
                {
                    string aaa = e.Message;
                }
            }
            
        }
    }
}