using Blog.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface ICommentService
    {
        void Create(CommentDTO post);
        void Modify(CommentDTO post);
        void Delete(int id);
        IEnumerable<CommentDTO> GetAll(int postId);
        CommentDTO Get(int? id);
        void Dispose();
    }
}
