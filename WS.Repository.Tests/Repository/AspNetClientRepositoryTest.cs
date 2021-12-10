using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Data.Context;
using WS.Data.Repository;
using WS.FakeData.AspNetClientData;
using WS.FakeData.AspNetClientMenuData;
using WS.FakeData.AspNetClientModuleData;
using WS.FakeData.AspNetMenuData;
using WS.FakeData.AspNetModuleData;
using WS.Mananger.Interfaces.Repositories;
using Xunit;

namespace WS.Repository.Tests.Repository
{
    public class AspNetClientRepositoryTest : IDisposable
    {
        private readonly IAspNetClientRepository _aspNetClientRepository;
        private readonly WsContext _contextTest;
        private readonly AspNetClient aspNetClient;

        private AspNetModuleRepositoryTest aspNetModuleRepositoryTest;
        private AspNetClientFaker aspNetClientFaker;

        public static WsContext GetWsContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbcontext = new WsContext(optionsBuilder);
            return dbcontext;
        }
        public AspNetClientRepositoryTest()
        {
            _contextTest = GetWsContext();
            _aspNetClientRepository = new AspNetClientRepository(_contextTest);

            aspNetClientFaker = new AspNetClientFaker();
            aspNetClient = aspNetClientFaker.Generate();

            aspNetModuleRepositoryTest = new AspNetModuleRepositoryTest();
        }

        private async Task<List<AspNetClient>> InsertAspNetClient()
        {
            var aspNetModules = await aspNetModuleRepositoryTest.InsertAspNetModule();
            var aspNetClients = aspNetClientFaker.Generate(5);
            foreach (var client in aspNetClients)
            {
                client.ClientId = 0;
                await _contextTest.aspNetClients.AddAsync(client);

                List<AspNetClientModule> aspNetClientModules = await InsertAspNetClientModule(client, aspNetModules);
                client.aspNetClientModules = aspNetClientModules;
                await _contextTest.SaveChangesAsync();
            }
            return aspNetClients;
        }
        private async Task<List<AspNetClientModule>> InsertAspNetClientModule(AspNetClient client, ICollection<AspNetModule> aspNetModules)
        {
            var aspNetClientModules = new AspNetClientModuleFaker(client.ClientId).Generate(5);

            foreach (var module in aspNetClientModules)
            {
                var random = new Random();
                module.ModuleId = random.Next(aspNetModules.First().ModuleId, aspNetModules.Last().ModuleId);
                // Cliente nao pode ter duas vezes o mesmo modulo
                while (_contextTest.aspNetClientModules.Contains(module))
                {
                    module.ModuleId = random.Next(aspNetModules.First().ModuleId, aspNetModules.Last().ModuleId);
                }
                await _contextTest.aspNetClientModules.AddAsync(module);

                List<AspNetClientMenu> aspNetClientMenus = await InsertAspNetClientMenu(client.ClientId, module.ModuleId, aspNetModules);
                client.aspNetClientMenus = aspNetClientMenus;
                await _contextTest.SaveChangesAsync();
            }
            return aspNetClientModules;
        }
        private async Task<List<AspNetClientMenu>> InsertAspNetClientMenu(int ClientId, int ModuleId, ICollection<AspNetModule> aspNetModules)
        {
            var aspNetClientMenus = new AspNetClientMenuFaker(ClientId, ModuleId).Generate(5);

            foreach (var menus in aspNetClientMenus)
            {
                var random = new Random();
                menus.MenuId = random.Next(aspNetModules.FirstOrDefault(md => md.ModuleId == ModuleId).aspNetMenus.First().MenuId,
                                           aspNetModules.LastOrDefault(md => md.ModuleId == ModuleId).aspNetMenus.Last().MenuId);
                // Cliente não pode ter duas vezes o mesmo menu
                while (_contextTest.aspNetClientMenus.Contains(menus))
                {
                    menus.MenuId = random.Next(aspNetModules.FirstOrDefault(md => md.ModuleId == ModuleId).aspNetMenus.First().MenuId,
                                               aspNetModules.LastOrDefault(md => md.ModuleId == ModuleId).aspNetMenus.Last().MenuId);
                }

                await _contextTest.aspNetClientMenus.AddAsync(menus);
                await _contextTest.SaveChangesAsync();
            }
            return aspNetClientMenus;
        }

        [Fact]
        public async Task GetAspNetClientsAsync_Found()
        {
            //Arrange
            var registers = await InsertAspNetClient();
            //Act
            var retorno = await _aspNetClientRepository.GetAspNetClientsAsync();
            //Assert
            retorno.Should().HaveCount(registers.Count);
            retorno.First().aspNetClientModules.Should().NotBeNull();
            retorno.First().aspNetClientMenus.Should().NotBeNull();
        }
        [Fact]
        public async Task GetAspNetClientsAsync_NotFound()
        {
            //Arrange

            //Act
            var retorno = await _aspNetClientRepository.GetAspNetClientsAsync();
            //Assert
            retorno.Should().HaveCount(0);
        }
        [Fact]
        public async Task GetAspNetClientAsync_Found()
        {
            //Arrange
            var registers = await InsertAspNetClient();
            //Act
            var retorno = await _aspNetClientRepository.GetAspNetClientAsync(registers.First().ClientId);
            //Assert
            retorno.Orgao.Should().Be(registers.First().Orgao);
            retorno.RazaoSocial.Should().Be(registers.First().RazaoSocial);
            retorno.Server.Should().Be(registers.First().Server);
            retorno.Banco.Should().Be(registers.First().Banco);
            retorno.Vencimento.Should().Be(registers.First().Vencimento);           

            //retorno.aspNetClientModules.Should().HaveCount(registers.First().aspNetClientModules.Count);
            //retorno.aspNetClientMenus.Should().HaveCount(registers.First().aspNetClientMenus.Count);
        }
        public void Dispose()
        {
            _contextTest.Database.EnsureDeleted();
        }
    }
}
