using EFRepository;
using Example1.UserApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp.Repository
{
    public class UserRepository(AppContext context) : Repository<AppContext, User, int>(context)
    {
        public async Task<bool> VerifyUserDoesNotExist(User user)
        {
            var results = await base.WhereAsync(r => r.Id == user.Id || r.name == user.name);
            return results.Count() == 0;
        }
    }
}
