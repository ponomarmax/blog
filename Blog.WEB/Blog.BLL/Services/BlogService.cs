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
    public class BlogService :Service, IBlogService
    {
        public BlogService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

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
