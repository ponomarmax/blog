namespace Blog.DAL.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Blog.DAL.Entities;
    using System.Data.Entity.Infrastructure;
    using Microsoft.AspNet.Identity.EntityFramework;

    public partial class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext() : base("Blog") { }
        public ApplicationContext(string connection = "Blog") : base(connection) { }

        //public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Role>()
            //    .HasMany(e => e.Users)
            //    .WithMany(e => e.)
            //    .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));
            //modelBuilder.Entity<Role>()
            //.HasKey(r => new { r.Id, r })
            //.ToTable("AspNetUserRoles");

            //modelBuilder.Entity<TUserLogin>()
            //            .HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId })
            //            .ToTable("AspNetUserLogins");

            modelBuilder.Entity<Posts>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Posts>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("PostsTags").MapLeftKey("PostID").MapRightKey("TagID"));
        }
    }
}
