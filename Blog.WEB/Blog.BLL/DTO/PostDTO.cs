using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class PostDTO
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int BlogID { get; set; }
        public virtual BlogDTO Blog { get; set; }

        public virtual ICollection<CommentDTO> Comments { get; set; }
        public virtual ICollection<TagDTO> Tags { get; set; }
    }
}
