using AutoMapper;
using Blog.BLL.DTO;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.WEB.Controllers
{
    public class BaseController : Controller
    {
        protected IMapper mapperBusinessToView, mapperViewToBusiness;

        public BaseController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogDTO, BlogModel>();
                cfg.CreateMap<PostDTO, PostModel>()
                .ForMember(x => x.ShortBody, n => n.MapFrom(news => GetShortBody(news)));
                cfg.CreateMap<UserDTO, UserModel>();
                cfg.CreateMap<CommentDTO, CommentModel>();
                cfg.CreateMap<TagDTO, TagModel>();
            });
            config.AssertConfigurationIsValid();
            mapperBusinessToView = config.CreateMapper();

            config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BlogModel, BlogDTO>();
                cfg.CreateMap<PostModel, PostDTO>();
                cfg.CreateMap<UserModel, UserDTO>();
                cfg.CreateMap<CommentModel, CommentDTO>();
                cfg.CreateMap<TagModel, TagDTO>();
            });
            config.AssertConfigurationIsValid();
            mapperViewToBusiness = config.CreateMapper();
        }
        string GetShortBody(PostDTO post)
        {
            const int CountWords = 100;

            List<string> words = new List<string>(post.Body.Split(new char[] { ' ' }));
            if (CountWords < words.Count)
            {
                words.RemoveRange(CountWords, words.Count - CountWords);
                return String.Join(" ", words);
            }
            return post.Body;
        }
    }
}