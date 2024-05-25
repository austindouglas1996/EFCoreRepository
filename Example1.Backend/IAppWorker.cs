using EFRepository;
using One.Backend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One.Backend
{
    internal interface IAppWorker : IWorker<AppContext>
    {
        UserRepository Users { get; }
        UserRoleRepository Roles { get; }
    }
}
