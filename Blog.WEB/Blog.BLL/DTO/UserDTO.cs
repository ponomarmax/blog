using System.Collections.Generic;

namespace Blog.BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        //public List<string> Role { get; set; }

        public bool Blocked { get; set; }

        public virtual ICollection<BlogDTO> Blogs { get; set; }

    }
}
