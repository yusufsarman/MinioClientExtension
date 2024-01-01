using Minio.DataModel.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinioExtension
{
    public class MinioFileResult : IDisposable
    {
        public MemoryStream Stream { get; }

        public MinioFileResult(MemoryStream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}
