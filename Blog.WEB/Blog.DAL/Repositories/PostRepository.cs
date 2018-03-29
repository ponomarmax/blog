using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    public class PostRepository
    {
        private ApplicationContext applicationContext;

        public PostRepository(ApplicationContext db)
        {
            applicationContext = db;
        }
        public void Create(Posts post)
        {

            IEnumerable<Tags> existingTags = applicationContext.Tags.Where(tag => true);
            Tags tempTag;
            for (int i = 0; i < post.Tags.Count; i++)
            {
                tempTag = existingTags.Where(tag => tag.Tag == post.Tags[i].Tag).FirstOrDefault();
                if (tempTag != null)
                    post.Tags[i] = tempTag;
            }
            applicationContext.Posts.Add(post);
            applicationContext.SaveChanges();
        }

        public void Delete(int id)
        {
            applicationContext.Posts.Remove(Get(id));
            applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext.Dispose();
        }
        public Posts Get(int id)
        {
            return applicationContext.Posts.Where(post => post.ID == id).FirstOrDefault();
        }

        public IEnumerable<Posts> GetAll()
        {
            return applicationContext.Posts.Where(post => true);//TODO how change
        }

        public IEnumerable<Posts> GetByBlogId(int id)
        {
            return applicationContext.Posts.Where(post => post.BlogID == id);
        }
        public IEnumerable<Posts> GetByTagId(int id)
        {
            return applicationContext.Posts.Where(post => post.Tags.Any(tag => tag.ID == id));
        }
        public void Modify(Posts item)
        {
            applicationContext.Entry(item).State = EntityState.Modified;
            applicationContext.SaveChanges();
        }
    }
}
