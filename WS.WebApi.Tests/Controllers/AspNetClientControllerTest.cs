using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetClient;
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

        public AspNetClientControllerTest()
        {
            _mananger = Substitute.For<IAspNetClientMananger>();
            _logger = Substitute.For<ILogger<AspNetClientController>>();
            _controller = new AspNetClientController(_mananger, _logger);
        }


        [Fact]
        public async Task Get_Ok() 
        {
            _mananger.GetAspNetClientsAsync().Returns(new List<AspNetClientView> { new AspNetClientView { RazaoSocial = "São Bernardo do Campo" } });

            var resultado = (ObjectResult) await _controller.Get();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);

        }
    }
}
