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
    public class CommentService : ICommentService
    {
        IMapper mapperBusinessToDB, mapperDBToBusiness;
        IUnitOfWork Database { get; set; }
        public CommentService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
            mapperBusinessToDB = new MapperConfiguration(cfg => cfg.CreateMap<CommentDTO, Comments>().PreserveReferences()).CreateMapper();
            mapperDBToBusiness = new MapperConfiguration(cfg => cfg.CreateMap<Comments, CommentDTO>().PreserveReferences()).CreateMapper();
        }
        public void Create(CommentDTO comment)
        {
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
                throw new Exception("Не установлено id коментарря");
            Comments comment = Database.commentRepository.Get(id.Value);
            if (comment == null)
                throw new Exception("коментарь  не найден");

            return mapperDBToBusiness.Map<CommentDTO>(comment);
        }

        public IEnumerable<CommentDTO> GetAll(int postId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Comments, CommentDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Comments>, List<CommentDTO>>(Database.commentRepository.GetAll(postId));
        }

        public void Modify(CommentDTO post)
        {
            throw new NotImplementedException();
        }
    }
}
