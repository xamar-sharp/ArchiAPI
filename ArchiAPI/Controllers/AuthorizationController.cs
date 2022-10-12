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
    [Authorize]
    [ApiController]
    public sealed class AuthorizationController : ControllerBase
    {
        private readonly GenericUserQuery _query;
        private readonly CreateTokenCommand _createToken;
        private readonly UpdateUserCommand _updateUser;
        private readonly UpdateTokenCommand _updateToken;
        private readonly RemoveUserCommand _removeUser;
        private readonly CreateUserCommand _createUser;
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IRefreshBuilder _refreshBuilder;
        private readonly IPathProvider _pathProvider;
        private readonly Config _config;
        public AuthorizationController(IJwtBuilder jwtBuilder, IPathProvider provider, IRefreshBuilder refreshBuilder,IOptions<Config> config, UpdateUserCommand updateUser, RemoveUserCommand removeUser, UpdateTokenCommand updateToken, CreateTokenCommand createToken, GenericUserQuery query, CreateUserCommand command)
        {
            _query = query;
            _config = config.Value;
            _refreshBuilder = refreshBuilder;
            _jwtBuilder = jwtBuilder;
            _pathProvider = provider;
            _createUser = command;
            _createToken = createToken;
            _updateToken = updateToken;
            _updateUser = updateUser;
            _removeUser = removeUser;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostUser([FromBody] UserProfile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _query.ExecuteAsync(profile.Login, GenericUserQuery.UserQueryIntent.UserByLogin);
            if (user is null)
            {
                User newUser = new User() { CreatedAt = DateTime.UtcNow, Login = profile.Login, Description = profile.Description };
                newUser.IconURI = _pathProvider.Add(profile.Login, profile.IconData, true);
                await _createUser.ExecuteAsync(newUser);
                string jwt = _jwtBuilder.Build(profile.Login,newUser.IconURI, _config.JwtTimeout);
                string refresh = _refreshBuilder.Generate();
                await _createToken.ExecuteAsync(newUser.Login, refresh);
                return new ObjectResult(new AuthorizationInfo() { Jwt = jwt, Refresh = refresh, JwtExpires = DateTime.UtcNow.Add(_config.JwtTimeout) });
            }
            return BadRequest();
        }
        [HttpDelete]
        public async Task<IActionResult> LogOut()
        {
            _pathProvider.Remove(User.FindFirst("IconURI").Value);
            await _removeUser.ExecuteAsync(User.Identity.Name);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> EditUser([FromBody]UserProfile profile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _query.ExecuteAsync(profile.Login, GenericUserQuery.UserQueryIntent.UserByLogin);
            await _updateUser.ExecuteAsync(profile, _pathProvider.Add(user.Login, profile.IconData, true));
            return Ok();
        }
        [HttpGet("{login}")]
        public async Task<IActionResult> GetUserInfo(string login)
        {
            var user = await _query.ExecuteAsync(login, GenericUserQuery.UserQueryIntent.UserByLogin);
            if(user is null)
            {
                return BadRequest();
            }
            return new ObjectResult(new UserProfile() { Description = user.Description, CreatedAt = user.CreatedAt, Login = user.Login, IconData = _pathProvider.Read(user.IconURI) });
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshAuthorize([FromHeader]string refreshToken)
        {
            User newUser = await _query.ExecuteAsync(refreshToken, GenericUserQuery.UserQueryIntent.UserByRefresh);
            if (newUser != null)
            {
                await _updateToken.ExecuteAsync(refreshToken);
                string jwt = _jwtBuilder.Build(newUser.Login, newUser.IconURI, _config.JwtTimeout);
                string refresh = _refreshBuilder.Generate();
                await _createToken.ExecuteAsync(newUser.Login, refresh);
                return new ObjectResult(new AuthorizationInfo() { Jwt = jwt, Refresh = refresh, JwtExpires = DateTime.UtcNow.Add(_config.JwtTimeout) });
            }
            return BadRequest();
        }
       
    }
}
