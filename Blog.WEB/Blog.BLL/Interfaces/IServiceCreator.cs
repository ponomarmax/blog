namespace Blog.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService userService { get; }
        IPostService postService { get; }
        IBlogService blogService { get; }
        IRoleService roleService { get; }
        ICommentService commentService { get; }
        IUserService UserService(string con);
        void Dispose();
    }
}
