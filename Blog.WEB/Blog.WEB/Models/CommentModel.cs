using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class CommentModel
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        public string Body { get; set; }

        public string UserID { get; set; }
        public virtual UserModel User { get; set; }

        public int PostID { get; set; }
        public virtual PostModel Post { get; set; }
    }
}