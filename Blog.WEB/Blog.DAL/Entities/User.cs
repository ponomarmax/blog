namespace Blog.DAL.Entities
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User : IdentityUser
    {
        public virtual ICollection<Blogs> Blogs { get; set; }
        public bool Blocked { get; set; }
        public byte[] Photo { get; set; }
    }
}