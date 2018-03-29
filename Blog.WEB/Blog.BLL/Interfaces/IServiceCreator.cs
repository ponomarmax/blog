namespace Blog.BLL.Interfaces
{
    public interface IServiceCreator
    {
        IUserService userService { get; }
        IPostService postService { get; }
        IBlogService blogService { get; }
        ICommentService commentService { get; }
        void Dispose();
    }
}
