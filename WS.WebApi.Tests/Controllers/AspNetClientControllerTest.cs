using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.FakeData.AspNetClientData;
using WS.Mananger.Interfaces;
using WS.WebApi.Controllers;
using Xunit;

namespace WS.WebApi.Tests.Controllers
{
    public class AspNetClientControllerTest
    {
        private readonly IAspNetClientMananger _mananger;
        private readonly ILogger<AspNetClientController> _logger;
        private readonly AspNetClientController _controller;
        private readonly List<AspNetClientView> listAspNetClient;

        public AspNetClientControllerTest()
        {
            _mananger = Substitute.For<IAspNetClientMananger>();
            _logger = Substitute.For<ILogger<AspNetClientController>>();
            _controller = new AspNetClientController(_mananger, _logger);

            listAspNetClient = new AspNetClientViewFaker().Generate(100);
        }


        [Fact]
        public async Task Get_Ok() 
        {
            _mananger.GetAspNetClientsAsync().Returns(listAspNetClient);

            var resultado = (ObjectResult) await _controller.Get();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(listAspNetClient);
        }
    }
}
