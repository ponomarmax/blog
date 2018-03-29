using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class BlogModel
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string BlogerID { get; set; }
        public string BlogerEmail { get; set; }

        public virtual UserModel Bloger { get; set; }

        public virtual ICollection<PostModel> Posts { get; set; }
    }
}
