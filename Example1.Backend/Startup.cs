using EFRepository;
using Example1.Backend.Model;
using Example1.Backend.Repository;
using Example1.Backend.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Example1.Backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Register DbContext with scoped lifetime
            services.AddDbContext<AppContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories with the interface
            services.AddScoped<UserRepository>();
            services.AddScoped<UserRoleRepository>();

            // Register other services
            services.AddScoped<UserService>();
            services.AddScoped<UserRoleService>();

            // Add controllers
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Apply migrations and perform initial operations
            ApplyMigrationsAsync(app.ApplicationServices).Wait();
            PerformUserOperationsAsync(app.ApplicationServices).Wait();
        }

        private async Task ApplyMigrationsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppContext>();
            await context.Database.MigrateAsync();
        }

        private async Task PerformUserOperationsAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
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
                    sb.AppendLine(u.ToString());
                }
            }
            catch (Exception e)
            {
                sb.AppendLine(e.Message);
            }

            return sb.ToString();
        }
    }
}
