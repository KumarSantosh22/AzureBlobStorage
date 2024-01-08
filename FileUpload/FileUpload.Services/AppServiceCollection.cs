using FileUpload.Concerns.Config;
using FileUpload.Services.Contracts;
using FileUpload.Services.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileUpload.Services
{
    public static class AppServiceCollection
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            AzureBlobConfig blobConfig = configuration.GetSection("AzureConfig").Get<AzureBlobConfig>();
             _ = services.AddSingleton(blobConfig);

            _ = services.AddScoped<IFileUploadContract, FileUploadProvider>();
        }
    }
}
