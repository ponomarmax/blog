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
    public class CommentService :Service, ICommentService
    {
        public CommentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void Create(CommentDTO comment)
        {
            if (comment == null)
                throw new NullReferenceException("comment");
            Database.commentRepository.Create(mapperBusinessToDB.Map<Comments>(comment));
        }

        public void Delete(int id)
        {
            Database.commentRepository.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public CommentDTO Get(int? id)
        {
            if (id == null)
                throw new NullReferenceException("Id is missing");
            Comments comment = Database.commentRepository.Get(id.Value);
            if (comment == null)
                throw new Exception("Comment is missing");

            return mapperDBToBusiness.Map<CommentDTO>(comment);
        }

        public IEnumerable<CommentDTO> GetAll(int postId)
        {
            //var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Comments, CommentDTO>()).CreateMapper();
            return mapperDBToBusiness.Map<IEnumerable<Comments>, List<CommentDTO>>(Database.commentRepository.GetAll(postId));
        }

        public void Modify(CommentDTO post)
        {
            throw new NotImplementedException();
        }
    }
}
