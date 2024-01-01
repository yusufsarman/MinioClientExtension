using Microsoft.Extensions.DependencyInjection;
using Minio;
namespace MinioExtension
{
    public static class MinioServiceExtensions
    {
        public static IServiceCollection AddMinioService(this IServiceCollection services, Action<MinioOptions> configure)
        {
            
            var options = new MinioOptions();
            configure?.Invoke(options);
            if (!string.IsNullOrWhiteSpace(options.EndPoint))
            {
                // Register MinioClient as Singleton
                services.AddSingleton(_ =>
                {
                    return new MinioClient().WithEndpoint(options.EndPoint)
                                            .WithCredentials(options.AccessKey, options.SecretKey)
                                            .WithSSL(options.IsSSL).Build();
                });

                // Register IMinioService as Transient, resolving MinioClient from the service provider
                services.AddTransient<IMinioService>(provider =>
                {
                    var minioClient = provider.GetRequiredService<IMinioClient>();
                    return new MinioService(minioClient);
                });
            }

            return services;
        }
    }
}
