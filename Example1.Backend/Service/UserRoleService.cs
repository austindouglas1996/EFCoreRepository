using EFRepository;
using Example1.Backend.Model;
using Example1.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.Backend.Service
{
    public class UserRoleService(UserRoleRepository repository) : EntityServiceBase<UserRoleRepository, UserRole, int>(repository)
    {
    }
}
