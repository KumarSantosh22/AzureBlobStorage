using FileUpload.Concerns.Core;
using FileUpload.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileUpload.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadsController : ControllerBase
    {
        private readonly IFileUploadContract fileUploadContract;

        public FileUploadsController(IFileUploadContract fileUpload)
        {
            fileUploadContract = fileUpload;
        }

        [HttpPost]
        public async Task<ActionResult<string>> UploadAsync([FromBody] BlobFile file)
        {
            try
            {
                return Ok(await fileUploadContract.UploadFileAsync(file));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
