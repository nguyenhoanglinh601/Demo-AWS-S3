using Amazon.S3.Model;
using AWS_S3_2.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_S3_2.Services.Interfaces
{
    public interface IAWSS3Service
    {
        Task<int> getNumberOfBucketsAsync();
        Task<PutObjectResponse> postObjectAsync(IFormFile file, int type);
        Task<AWSS3ObjectResponse> getObjectAsync(string fileKey, int type);
    }
}
