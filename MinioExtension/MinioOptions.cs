using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinioExtension
{
    public class MinioOptions
    {
        public string EndPoint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public bool IsSSL { get; set; }
    }
}
