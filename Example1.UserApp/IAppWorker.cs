using EFRepository;
using Example1.UserApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp
{
    internal interface IAppWorker : IWorker<AppContext>
    {
        UserRepository Users { get; }
        UserRoleRepository Roles { get; }
    }
}
