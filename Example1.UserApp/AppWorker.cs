using EFRepository;
using Example1.UserApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp
{
    public class AppWorker : IAppWorker
    {
        public AppContext Context { get; private set; }

        public UserRepository Users { get; private set; }

        public UserRoleRepository Roles { get; private set; }

        public AppWorker(AppContext context)
        {
            Context = context;
            this.InitRepository();
        }

        public void Dispose()
        {
            this.Context.Dispose();
        }

        public async Task<bool> SaveChanges()
        {
            await this.Context.SaveChangesAsync();
            return true;
        }

        private void InitRepository()
        {
            Users = new UserRepository(this.Context);
            Roles = new UserRoleRepository(this.Context);
        }
    }
}
