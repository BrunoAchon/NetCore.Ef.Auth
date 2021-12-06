using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetModule;
using WS.FakeData.AspNetModuleData;
using WS.Mananger.Interfaces.Managers;
using WS.WebApi.Controllers;
using Xunit;

namespace WS.WebApi.Tests.Controllers
{
    public class AspNetModuleControllerTest
    {
        private readonly IAspNetModuleManager _manager;
        private readonly ILogger<AspNetModuleController> _logger;
        private readonly AspNetModuleController _controller;

        private readonly List<AspNetModuleView> listAspNetModule;

        private readonly AspNetModuleView aspNetModuleView;
        private readonly AspNetModuleNovo aspNetModuleNovo;

        public AspNetModuleControllerTest()
        {
            _manager = Substitute.For<IAspNetModuleManager>();
            _logger = Substitute.For<ILogger<AspNetModuleController>>();
            _controller = new AspNetModuleController(_manager, _logger);

            aspNetModuleView = new AspNetModuleViewFaker().Generate();
            listAspNetModule = new AspNetModuleViewFaker().Generate(100);

            aspNetModuleNovo = new AspNetModuleNovoFaker().Generate();
        }

        [Fact]
        public async Task Get_Ok()
        {
            // Arranje - Criando o controle de comparação com base no model de produção
            var controll = new List<AspNetModuleView>();
            listAspNetModule.ForEach(p => controll.Add((AspNetModuleView)p.Clone()));
            _manager.GetAspNetModulesAsync().Returns(listAspNetModule);

            // Act - Execução do processo
            var resultado = (ObjectResult)await _controller.Get();

            // Assert - comparando o ambiente de teste com o de produção para validar a integridade do software
            await _manager.Received().GetAspNetModulesAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(controll);
        }

        [Fact]
        public async Task Get_NotFound()
        {
            _manager.GetAspNetModulesAsync().Returns(new List<AspNetModuleView>());

            var resultado = (StatusCodeResult)await _controller.Get();

            await _manager.Received().GetAspNetModulesAsync();
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task GetById_Ok()
        {
            _manager.GetAspNetModuleAsync(Arg.Any<int>()).Returns((AspNetModuleView)aspNetModuleView.Clone());

            var resultado = (ObjectResult)await _controller.Get(aspNetModuleView.ModuleId);

            await _manager.Received().GetAspNetModuleAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(aspNetModuleView);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _manager.GetAspNetModuleAsync(Arg.Any<int>()).Returns(new AspNetModuleView());

            var resultado = (StatusCodeResult)await _controller.Get(1);

            await _manager.Received().GetAspNetModuleAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Post_Created()
        {
            _manager.InsertAspNetModuleAsync(Arg.Any<AspNetModuleNovo>()).Returns((AspNetModuleView)aspNetModuleView.Clone());

            var resultado = (ObjectResult)await _controller.Post(aspNetModuleNovo);

            await _manager.Received().InsertAspNetModuleAsync(Arg.Any<AspNetModuleNovo>());
            resultado.StatusCode.Should().Be(StatusCodes.Status201Created);
            resultado.Value.Should().BeEquivalentTo(aspNetModuleView);
        }

        [Fact]
        public async Task Put_Ok()
        {
            _manager.UpdateAspNetModuleAsync(Arg.Any<AspNetModuleAlterar>()).Returns((AspNetModuleView)aspNetModuleView.Clone());

            var resultado = (ObjectResult)await _controller.Put(new AspNetModuleAlterar());

            await _manager.Received().UpdateAspNetModuleAsync(Arg.Any<AspNetModuleAlterar>());
            resultado.StatusCode.Should().Be(StatusCodes.Status200OK);
            resultado.Value.Should().BeEquivalentTo(aspNetModuleView);
        }

        [Fact]
        public async Task Put_NotFound()
        {
            _manager.UpdateAspNetModuleAsync(Arg.Any<AspNetModuleAlterar>()).ReturnsNull();

            var resultado = (StatusCodeResult)await _controller.Put(new AspNetModuleAlterar());
            await _manager.Received().UpdateAspNetModuleAsync(Arg.Any<AspNetModuleAlterar>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task Delete_NoContent()
        {
            _manager.DeleteAspNetModuleAsync(Arg.Any<int>()).Returns((AspNetModuleView)aspNetModuleView.Clone());

            var resultado = (StatusCodeResult)await _controller.Delete(1);

            await _manager.Received().DeleteAspNetModuleAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            _manager.DeleteAspNetModuleAsync(Arg.Any<int>()).ReturnsNull();

            var resultado = (StatusCodeResult)await _controller.Delete(11);

            await _manager.Received().DeleteAspNetModuleAsync(Arg.Any<int>());
            resultado.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
