using EFRepository;
using Example1.UserApp.Model;
using Example1.UserApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp.Service
{
    public class UserRoleService(UserRoleRepository repository) : EntityServiceBase<UserRoleRepository, UserRole, int>(repository)
    {
    }
}
