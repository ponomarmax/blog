using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class PostModel
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(256)]
        public string Title { get; set; }

        public string ShortBody { get; set; }
        [Required]
        public string Body { get; set; }

        public int BlogID { get; set; }
        public virtual BlogModel Blog { get; set; }

        public virtual ICollection<CommentModel> Comments { get; set; }
        public ICollection<TagModel> Tags { get; set; }
    }
}
