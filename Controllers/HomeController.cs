using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using online_store.Models;
using System.IO;

namespace online_store.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //var check_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
        //[Authorize(Roles="admin")] [Authorize]


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List_objects()
        {
            List<Object_os_for_view> res = new List<Object_os_for_view>();
            var lst=db.Objects.ToList();
            foreach(var i in lst)
            {
                var tmp = new Object_os_for_view(i);
                tmp.Images.AddRange(db.Images.Where(x1=>x1.Something_id==i.Id.ToString()&&x1.What_something=="Object"));
                

                res.Add(tmp);
            }
            return PartialView(res);
        }
        public ActionResult List_objects_type()
        {


            return View();
        }
            public ActionResult Object_view(int id)
        {
            var check_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var not_res=db.Objects.FirstOrDefault(x1 => x1.Id == id);
            Object_os_for_view res = new Object_os_for_view(not_res);
            var img =db.Images.Where(x1=>x1.Something_id==id.ToString()&&x1.What_something== "Object");
            res.Images = img.ToList();
            var com = db.Comments.Where(x1 => x1.Object_id == id).ToList();
            var com_person = com.FirstOrDefault(x1=>x1.Person_id== check_id);
            if (com_person == null)
                ViewBag.Can_commented = true;
            else
            {
                if(string.IsNullOrEmpty(com_person.Text))
                    ViewBag.Can_commented = true;
                else
                    ViewBag.Can_commented = false;
            }
               
            foreach (var i in com)
            {
                var user = db.Users.First(x1 => x1.Id == i.Person_id);
                var tmp = new Comment_view(i) { Image_user=user.Image, User_name=user.Name };
                
                res.Comments.Add(tmp);
            }
           


            return View(res);
        }
        public ActionResult Add_comment(int id_object,string text,int mark)
        {
            var check_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var new_comm = new Comment() { Object_id= id_object, Person_id=check_id, Text=text };
            if (mark > 0)
            
                new_comm.Mark = mark;
            
            else
                new_comm.Mark = null;
            db.Comments.Add(new_comm);
            db.SaveChanges();

            return RedirectToAction("Object_view", "Home",new {id=id_object });

        }
            public ActionResult Add_mark_for_object(int id,string num="")
        {
            ViewBag.Id = id;
            ViewBag.Num = num;
            var marks=db.Comments.Where(x1 => x1.Object_id == id && x1.Mark != null).ToList();
            int mark = 0;
            if (marks.Count > 0)
            {
                mark = (int)(marks.Sum(x1 => x1.Mark) / marks.Count);
            }
             
            ViewBag.Mark = mark;
            return PartialView();
        }
        //[Authorize]
        public ActionResult Change_mark_for_object(int id,int num,string num_block_for_list="")
        {
            var check_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var marks = db.Comments.FirstOrDefault(x1 => x1.Object_id == id &&x1.Person_id==check_id);
            if (marks == null)
            {
                db.Comments.Add(new Comment() { Object_id = id, Person_id = check_id, Mark = num });
                db.SaveChanges();
            }
            else
            {
                //var mm=db.Comments.FirstOrDefault(x1 => x1.Object_id == id  && x1.Person_id == check_id);
                //if (mm != null)
                //{
                marks.Mark = num;
                    db.SaveChanges();
                //}
            }
            //white_star.png
            return RedirectToAction("Add_mark_for_object","Home",new {id=id,num= num_block_for_list}); 
        }
        //[Authorize]
        public ActionResult Personal_record(string id)
        {
            id=string.IsNullOrEmpty(id) ? System.Web.HttpContext.Current.User.Identity.GetUserId() : id;
            var not_res = db.Users.First(x1 => x1.Id == id);
            var res = new Person(not_res);

            return View(res);
        }
            //[Authorize]
            public ActionResult Object_follow(int id,bool? click, string num_block_for_list="")
        {

            var check_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ViewBag.Id = id;
            ViewBag.Follow = false;
            ViewBag.Num = num_block_for_list;
            if (!string.IsNullOrEmpty(check_id))
            {
                var foll = db.Follow_obgects.FirstOrDefault(x1 => x1.Object_id == id && x1.Person_id == check_id);
                if (foll != null)
                {
                    ViewBag.Follow = true;
                    
                }
                if (click==true)
                {
                    if (ViewBag.Follow == true)
                    {
                        db.Follow_obgects.Remove(foll);
                    }
                    else
                    {
                        db.Follow_obgects.Add(new Follow_obgect() { Object_id=id, Person_id=check_id });
                    }
                    db.SaveChanges();
                    ViewBag.Follow = !ViewBag.Follow;
                }
            }
            


            return PartialView();
        }
        //[Authorize]
        public ActionResult Object_add_basket(int id, bool? click, string num_block_for_list="")
        {
            ViewBag.Id = id;
            ViewBag.Num = num_block_for_list;
            var check_id = System.Web.HttpContext.Current.User.Identity.GetUserId();
            ViewBag.InBasket = false;
            if (!string.IsNullOrEmpty(check_id))
            {
                var bask = db.Baskets.FirstOrDefault(x1 => x1.Object_id == id && x1.Person_id == check_id);
                if (bask != null)
                {
                    ViewBag.InBasket = true;

                }
                if (click==true)
                {
                    if (ViewBag.InBasket == true)
                    {
                        db.Baskets.Remove(bask);
                    }
                    else
                    {
                        db.Baskets.Add(new Connect_basket() { Object_id = id, Person_id = check_id });
                    }
                    db.SaveChanges();
                    ViewBag.InBasket = !ViewBag.InBasket;
                }
            }



            return PartialView();
        }
        //[Authorize(Roles="admin")]
        public ActionResult Delete_object(int id)
        {
            db.Objects.Remove(db.Objects.First(x1=>x1.Id==id));
            db.Comments.RemoveRange(db.Comments.Where(x1=>x1.Object_id==id));

            return RedirectToAction("Index","Home",new { });
        }
            //[Authorize(Roles="admin")]
            public ActionResult Add_object()
        {
            Object_os res = new Object_os();

            return View(res);
        }
        //[Authorize(Roles="admin")]
        [HttpPost]
        public ActionResult Add_object(Object_os a)
        {
            //проверки и тд

            db.Objects.Add(a);
            db.SaveChanges();
            ViewBag.Id = a.Id;
            return RedirectToAction("Work_with_images_object", new {id=a.Id });
            
            //return View();
        }
        //[Authorize(Roles="admin")]
        public ActionResult Work_with_images_object(int id)
        {
            ViewBag.Id = id;
            var imgs=db.Images.Where(x1=>x1.What_something=="Object"&&x1.Something_id==id.ToString());
            ViewBag.Images = imgs.ToList();
            return View();
        }
        //[Authorize(Roles="admin")]
        public ActionResult Delete_img_block(int id)
        {
            db.Images.Remove(db.Images.First(x1=>x1.Id==id));
            db.SaveChanges();
            return PartialView();
        }
        //[Authorize(Roles="admin")]
        public ActionResult Add_new_image(HttpPostedFileBase[] uploadImage,int id)
        {
            var imgs = Get_photo_post(uploadImage);
            foreach(var i in imgs)
            {
                db.Images.Add(new Connect_image() { Something_id = id.ToString(), What_something = "Object", Image =i });
                db.SaveChanges();
            }
           

            return RedirectToAction("Object_view","Home",new {id= id });
        }















            //-----------------------------------
            public ActionResult Main_header()
        {
            
            ViewBag.List_class_for_header=new string[] {"what1", "what2", "what3", "what4", "what5", "what6", "what7", "what7" };

            return PartialView();
        }















        public List<byte[]> Get_photo_post(HttpPostedFileBase[] uploadImage)
        {

            /* сохранение картинок как файл ...
              HttpPostedFileBase image = Request.Files["fileInput"];
            
            if (image != null && image.ContentLength > 0 && !string.IsNullOrEmpty(image.FileName))
            {
                string fileName = image.FileName;
                image.SaveAs(Path.Combine(Server.MapPath("Images"), fileName));
            }
             
             * */
            List<byte[]> res = new List<byte[]>();
            if (uploadImage != null)
            {

                foreach (var i in uploadImage)
                {
                    try
                    {
                        byte[] imageData = null;
                        // считываем переданный файл в массив байтов
                        using (var binaryReader = new BinaryReader(i.InputStream))
                        {
                            imageData = binaryReader.ReadBytes(i.ContentLength);
                        }
                        // установка массива байтов
                        res.Add(imageData);

                    }
                    catch
                    {

                    }



                }

            }


            return res;
        }
    }
}