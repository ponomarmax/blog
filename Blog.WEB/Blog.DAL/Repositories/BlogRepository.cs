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
    public class BlogRepository
    {
        private ApplicationContext applicationContext;

        public BlogRepository(ApplicationContext db)
        {
            applicationContext = db;
        }

        public void Create(Blogs blog)
        {
            applicationContext.Blogs.Add(blog);
            applicationContext.SaveChanges();
        }

        public void Delete(int id)
        {
            applicationContext.Blogs.Remove(Get(id));
            applicationContext.SaveChanges();
        }

        public void Dispose()
        {
            applicationContext.Dispose();
        }

        public Blogs Get(int id)
        {
            return applicationContext.Blogs.Where(blog => blog.ID == id).FirstOrDefault();
        }

        //public IEnumerable<Blogs> GetAll()
        //{
        //    return applicationContext.Blogs.Where(blog => true);//TODO how change
        //}

        public IEnumerable<Blogs> GetByName(string name)
        {
            return applicationContext.Blogs.Where(blog => blog.Bloger.UserName ==name );
        }
        public IEnumerable<Blogs> GetAll()
        {
            return applicationContext.Blogs.Where(blog =>true);
        }
        public void Modify(Blogs item)
        {
            throw new NotImplementedException();
        }
    }
}
