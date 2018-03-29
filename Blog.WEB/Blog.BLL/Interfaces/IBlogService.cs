using Blog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IBlogService
    {
        void Create(BlogDTO blog);
        void Modify(BlogDTO blog);
        IEnumerable<BlogDTO> GetByName(string name);
        IEnumerable<BlogDTO> GetAll();
        BlogDTO Get(int? id);
        void Dispose();
    }
}
