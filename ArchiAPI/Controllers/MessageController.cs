using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using ArchiAPI.Models;
using System.Threading.Tasks;
using ArchiAPI.Commands;
using Microsoft.AspNetCore.Authorization;
using ArchiAPI.Queries;
using System;
using ArchiAPI.Services;
namespace ArchiAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public sealed class MessageController : ControllerBase
    {
        private readonly GenericMessageQuery _query;
        private readonly CreateMessageCommand _createMessage;
        private readonly RemoveMessageCommand _removeMessage;
        public MessageController(GenericMessageQuery query,CreateMessageCommand createMessage,RemoveMessageCommand removeMessage)
        {
            _query = query;
            _createMessage = createMessage;
            _removeMessage = removeMessage;
        }
        [HttpGet("{last}")]
        public async Task<IActionResult> GetMessages(int last)
        {
            return new ObjectResult(await _query.GetMessages(last));
        }
        [HttpDelete("{createdAt}/{txt}")]
        public async Task<IActionResult> RemoveMessage(DateTime createdAt,string txt)
        {
            txt = txt.Replace("$", " ");
            await _removeMessage.ExecuteAsync(txt, User.Identity.Name, createdAt);
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody]MessageDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _createMessage.ExecuteAsync(User.Identity.Name, dto);
            return Ok();
        }
    }
}
