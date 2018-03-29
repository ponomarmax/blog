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

namespace Blog.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }
        IMapper mapperDBToBusiness, mapperBusinessToDB;
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Blogs, BlogDTO>().PreserveReferences();
                cfg.CreateMap<Comments, CommentDTO>().PreserveReferences();
                cfg.CreateMap<Tags, TagDTO>().PreserveReferences();
                cfg.CreateMap<Posts, PostDTO>().PreserveReferences();
                cfg.CreateMap<User, UserDTO>().PreserveReferences()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.MapFrom<string>(user => {
                    string role = "";
                    foreach (var i in user.Roles)
                    {
                        role += Database.roleRepository.FindById(i.RoleId);
                    }
                    return role;
                });
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

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await Database.userRepository.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email };
                await Database.userRepository.CreateAsync(user, userDto.Password);
                // добавляем роль
                await Database.userRepository.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");

            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

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

        //// начальная инициализация бд
        //public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        //{
        //    foreach (string roleName in roles)
        //    {
        //        var role = await Database.roleRepository.FindByNameAsync(roleName);
        //        if (role == null)
        //        {
        //            role = new Role { Name = roleName };
        //            await Database.roleRepository.CreateAsync(role);
        //        }
        //    }

        //    await Create(adminDto);
        //}

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
            Database.userRepository.Update(mapperBusinessToDB.Map<User>(user));
        }
    }


}
