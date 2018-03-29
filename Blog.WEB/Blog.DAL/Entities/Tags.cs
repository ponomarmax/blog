using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Entities
{
    public class Tags
    {
        public int ID { get; set; }

        public string Tag { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
