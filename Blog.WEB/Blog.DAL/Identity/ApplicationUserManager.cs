using Blog.DAL.Entities;
using Microsoft.AspNet.Identity;

namespace Blog.DAL.Identity
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}
