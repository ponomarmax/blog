using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class BlogDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string BlogerID { get; set; }
        public string BlogerEmail { get; set; }

        public virtual UserDTO Bloger { get; set; }

        public virtual ICollection<PostDTO> Posts { get; set; }
    }
}
