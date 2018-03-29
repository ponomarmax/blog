using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Posts
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public int BlogID { get; set; }
        public virtual Blogs Blog { get; set; }

        public virtual ICollection<Comments> Comments { get; set; }
        public virtual IList<Tags> Tags { get; set; }
    }
}
