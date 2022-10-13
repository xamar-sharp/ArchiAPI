using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using System.IO;
using ArchiAPI.Services;
using Microsoft.Extensions.Options;
using ArchiAPI.Commands;
using ArchiAPI.Queries;
using ArchiAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using ArchiAPI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
namespace ArchiAPI.Tests
{
    public sealed class ArchiApiTests
    {
        [Fact]
        public async void TestAuthorization()
        {
            //ARRANGE
            Mock<IJwtBuilder> jwtMock = new Mock<IJwtBuilder>();
            jwtMock.Setup(ent => ent.Build("xamarin@gmail.com", "xamarin@gmail.com\\icon.jpg", TimeSpan.FromMinutes(2)));
            Mock<IRefreshBuilder> refreshMock = new Mock<IRefreshBuilder>();
            refreshMock.Setup(ent => ent.Generate()).Returns($"{Guid.NewGuid()}");
            Mock<IPathProvider> provider = new Mock<IPathProvider>();
            byte[] data = new byte[] { 0, 34, 6, 7, 8, 9, 0 };
            provider.Setup(ent => ent.Add("MainUser",data, true)).Returns("xamarin@gmail.com\\icon.jpg");
            provider.Setup(ent => ent.Remove("xamarin@gmail.com\\icon.jpg")).Returns(true);
            Mock<IOptions<Config>> configMock = new Mock<IOptions<Config>>();
            var secure = File.ReadAllText("C:\\jwt.txt");
            configMock.Setup(ent => ent.Value).Returns(new Config() { Audience = "MOBILEAPP", Issuer = "WEBAPI", JwtTimeout = TimeSpan.FromMinutes(10), Key = "helloworldcryptography", LogPath = "C:\\test.txt", NetString = secure });
            Repository repos = new Repository(new DbContextOptionsBuilder<Repository>().UseSqlServer(secure).Options);
            AuthorizationController ctr = new AuthorizationController(jwtMock.Object, provider.Object, refreshMock.Object, configMock.Object, new UpdateUserCommand(repos)
                , new RemoveUserCommand(repos), new UpdateTokenCommand(repos), new CreateTokenCommand(repos), new GenericUserQuery(repos), new CreateUserCommand(repos));
            //ACT
            var userCreating = await ctr.PostUser(new UserProfile() { CreatedAt = DateTime.UtcNow, Description = "Just in test!", IconData = data, Login = "xamarin@gmail.com" });
            repos.Dispose();
            //ASSERT
            Assert.IsNotType<BadRequestResult>(userCreating);
        }
    }
}
