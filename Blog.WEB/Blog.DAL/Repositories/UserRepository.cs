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
    //public class UserRepository : IRepository<User>
    //{
    //    private ApplicationContext applicationContext;

    //    public UserRepository(ApplicationContext db)
    //    {
    //        applicationContext = db;
    //    }
    //    public void Create(User user)
    //    {
    //        applicationContext.Users.Add(user);
    //        applicationContext.SaveChanges();
    //    }
    //    public void Dispose()
    //    {
    //        applicationContext.Dispose();
    //    }
    //    public void Delete(string id)
    //    {
    //        applicationContext.Users.Remove(Get(id));
    //        applicationContext.SaveChanges();
    //    }

    //    public User Get(string id)
    //    {
    //        return applicationContext.Users.Where(user => user.Id == id).FirstOrDefault();
    //    }

    //    public IEnumerable<User> GetAll()
    //    {
    //        return applicationContext.Users.Where(user => true);//TODO how change
    //    }
    //}
}
