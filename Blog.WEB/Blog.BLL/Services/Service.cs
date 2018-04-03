using AutoMapper;
using Blog.BLL.DTO;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class Service
    {
        protected IMapper mapperBusinessToDB, mapperDBToBusiness;
        protected IUnitOfWork Database { get; set; }
        public Service(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Blogs, BlogDTO>().PreserveReferences();
                cfg.CreateMap<Comments, CommentDTO>().PreserveReferences();
                cfg.CreateMap<Tags, TagDTO>().PreserveReferences();
                cfg.CreateMap<Posts, PostDTO>().PreserveReferences();
                cfg.CreateMap<User, UserDTO>().PreserveReferences()
                .ForMember(dest => dest.Password, opt => opt.Ignore());
                //.ForMember(dest => dest.Role, opt => opt.Ignore());
            });
            config.AssertConfigurationIsValid();
            mapperDBToBusiness = config.CreateMapper();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDTO, Blogs>().PreserveReferences();
                cfg.CreateMap<PostDTO, Posts>().PreserveReferences();
                cfg.CreateMap<UserDTO, User>().PreserveReferences().ForAllOtherMembers(dest => dest.Ignore());
                cfg.CreateMap<CommentDTO, Comments>().PreserveReferences();
                cfg.CreateMap<TagDTO, Tags>().PreserveReferences();
            });
            config.AssertConfigurationIsValid();
            mapperBusinessToDB = config.CreateMapper();
        }
    }
}
