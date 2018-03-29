using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Blogs
    {
        public int ID { get; set; }

        [Required]
        public string Title { get; set; }

        public string BlogerID { get; set; }
        public string BlogerEmail { get; set; }

        public virtual User Bloger { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
