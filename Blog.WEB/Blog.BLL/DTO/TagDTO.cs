using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.DTO
{
    public class TagDTO
    {
        public int ID { get; set; }

        public string Tag { get; set; }

        public virtual IList<PostDTO> Posts { get; set; }
    }
}
