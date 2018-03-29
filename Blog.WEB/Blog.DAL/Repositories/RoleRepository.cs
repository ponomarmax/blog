using Blog.DAL.EF;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repositories
{
    //public class RoleRepository : IRepository<IdentityRole>
    //{
    //    private ApplicationContext applicationContext;

    //    public RoleRepository(ApplicationContext db)
    //    {
    //        applicationContext = db;
    //    }
    //    public void Create(IdentityRole role)
    //    {
    //        applicationContext.Roles.Add(role);
    //        applicationContext.SaveChanges();
    //    }

    //    public void Delete(string id)
    //    {
    //        applicationContext.Roles.Remove(Get(id));
    //        applicationContext.SaveChanges();
    //    }
    //    public void Dispose()
    //    {
    //        applicationContext.Dispose();
    //    }
    //    public IdentityRole Get(string id)
    //    {
    //        return applicationContext.Roles.Where(role => role.Id == id).FirstOrDefault();
    //    }

    //    public IEnumerable<IdentityRole> GetAll()
    //    {
    //        return applicationContext.Roles.Where(role => true);//TODO how change
    //    }
    //}
}
