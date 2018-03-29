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
    public class TagRepository : IRepository<Tags>
    {
        private ApplicationContext applicationContext;

        public TagRepository(ApplicationContext db)
        {
            applicationContext = db;
        }
        public void Create(Tags tag)
        {
            applicationContext.Tags.Add(tag);
            applicationContext.SaveChanges();
        }

        public void Delete(int id)
        {
            applicationContext.Tags.Remove(Get(id));
            applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext.Dispose();
        }
        public Tags Get(int id)
        {
            return applicationContext.Tags.Where(tag => tag.ID == id).FirstOrDefault();
        }

        public IEnumerable<Tags> GetAll()
        {
            return applicationContext.Tags.Where(tag => true);//TODO how change
        }

        public IEnumerable<Tags> GetAll(int id)
        {
            throw new NotImplementedException();
        }

        public void Modify(Tags item)
        {
            throw new NotImplementedException();
        }
    }
}
