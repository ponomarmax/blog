﻿using AutoMapper;
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
    public class PostService :Service, IPostService
    {
        public PostService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(PostDTO post,string tags)
        {
            if (post == null)
                throw new NullReferenceException("post");
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
                throw new NullReferenceException("id is missing");
            Posts post = Database.postRepository.Get(id.Value);
            if (post == null)
                throw new Exception("Post is missing");

            return mapperDBToBusiness.Map<PostDTO>(post);
        }

        public IEnumerable<PostDTO> GetByBlogId()
        {
          //  var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Posts, PostDTO>()).CreateMapper();
            return mapperDBToBusiness.Map<IEnumerable<Posts>, List<PostDTO>>(Database.postRepository.GetAll());
        }

        public void Modify(PostDTO postDTO)
        {
            if (postDTO == null)
                throw new NullReferenceException("post");
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
