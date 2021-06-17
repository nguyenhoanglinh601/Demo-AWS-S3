using AWS_S3_2.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_S3_2.Controllers
{
    [Produces("application/json")]
    [Route("api/AWSS3Object")]
    public class AWSS3ObjectController : Controller
    {
        private readonly IAWSS3Service _service;
        public AWSS3ObjectController(IAWSS3Service service)
        {
            _service = service;
        }

        [Route("postObject")]
        [HttpPost]
        public async Task<IActionResult> postObject(IFormFile file)
        {
            return Ok(await _service.postObjectAsync(file));
        }

        [Route("getObject")]
        [HttpGet]
        public async Task<IActionResult> getObject(string fileName)
        {
            try
            {
                var ObjResult = await _service.getObjectAsync(fileName);
                return File(ObjResult.Result, ObjResult.Extenstion);
            }
            catch
            {
                return Ok("NoFile");
            }
        }
    }
}
