using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VolumeTest.Services;

namespace VolumeTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly string _infoFilePath = "app/info_volume/logs.txt";
        private readonly string _errorFilePath = "app/error_volume/logs.txt";
        public DataController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<string> Get(string value)
        {
            return await _fileService.GetData(value);
        }
        [HttpPost] 
        public async Task<IActionResult> Post([FromBody] string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length <= 3)
            {
                
                var success = await _fileService.SetData(_errorFilePath, $"Error: {value}");

                if (!success)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error writing to log file.");
                }

                return BadRequest("Value must be greater than 3 characters.");
            }

         
            var successInfo = await _fileService.SetData(_infoFilePath, $"Info: {value}");

            if (!successInfo)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error writing to log file.");
            }

            return Ok(value);
        }
    }
}
