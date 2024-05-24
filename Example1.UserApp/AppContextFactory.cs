using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp
{
    public class AppContextFactory : IDesignTimeDbContextFactory<AppContext>
    {
        public AppContext CreateDbContext(string[] args)
        {
            // Build configuration
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Get connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configure DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<AppContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppContext(optionsBuilder.Options);
        }
    }
}
