using EFRepository;
using Example1.UserApp.Model;
using Example1.UserApp.Repository;
using Example1.UserApp.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Example1.UserApp
{
    public class Startup
    {
        public async Task RunAsync(string[] args)
        {
            Console.WriteLine("Starting...");

            // Create a service collection
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Build the service provider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Apply migrations automatically
            await ApplyMigrationsAsync(serviceProvider);

            // Perform operations using the UserService
            await PerformUserOperationsAsync(serviceProvider);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Build configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Register DbContext with scoped lifetime
            services.AddDbContext<AppContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories with the interface
            services.AddScoped<UserRepository>();
            services.AddScoped<UserRoleRepository>();

            // Register other services
            services.AddScoped<UserService>();
            services.AddScoped<UserRoleService>();
        }

        private async Task ApplyMigrationsAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppContext>();
                await context.Database.MigrateAsync();
            }
        }

        private async Task PerformUserOperationsAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userService = scope.ServiceProvider.GetRequiredService<UserService>();

                Console.WriteLine(await AddUser("John Doe", "Johnny@email.com", userService));

                await Task.Delay(1000);
                Console.WriteLine(await AddUser("Joshua", "Joshua@email.com", userService));

                await Task.Delay(1000);
                Console.WriteLine(await AddUser("Austin", "Austin@email.com", userService));

                await userService.SaveChangesAsync();

                Console.WriteLine("List of users:");
                Console.WriteLine(await ListAllUsers(userService));
            }
        }

        private async Task<string> AddUser(string name, string email, UserService service)
        {
            var user = new User { name = name, email = email, password = "password", age = 27 };

            try
            {
                await service.AddAsync(user);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return $"Added: {name} to users!";
        }

        private async Task<string> ListAllUsers(UserService service)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                var users = await service.GetAllAsync();

                if (users.Count() == 0)
                    sb.AppendLine("No users!");

                foreach (var u in users)
                {
                    Console.WriteLine(u.ToString());
                }
            }
            catch(Exception e)
            {
                sb.AppendLine(e.Message);
            }

            return sb.ToString();
        }
    }
}
