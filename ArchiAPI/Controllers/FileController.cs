using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ArchiAPI.Services;
using System.IO;
namespace ArchiAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IPathProvider _pathProvider;
        public FileController(IPathProvider provider)
        {
            _pathProvider = provider;
        }
        [HttpGet]
        public IActionResult LoadFile([FromQuery]string targetPath)
        {
            var data = _pathProvider.Read(targetPath);
            return File(data, "application/octate-stream");
        }
    }
}
