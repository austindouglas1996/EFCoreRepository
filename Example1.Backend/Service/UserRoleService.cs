using EFRepository;
using One.Shared.Model;
using One.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One.Backend.Service
{
    public class UserRoleService(UserRoleRepository repository) : EntityServiceBase<UserRoleRepository, UserRole, int>(repository)
    {
    }
}
