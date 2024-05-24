using Example1.UserApp;
using Example1.UserApp.Model;
using Example1.UserApp.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class Program
{
    public static async Task Main(string[] args)
    {
        var startup = new Startup();
        await startup.RunAsync(args);

        Console.ReadLine();
    }
}

