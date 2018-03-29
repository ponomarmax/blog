using System.Collections.Generic;

namespace Blog.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public virtual ICollection<BlogModel> Blogs { get; set; }
        public bool Blocked { get; set; }
    }
}
