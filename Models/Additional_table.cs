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
        public int? Mark { get; set; }

        public Comment()
        {
            Id = 0;
            Object_id = 0;
            Person_id = null;
            Text = null;
            Mark = null;
        }

    }
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
        public string Person_id { get; set; }
        public Connect_basket()
        {
            Id = 0;
            Object_id = 0;
            Person_id = "";

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


}