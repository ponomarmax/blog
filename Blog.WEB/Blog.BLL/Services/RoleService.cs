using AutoMapper;
using Blog.BLL.DTO;
using Blog.BLL.Infrastructure;
using Blog.BLL.Interfaces;
using Blog.DAL.Entities;
using Blog.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Services
{
    public class RoleService :Service, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public List<string> GetRoles(string id)
        {
            var user = Database.userRepository.FindById(id);
            if (user == null)
                throw new Exception("User is not find");
            List<string> roles = new List<string>();
            foreach (var role in user.Roles)
                roles.Add(Database.roleRepository.FindById(role.RoleId).Name);
            
            return roles;
        }

        public OperationDetails UpdateListRoles(string[] changedRoles, string userId)
        {
            if (changedRoles == null)
                return new OperationDetails(false, "You have empty user's roles", "changedRoles");

            User user =  Database.userRepository.FindById(userId);
            if (user == null)
                return new OperationDetails(false, "User is not find", "userId");
            user.Roles.Clear();
            Database.Save();
            var roles = Database.roleRepository.Roles;
            var validateRole = new List<string>();
            foreach (var r in roles)
                if (changedRoles.Contains(r.Name))
                    validateRole.Add(r.Name);

             Database.userRepository.AddToRoles(user.Id, validateRole.ToArray());
             Database.Save();
            //var e = Database.userRepository.GetRoles(userId);
            return new OperationDetails(true, "Roles successfully updated", "");
        }

        public List<string> GetRolesByUserId(string userId)
        {
            List<string> roles = new List<string>();
            User user = Database.userRepository.FindById(userId);
            if (user == null)
                throw new Exception("User is not find");

            foreach (var currentRole in user.Roles)
                roles.Add(GetById(currentRole.RoleId));

            return roles;
        }

        public List<string> GetAll()
        {
            List<string> roles = new List<string>();
            foreach (var item in Database.roleRepository.Roles)
                roles.Add(item.Name);
            return roles;
        }

        public string GetById(string id)
        {
            return Database.roleRepository.FindById(id).Name;
        }
    }
}
