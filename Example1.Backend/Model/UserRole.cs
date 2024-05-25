using EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.Backend.Model
{
    public class UserRole : IEntityT<int>
    {
        public int Id { get; set; }
        public required User User { get; set; }
        public Role Role { get; set; }
    }
}
