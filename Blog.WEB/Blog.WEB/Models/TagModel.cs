using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class TagModel
    {
        public int ID { get; set; }

        public string Tag { get; set; }

        public virtual ICollection<PostModel> Posts { get; set; }
    }
}