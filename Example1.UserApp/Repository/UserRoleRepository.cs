using EFRepository;
using Example1.UserApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp.Repository
{
    public class UserRoleRepository(AppContext context) : Repository<AppContext, UserRole, int>(context)
    {
    }
}
