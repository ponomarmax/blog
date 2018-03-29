using Blog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IPostService
    {
        void Create(PostDTO post, string tags);
        void Modify(PostDTO post);
        void Delete(int id);
        IEnumerable<PostDTO> GetByBlogId(int id);
        IEnumerable<PostDTO> GetByTagId(int id);
        PostDTO Get(int? id);
        void Dispose();
    }
}

