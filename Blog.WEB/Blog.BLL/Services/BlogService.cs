using AutoMapper;
using Blog.BLL.DTO;
using Blog.BLL.Interfaces;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Blog.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class BlogService : IBlogService
    {
        IMapper mapperBusinessToDB, mapperDBToBusiness;
        IUnitOfWork Database { get; set; }
        public BlogService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Blogs, BlogDTO>().PreserveReferences();
                cfg.CreateMap<Comments, CommentDTO>().PreserveReferences();
                cfg.CreateMap<Tags, TagDTO>().PreserveReferences();
                cfg.CreateMap<Posts, PostDTO>().PreserveReferences();
                cfg.CreateMap<User, UserDTO>().PreserveReferences()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            });
            config.AssertConfigurationIsValid();
            mapperDBToBusiness = config.CreateMapper();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap< BlogDTO, Blogs>().PreserveReferences();
                cfg.CreateMap<PostDTO, Posts>().PreserveReferences();
                cfg.CreateMap<UserDTO, User>().PreserveReferences().ForAllOtherMembers(dest=>dest.Ignore());
                cfg.CreateMap<CommentDTO,Comments >().PreserveReferences();
                cfg.CreateMap<TagDTO,Tags>().PreserveReferences();
            });
            config.AssertConfigurationIsValid();
            mapperBusinessToDB = config.CreateMapper();
        }
        public void  Create(BlogDTO blog)
        {
            User user = Database.userRepository.Users.Where(u=>u.Email==blog.BlogerEmail).First();
            blog.BlogerID = user.Id;
            Database.blogRepository.Create(mapperBusinessToDB.Map<Blogs>(blog));
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public BlogDTO Get(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id телефона");
            Blogs blog = Database.blogRepository.Get(id.Value);
            if (blog == null)
                throw new Exception("Телефон не найден");

            return mapperDBToBusiness.Map<BlogDTO>(blog);
        }

        public IEnumerable<BlogDTO> GetByName(string name)
        {


            //    new MapperConfiguration(cfg => cfg.CreateMap<Blogs, BlogDTO>().
            //ForMember(dest=>dest.Posts, opt=>opt.MapFrom(src=>src.Posts)).
            //ForMember(dest => dest.Bloger, opt => opt.MapFrom(src => src.Bloger)).
            //ForMember(dest => dest.Bloger.Password, opt => opt.Ignore()).
            //ForMember(dest => dest.Bloger.Role, opt => opt.Ignore())).CreateMapper();

            return mapperDBToBusiness.Map<IEnumerable<BlogDTO>>(Database.blogRepository.GetByName(name));
        }

        public void Modify(BlogDTO blog)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BlogDTO> GetAll()
        {
            return mapperDBToBusiness.Map<IEnumerable<BlogDTO>>(Database.blogRepository.GetAll());
        }
    }
}
