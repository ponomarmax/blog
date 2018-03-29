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
    public class PostService : IPostService
    {
        IMapper mapperBusinessToDB, mapperDBToBusiness;
        IUnitOfWork Database { get; set; }
        public PostService(IUnitOfWork unitOfWork)
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
                cfg.CreateMap<BlogDTO, Blogs>().PreserveReferences();
                cfg.CreateMap<PostDTO, Posts>().PreserveReferences();
                cfg.CreateMap<UserDTO, User>().PreserveReferences().ForAllOtherMembers(dest => dest.Ignore());
                cfg.CreateMap<CommentDTO, Comments>().PreserveReferences();
                cfg.CreateMap<TagDTO, Tags>().PreserveReferences();
            });
            config.AssertConfigurationIsValid();
            mapperBusinessToDB = config.CreateMapper();
        }
        public void Create(PostDTO post,string tags)
        {
            
            if (tags != null && tags != "")
            {
                string[] tempTags = tags.Split(' ');
                post.Tags = new List<TagDTO>();
                foreach (var t in tempTags)
                    post.Tags.Add(new TagDTO() { Tag = t });
            }

            Database.postRepository.Create(mapperBusinessToDB.Map<Posts>(post));
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public PostDTO Get(int? id)
        {
            if (id == null)
                throw new Exception("Не установлено id телефона");
            Posts post = Database.postRepository.Get(id.Value);
            if (post == null)
                throw new Exception("Телефон не найден");

            return mapperDBToBusiness.Map<PostDTO>(post);
        }

        public IEnumerable<PostDTO> GetByBlogId()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Posts, PostDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Posts>, List<PostDTO>>(Database.postRepository.GetAll());
        }

        public void Modify(PostDTO postDTO)
        {
            if (postDTO == null)
            {
                throw new ArgumentException("post");
            }
            Posts post = mapperBusinessToDB.Map<Posts>(postDTO);
            Database.postRepository.Modify(post);
        }

        public void Delete(int id)
        {
            Database.postRepository.Delete(id);
        }

        public IEnumerable<PostDTO> GetByBlogId(int id)
        {
            return mapperDBToBusiness.Map<IEnumerable<Posts>, List<PostDTO>>(Database.postRepository.GetByBlogId(id));
        }

        public IEnumerable<PostDTO> GetByTagId(int id)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Posts, PostDTO>()).CreateMapper();
            return mapperDBToBusiness.Map<IEnumerable<Posts>, List<PostDTO>>(Database.postRepository.GetByTagId(id));
        }
    }
}
