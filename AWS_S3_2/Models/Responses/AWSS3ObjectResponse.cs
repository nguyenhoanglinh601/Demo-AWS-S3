using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_S3_2.Models.Responses
{
    public class AWSS3ObjectResponse
    {
        public Stream Result { get; set; }
        public string Extenstion { get; set; }
    }
}
