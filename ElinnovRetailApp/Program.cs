using ElinnovRetail.Data.Context;
using ElinnovRetail.Data.Repositories;
using ElinnovRetail.Data.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ElinnovRetail.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            var serviceProvider = SetupServices();
            var inventory = serviceProvider.GetRequiredService<InventoryMain>();
            inventory.Run();
        }

        private static ServiceProvider SetupServices()
        {
            return new ServiceCollection()
            .AddScoped<RetailAppDbContext>()
            .AddSingleton<InventoryMain>()
            .AddScoped<IInventoryManager, InventoryManager>()
            .BuildServiceProvider();
        }
    }
}
