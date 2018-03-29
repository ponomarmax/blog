using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;

namespace Blog.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        IEnumerable<UserDTO> GetAll();
        UserDTO GetByName(string name);
        UserDTO GetById(string id);
        void BlockUnblock(string id);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        void Modify(UserDTO user);
        //Task SetInitialData(UserDTO adminDto, List<string> roles);
    } 
}
