using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Comments
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        public string Body { get; set; }

        public string UserID { get; set; }
        public virtual User User { get; set; }

        public int PostID { get; set; }
        public virtual Posts Post { get; set; }

    }
}
