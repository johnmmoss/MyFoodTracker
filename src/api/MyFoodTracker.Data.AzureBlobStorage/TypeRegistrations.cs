using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using MyFoodTracker.Api;

namespace MyFoodTracker.Data.AzureBlobStorage;

public static class TypeRegistrations
{
    private const string StorageAccountName = "mftwebdevuks";
    
    public static void AddAzureBlobFoodRepository(this IServiceCollection servicesCollection)
    {
        servicesCollection.AddAzureClients(x =>
        {
            x.AddBlobServiceClient(new Uri($"https://{StorageAccountName}.blob.core.windows.net"));
            x.UseCredential(new DefaultAzureCredential());
        });
        servicesCollection.AddTransient<IFoodRepository, AzureBlobFoodRepository>();
    }
}