using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinioExtension;

namespace Client
{
    public class Startup
    {
        IConfigurationRoot Configuration { get; }

        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = Configuration.GetSection("Minio");            
            services.AddSingleton<IConfigurationRoot>(Configuration);
            services.AddMinioService(options =>
            {
                options.EndPoint = configuration["EndPoint"];
                options.AccessKey = configuration["AccessKey"];
                options.SecretKey = configuration["SecretKey"];
                options.IsSSL = Convert.ToBoolean(configuration["IsSSL"]);                
            });
        }
    }
}
