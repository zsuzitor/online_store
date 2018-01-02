using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using online_store.Models;

namespace online_store.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //var check_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List_objects()
        {
            List<Object_os_for_view> res = new List<Object_os_for_view>();


            return PartialView(res);
        }
        public ActionResult Object_view(int id)
        {
            return View();
        }


        public ActionResult Object_follow(int id)
        {
            ViewBag.Id = id;
            return PartialView();
        }
        public ActionResult Object_add_basket(int id)
        {
            ViewBag.Id = id;
            return PartialView();
        }



















        //-----------------------------------
        public ActionResult Main_header()
        {
            
            ViewBag.List_class_for_header=new string[] {"what1", "what2", "what3", "what4", "what5", "what6", "what7", "what7" };

            return PartialView();
        }
    }
}