using EFRepository;
using One.Backend.Repository;
using One.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One.Backend.Service
{
    public class UserService(UserRepository repository) : EntityServiceBase<UserRepository, User, int>(repository)
    {
        public override async Task AddAsync(User entity)
        {
            if (await Repository.VerifyUserDoesNotExist(entity) == false)
            {
                throw new ArgumentException("A user already exists by that id or name.");
            }

            if (entity.name == "Joshua")
                Console.WriteLine("Booooooooooooooooomer");

            await base.AddAsync(entity);
        }
    }
}
