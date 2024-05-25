using EFRepository;
using One.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One.Backend.Repository
{
    public class UserRoleRepository(AppContext context) : Repository<AppContext, UserRole, int>(context)
    {
    }
}
