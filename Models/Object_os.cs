using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace online_store.Models
{
    public class Object_os
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public string Color { get; set; }
        public string Composition { get; set; }//состав 
        public string Description { get; set; }
        public int Count_buy { get; set; }
        public Object_os()
        {
            Id = 0;
            Name = "";
            Type = "";
            Manufacturer = "";
            Color = "";
            Composition = "";
            Description = "";
            Count_buy = 0;
            Price = 0;
            Discount = 0;
        }
    }

    public class Object_os_for_view
    {
        public Object_os Db;
        public List<Connect_image> Images;
        public List<Comment_view> Comments;
        public Object_os_for_view(Object_os a)
        {
            Db = a;
            Images = new List<Connect_image>();
            Comments = new List<Comment_view>();
        }
    }
}



