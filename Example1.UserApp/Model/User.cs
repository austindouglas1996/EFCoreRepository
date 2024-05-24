using EFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp.Model
{
    public class User : IEntityT<int>
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int age { get; set; }
        public User() { }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {name}, Email: {email}, Age: {age}";
        }
    }
}
