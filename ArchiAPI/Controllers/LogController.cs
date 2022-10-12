﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ArchiAPI.Services;
using Microsoft.Extensions.Logging;
namespace ArchiAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public sealed class LogController : ControllerBase
    {
        private readonly ILogger _logger;
        public LogController(ILoggerWrapper wrapper)
        {
            _logger = wrapper.Unwrap();
        }
        [HttpPost]
        public IActionResult Post([FromHeader]string log)
        {
            _logger.LogInformation(log);
            return Ok();
        }
    }
}