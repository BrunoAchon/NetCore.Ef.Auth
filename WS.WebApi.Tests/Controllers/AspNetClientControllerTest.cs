using WS.Core.Shared.ModelViews.AspNetClient;
using WS.FakeData.AspNetClientData;
using WS.Mananger.Interfaces.Managers;
using WS.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace WS.WebApi.Tests.Controllers
{
    public class AspNetClientControllerTest
    {
        private readonly IAspNetClientManager _manager;
        private readonly ILogger<AspNetClientController> _logger;
        private readonly AspNetClientController _controller;

        private readonly List<AspNetClientView> listAspNetClient;

        private readonly AspNetClientView aspNetClientView;
        private readonly AspNetClientNovo aspNetClientNovo;

        public AspNetClientControllerTest()
        {
            _manager = Substitute.For<IAspNetClientManager>();
            _logger = Substitute.For<ILogger<AspNetClientController>>();
            _controller = new AspNetClientController(_manager, _logger);

            aspNetClientView = new AspNetClientViewFaker().Generate();
            listAspNetClient = new AspNetClientViewFaker().Generate(100);

            aspNetClientNovo = new AspNetClientNovoFaker().Generate();
        }


        [Fact]
        public async Task Get_Ok()
        {
            // Arranje - Criando o controle de comparação com base no model de produção
            var controll = new List<AspNetClientView>();
            listAspNetClient.ForEach(p => controll.Add((AspNetClientView)p.Clone()));
            _manager.GetAspNetClientsAsync().Returns(listAspNetClient);

            // Act - Execução do processo
            var resultado = (ObjectResult)await _controller.Get();

            // Assert - comparando o ambiente de teste com o de produção para validar a integridade do software
            await _manager.Received().GetAspNetClientsAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controll);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            _manager.GetAspNetClientsAsync().Returns(new List<AspNetClientView>());

            var resultado = (StatusCodeResult)await _controller.Get();

            await _manager.Received().GetAspNetClientsAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            _manager.GetAspNetClientAsync(Arg.Any<int>()).Returns((AspNetClientView)aspNetClientView.Clone());

            var resultado = (ObjectResult)await _controller.Get(aspNetClientView.ClientId);

            await _manager.Received().GetAspNetClientAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(aspNetClientView);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _manager.GetAspNetClientAsync(Arg.Any<int>()).Returns(new AspNetClientView());

            var resultado = (StatusCodeResult)await _controller.Get(1);

            await _manager.Received().GetAspNetClientAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            _manager.InsertAspNetClientAsync(Arg.Any<AspNetClientNovo>()).Returns((AspNetClientView)aspNetClientView.Clone());

            var resultado = (ObjectResult)await _controller.Post(aspNetClientNovo);

            await _manager.Received().InsertAspNetClientAsync(Arg.Any<AspNetClientNovo>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(aspNetClientView);
        }

        [Fact]
        public async Task Put_Ok()
        {
            _manager.UpdateAspNetClientAsync(Arg.Any<AspNetClientAlterar>()).Returns((AspNetClientView)aspNetClientView.Clone());

            var resultado = (ObjectResult)await _controller.Put(new AspNetClientAlterar());

            await _manager.Received().UpdateAspNetClientAsync(Arg.Any<AspNetClientAlterar>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(aspNetClientView);
        }

        [Fact]
        public async Task Put_NotFound()
        {
            _manager.UpdateAspNetClientAsync(Arg.Any<AspNetClientAlterar>()).ReturnsNull();

            var resultado = (StatusCodeResult)await _controller.Put(new AspNetClientAlterar());
            await _manager.Received().UpdateAspNetClientAsync(Arg.Any<AspNetClientAlterar>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            _manager.DeleteAspNetClientAsync(Arg.Any<int>()).Returns((AspNetClientView)aspNetClientView.Clone());

            var resultado = (StatusCodeResult)await _controller.Delete(1);

            await _manager.Received().DeleteAspNetClientAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            _manager.DeleteAspNetClientAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await _controller.Delete(11);

            await _manager.Received().DeleteAspNetClientAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
