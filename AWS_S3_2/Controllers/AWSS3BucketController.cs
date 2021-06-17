using AWS_S3_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_S3_2.Controllers
{
    [Produces("application/json")]
    [Route("api/AWSS3Bucket")]
    public class AWSS3BucketController : Controller
    {
        private readonly IAWSS3Service _service;
        public AWSS3BucketController(IAWSS3Service service)
        {
            _service = service;
        }

        [Route("getNumberOfBuckets")]
        [HttpGet]
        public async Task<int> getNumberOfBuckets()
        {
            return await _service.getNumberOfBucketsAsync();
        }
    }
}
