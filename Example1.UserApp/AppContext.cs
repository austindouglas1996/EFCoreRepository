using Example1.UserApp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp
{
    public class AppContext : DbContext
    {
        public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
