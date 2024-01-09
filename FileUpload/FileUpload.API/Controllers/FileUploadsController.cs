using FileUpload.Concerns.Core;
using FileUpload.Services.Contracts;
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

        [HttpGet("{name}")]
        public async Task<ActionResult<Stream>> GetAsync([FromRoute] string name)
        {
            try
            {
                return Ok(await fileUploadContract.GetBlobAsync(name));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

        [HttpDelete("{name}")]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] string name)
        {
            try
            {
                return Ok(await fileUploadContract.DeleteFileAsync(name));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
