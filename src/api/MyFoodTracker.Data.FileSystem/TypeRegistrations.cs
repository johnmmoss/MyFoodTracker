using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFoodTracker.Api;

namespace MyFoodTracker.Data.FileSystem;

public static class TypeRegistrations
{
    public static void AddJsonFoodRepository(this IServiceCollection servicesCollection, ConfigurationManager configuration)
    {
        //servicesCollection.Configure<MyFoodTrackerSettings>(configuration.GetSection(MyFoodTrackerSettings.Name));
        servicesCollection.AddTransient<IFoodRepository, JsonFoodRepository>();
        servicesCollection.AddTransient<IFileSystem, FileSystem>();
    }
}