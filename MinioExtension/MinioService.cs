using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MinioExtension
{
    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minioClient;

        public MinioService(IMinioClient minioClient)
        {
            _minioClient = minioClient;
        }

        public async Task<List<string>> ListBuckets()
        {
            var buckets = await _minioClient.ListBucketsAsync();
            return buckets.Buckets.Select(bucket => bucket.Name).ToList();
        }

        public async Task<bool> BucketExists(string bucketName)
        {
            try
            {
                bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
                return found;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> CreateBucket(string bucketName)
        {
            try
            {
                await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PutObjectResponse> UploadFile(string bucketName, string objectName, string filePath, string contentType)
        {
            return await _minioClient.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithFileName(filePath)
                    .WithContentType(contentType));
        }
        public async Task<PutObjectResponse> UploadFileWithByteArray(string bucketName, string objectName, byte[] fileData, string contentType)
        {
            return await _minioClient.PutObjectAsync(new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithStreamData(new MemoryStream(fileData))
                    .WithObjectSize(fileData.Length)
                    .WithContentType(contentType));
        }       
        public async Task<MinioFileResult> DownloadFile(string bucketName, string objectName)
        {
            var stream = new MemoryStream();
            await _minioClient.GetObjectAsync(new GetObjectArgs().WithBucket(bucketName).WithObject(objectName)
                .WithCallbackStream(x =>
                {
                    x.CopyTo(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                }));           
            return new MinioFileResult(stream);
        }        
    }
}
