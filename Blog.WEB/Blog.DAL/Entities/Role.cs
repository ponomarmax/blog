namespace Blog.DAL.Entities
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role:IdentityRole
    {

        //public int Id { get; set; }

        //[Required]
        //[StringLength(256)]
        //public string Name { get; set; }

        //public virtual ICollection<User> Users { get; set; }
    }
}
