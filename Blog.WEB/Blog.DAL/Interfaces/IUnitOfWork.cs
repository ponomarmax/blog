using Blog.DAL.Entities;
using Blog.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Identity;

namespace Blog.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        PostRepository postRepository { get; }
        BlogRepository  blogRepository { get; }
        CommentRepository commentRepository { get; }
        TagRepository tagRepository { get; }

        ApplicationRoleManager roleRepository { get; }
        ApplicationUserManager userRepository { get; }

        Task SaveAsync();
    }
}
