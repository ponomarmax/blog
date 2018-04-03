using Blog.BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Interfaces
{
    public interface IRoleService
    {
        List<string> GetRolesByUserId(string userId);
        List<string> GetAll();
        string GetById(string id);
        OperationDetails UpdateListRoles(string[] changedRoles, string userId);
    }
}
