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
    public sealed class StoreController : ControllerBase
    {
        private readonly CreateStoreCommand _createStore;
        private readonly UpdateStoreCommand _updateStore;
        private readonly RemoveStoreCommand _removeStore;
        private readonly GenericStoreQuery _query;
        private readonly IPathProvider _provider;
        public StoreController(IPathProvider provider,CreateStoreCommand createStore,UpdateStoreCommand updateStore,RemoveStoreCommand removeStore,GenericStoreQuery query)
        {
            _provider = provider;
            _createStore = createStore;
            _updateStore = updateStore;
            _removeStore = removeStore;
            _query = query;
        }
        [HttpGet("{title}")]
        public async Task<IActionResult> GetStore(string title)
        {
            return new ObjectResult(await _query.GetStore(title));
        }
        [HttpGet("{top:int}/{offset:int}")]
        public async Task<IActionResult> GetStores(int top,int offset)
        {
            return new ObjectResult(await _query.GetStores(top, offset));
        }
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody]StoreDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _createStore.ExecuteAsync(User.Identity.Name, dto, _provider.Add(User.Identity.Name, dto.IconData, false));
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateStore([FromBody]StoreDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _updateStore.ExecuteAsync(dto, _provider.Add(User.Identity.Name, dto.IconData, false));
            return Ok();
        }
        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteStore(string title)
        {
            await _removeStore.ExecuteAsync(title);
            return Ok();
        }
    }
}
