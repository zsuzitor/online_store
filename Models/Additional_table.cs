using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace online_store.Models
{
    //отзывы
    public class Comment
    {
        public int Id { get; set; }
        public int Object_id { get; set; }
        public string Person_id{ get; set; }
        public string Text{ get; set; }
        private int? mark { get; set; }
        public int? Mark { get { return mark; } set {
                if (value == null)
                {
                    mark = null;
                    return;
                }
                    
                if (value < 0)
                    mark = 0;
                else
                {
                    if (value > 5)
                        mark = 5;
                    else
                        mark = value;
                }
                
            } }
        

        public Comment()
        {
            Id = 0;
            Object_id = 0;
            Person_id = null;
            Text = null;
            mark = null;
        }

    }
    public class Comment_view
    {
        public Comment Db { get; set; }
        public byte[] Image_user { get; set; }
        public string User_name { get; set; }
        public Comment_view()
        {
            Db = null;
            Image_user = null;
            User_name = null;
        }
        public Comment_view(Comment a)
        {
            Db = a;
            Image_user = null;
            User_name = null;
        }
    }

        //
        public class Connect_image
    {
        public int Id { get; set; }
        public string Something_id { get; set; }
        public string What_something { get; set; }//Person Object
        public byte[] Image { get; set; }
        public Connect_image()
        {
            Id = 0;
            Something_id = null;
            What_something = null;
            Image = null;
        }
    }
   
    public class Connect_basket
    {
        public int Id { get; set; }
        public int Object_id { get; set; }
        public double Price { get; set; }
        public string Person_id { get; set; }
        public Connect_basket()
        {
            Id = 0;
            Object_id = 0;
            Person_id = "";
            Price = 0;

        }
    }
    public class Purchase
    {
        public int Id { get; set; }
        public int Object_id { get; set; }
        public string Person_id { get; set; }

        public Purchase()
        {
            Id = 0;
            Object_id = 0;
            Person_id = "";

        }

    }
    public class Follow_obgect
    {
        public int Id { get; set; }
        public int Object_id { get; set; }
        public string Person_id { get; set; }

        public Follow_obgect()
        {
            Id = 0;
            Object_id = 0;
            Person_id = "";

        }

    }
    public class Person
    {
        public ApplicationUser Db { get; set; }




        public Person()
        {
            Db = null;
        }
        public Person(ApplicationUser a)
        {
            Db = a;
        }
    }

    }