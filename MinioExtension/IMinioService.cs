using Minio.DataModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinioExtension
{
    public interface IMinioService
    {
        Task<List<string>> ListBuckets();
        Task<bool> BucketExists(string bucketName);
        Task<bool> CreateBucket(string bucketName);
        Task<PutObjectResponse> UploadFile(string bucketName, string objectName, string filePath, string contentType);
        Task<PutObjectResponse> UploadFileWithByteArray(string bucketName, string objectName, byte[] fileData, string contentType);
        Task<MinioFileResult> DownloadFile(string bucketName, string objectName);
    }
}
