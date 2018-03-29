using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class CommentDTO
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        public string Body { get; set; }

        public string UserID { get; set; }
        public virtual UserDTO User { get; set; }

        public int PostID { get; set; }
        public virtual PostDTO Post { get; set; }
    }
}
