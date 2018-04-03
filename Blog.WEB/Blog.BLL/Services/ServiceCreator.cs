using Blog.BLL.Interfaces;
using Blog.DAL.Interfaces;
using Blog.DAL.Repositories;

namespace Blog.BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        IUnitOfWork unitOfWork;
        public ServiceCreator(string connection)
        {
            unitOfWork = new IdentityUnitOfWork(connection);
            userService = new UserService(unitOfWork);
            postService = new PostService(unitOfWork);
            blogService = new BlogService(unitOfWork);
            commentService = new CommentService(unitOfWork);
            roleService = new RoleService(unitOfWork);
        }
        public IUserService UserService(string con) => new UserService(new IdentityUnitOfWork(con));
        public IUserService userService { get; private set; }
        public IRoleService roleService { get; private set; }

        public IPostService postService { get; private set; }

        public IBlogService blogService { get; private set; }

        public ICommentService commentService { get; private set; }
        public void Dispose()
        {
            //userService.Dispose();
            //postService.Dispose();
            //postService.Dispose();
            //blogService.Dispose();
            //commentService.Dispose();
            unitOfWork.Dispose();
        }

    }
}
