using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MinioExtension;

namespace Client
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

           

            // Get Service and call method
            var minioService = serviceProvider.GetService<IMinioService>();
            string bucketName = "aaa";
            string fileName = "testfile.jpg";
            string filePath = "C:\\Users\\Yusuf\\Desktop";
            string contentType = "image/jpeg";

            string fileName2 = "testfile2.jpg";

            if (minioService != null)
            {
                //Check bucket exist
                if (!await minioService.BucketExists(bucketName))
                {
                    //Create bucket
                    bool isBucketCreated = await minioService.CreateBucket(bucketName);
                    if (!isBucketCreated)
                    {
                        Console.WriteLine($"The bucket could not be created !!");
                        return;
                    }
                        Console.WriteLine($"Bucket Created. Name = {bucketName}\n\n\n");
                }
                //List Buckets
                var buckets = await minioService.ListBuckets();
                if (buckets != null && buckets.Count > 0)
                {
                    Console.WriteLine("Bucket List : \n\n");
                    foreach (var bucket in buckets)
                    {
                        Console.WriteLine(bucket + "\n");
                    }
                }
                //upload file
                var uploadedInformation = await minioService.UploadFile(bucketName, fileName, filePath, contentType);

                //upload file with byte[]
                byte[] fileBytes = File.ReadAllBytes(filePath + "\\" + fileName2);
                var uploadedInformation2 = await minioService.UploadFileWithByteArray(bucketName, fileName, fileBytes, contentType);
                //download file
                var file1 = await minioService.DownloadFile(bucketName, fileName);
                var file2 = await minioService.DownloadFile(bucketName, fileName2);
                
                //disposable
                file1.Dispose();
                file2.Dispose();
            }



        }
    }
}