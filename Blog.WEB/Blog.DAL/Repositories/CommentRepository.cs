using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class CommentRepository : IRepository<Comments>
    {
        private ApplicationContext applicationContext;

        public CommentRepository(ApplicationContext db)
        {
            applicationContext = db;
        }
        public void Create(Comments comment)
        {
            applicationContext.Comments.Add(comment);
            applicationContext.SaveChanges();
        }

        public void Delete(int id)
        {
            applicationContext.Comments.Remove(Get(id));
            applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext.Dispose();
        }
        public Comments Get(int id)
        {
            return applicationContext.Comments.Where(comment => comment.ID == id).FirstOrDefault();
        }

        public IEnumerable<Comments> GetAll(int postId)
        {
            return applicationContext.Comments.Where(comment => comment.PostID==postId);
        }

        public void Modify(Comments item)
        {
            throw new NotImplementedException();
        }
    }
}
