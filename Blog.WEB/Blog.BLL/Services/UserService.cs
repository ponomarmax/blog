using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;
using Blog.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using Blog.BLL.Interfaces;
using Blog.DAL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using System;
using Blog.DAL.Repositories;

namespace Blog.BLL.Services
{
    public class UserService :Service, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await Database.userRepository.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email };
                await Database.userRepository.CreateAsync(user, userDto.Password);
                // добавляем роль
                await Database.userRepository.AddToRoleAsync(user.Id, "user");
                // создаем профиль клиента
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");

            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }
        //public void Create(UserDTO userDto)
        //{
        //    User user = Database.userRepository.FindByEmail(userDto.Email);
        //    if (user == null)
        //    {
        //        user = new User { Email = userDto.Email, UserName = userDto.Email };
        //        Database.userRepository.Create(user, userDto.Password);
        //        // добавляем роль
        //         Database.userRepository.AddToRole(user.Id, "user");
        //        // создаем профиль клиента
        //         Database.Save();
        //        //return new OperationDetails(true, "Регистрация успешно пройдена", "");

        //    }
        //    else
        //    {
        //        //return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
        //    }
        //}
        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            User user = await Database.userRepository.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.userRepository.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            IEnumerable<UserDTO> users = mapperDBToBusiness.Map<IEnumerable<UserDTO>>(Database.userRepository.Users);
            return users;
        }

        public UserDTO GetByName(string name)
        {
            if (name == null || name == "")
                throw new NullReferenceException("name");
            UserDTO users = mapperDBToBusiness.Map<UserDTO>(Database.userRepository.FindByName(name));
            return users;
        }
        public UserDTO GetById(string id)
        {
            if (id == null || id == "")
                throw new NullReferenceException("id");
            UserDTO users = mapperDBToBusiness.Map<UserDTO>(Database.userRepository.FindById(id));
            return users;
        }

        public void BlockUnblock(string id)
        {
            if (id == null || id == "")
                throw new NullReferenceException("id");
            User user = Database.userRepository.FindById(id);
            user.Blocked = !user.Blocked;
            Database.userRepository.Update(user);
        }
        public void Modify(UserDTO user)
        {
            if (user == null)
                throw new NullReferenceException("user");


            User userDB = Database.userRepository.FindById(user.Id);
            userDB.Photo=user.Photo;
            Database.userRepository.Update(userDB);
            Database.Save();
        }

       
    }


}
