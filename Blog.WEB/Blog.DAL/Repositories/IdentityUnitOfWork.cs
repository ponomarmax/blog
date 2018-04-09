using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.DAL.Identity;

namespace Blog.DAL.Repositories
{

    public class IdentityUnitOfWork : IUnitOfWork
    {
        UserStore<User> store;
        public IdentityUnitOfWork(string connectionString = "Blog")
        {
            db = new ApplicationContext(connectionString);
            blogRepository = new BlogRepository(db);
            postRepository = new PostRepository(db);
            commentRepository = new CommentRepository(db);
            roleRepository = new ApplicationRoleManager(new RoleStore<Role>(db));
            tagRepository = new TagRepository(db);
            store = new UserStore<User>(db);
            userRepository = new ApplicationUserManager(store);
        }
        //ApplicationUserManager user;
        public PostRepository postRepository { get; }
        public BlogRepository blogRepository { get; }
        public CommentRepository commentRepository { get; }
        public TagRepository tagRepository { get; }

        public ApplicationRoleManager roleRepository { get; }
        public ApplicationUserManager userRepository { get; }
        public ApplicationContext db { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    postRepository.Dispose();
                    commentRepository.Dispose();
                    blogRepository.Dispose();
                    tagRepository.Dispose();
                    userRepository.Dispose();
                    roleRepository.Dispose();
                }
                this.disposed = true;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Save()
        {
            store.Context.SaveChanges();
            db.SaveChanges();
        }
    }
}
