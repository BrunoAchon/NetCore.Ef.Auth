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
using WS.FakeData.AspNetMenuData;
using WS.FakeData.AspNetModuleData;
using WS.Mananger.Interfaces.Repositories;
using Xunit;

namespace WS.Repository.Tests.Repository
{
    public class AspNetModuleRepositoryTest : IDisposable
    {
        private readonly IAspNetModuleRepository _aspNetModuleRepository;
        private readonly WsContext _contextTest;
        private readonly AspNetModule aspNetModule;

        private AspNetModuleFaker aspNetModuleFaker;

        public static WsContext GetWsContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<WsContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            var dbcontext = new WsContext(optionsBuilder);
            return dbcontext;
        }
        public AspNetModuleRepositoryTest()
        {
            _contextTest = GetWsContext();
            _aspNetModuleRepository = new AspNetModuleRepository(_contextTest);

            aspNetModuleFaker = new AspNetModuleFaker();
            aspNetModule = aspNetModuleFaker.Generate();
        }

        public async Task<List<AspNetModule>> InsertAspNetModule()
        {
            var aspNetModules = aspNetModuleFaker.Generate(20);
            foreach (var module in aspNetModules)
            {
                module.ModuleId = 0;
                await _contextTest.aspNetModules.AddAsync(module);

                List<AspNetMenu> aspNetMenus = await InsertAspNetMenu(module.ModuleId);
                module.aspNetMenus = aspNetMenus;
                await _contextTest.SaveChangesAsync();
            }
            return aspNetModules;
        }
        private async Task<List<AspNetMenu>> InsertAspNetMenu(int ModuleId)
        {
            var aspNetMenus = new AspNetMenuFaker(ModuleId).Generate(30);

            foreach (var menu in aspNetMenus)
            {
                menu.MenuId = 0;
                await _contextTest.aspNetMenus.AddAsync(menu);
                await _contextTest.SaveChangesAsync();
            }
            return aspNetMenus;
        }

        [Fact]
        public async Task GetAspNetModulesAsync_Found()
        {
            //Arrange
            var registers = await InsertAspNetModule();
            //Act
            var retorno = await _aspNetModuleRepository.GetAspNetModulesAsync();
            //Assert
            retorno.Should().HaveCount(registers.Count);
            retorno.First().aspNetMenus.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAspNetModulesAsync_NotFound()
        {
            //Arrange
            
            //Act
            var retorno = await _aspNetModuleRepository.GetAspNetModulesAsync();
            //Assert
            retorno.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetAspNetModuleAsync_Found()
        {
            //Arrange
            var registers = await InsertAspNetModule();
            //Act
            var retorno = await _aspNetModuleRepository.GetAspNetModuleAsync(registers.First().ModuleId);
            //Assert
            retorno.Module.Should().Be(registers.First().Module);
            retorno.ImgMenu.Should().Be(registers.First().ImgMenu);
            retorno.Ordem.Should().Be(registers.First().Ordem);

            retorno.aspNetMenus.Should().HaveCount(registers.First().aspNetMenus.Count);
        }

        [Fact]
        public async Task GetAspNetModuleAsync_NotFound()
        {
            //Arrange

            //Act
            var retorno = await _aspNetModuleRepository.GetAspNetModuleAsync(1);
            //Assert
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task InsertAspNetModuleAsync_Sucess()
        {
            //Arrange

            //Act
            var retorno = await _aspNetModuleRepository.InsertAspNetModuleAsync(aspNetModule);
            //Assert
            retorno.Should().BeEquivalentTo(aspNetModule);
        }

        [Fact]
        public async Task UpdateAspNetModuleAsync_Sucess()
        {
            //Arrange
            var registers = await InsertAspNetModule();
            var aspNetModuleAlterado = aspNetModuleFaker.Generate();

            //Act
            aspNetModuleAlterado.ModuleId = registers.First().ModuleId;
            var retorno = await _aspNetModuleRepository.UpdateAspNetModuleAsync(aspNetModuleAlterado);
            
            //Assert
            retorno.Should().BeEquivalentTo(aspNetModuleAlterado);
        }
        
        [Fact]
        public async Task UpdateAspNetModuleAsync_Include()
        {
            //Arrange
            await InsertAspNetModule();

            var aspNetModuleAlterado = await _contextTest.aspNetModules
                                               .Include(m => m.aspNetMenus)
                                               .AsNoTracking().FirstAsync();

            var aspNetMenu = await _contextTest.aspNetMenus
                                            .Where(p => !aspNetModuleAlterado
                                                            .aspNetMenus
                                                            .Select(i => i.MenuId)
                                                            .Contains(p.MenuId))
                                                .AsNoTracking()
                                                .FirstAsync();

            aspNetModuleAlterado.aspNetMenus.Add(aspNetMenu);
            
            //Act
            var retorno = await _aspNetModuleRepository.UpdateAspNetModuleAsync(aspNetModuleAlterado);

            //Assert
            retorno.aspNetMenus.Should().HaveCount(aspNetModuleAlterado.aspNetMenus.Count());
        }

        [Fact]
        public async Task UpdateAspNetModuleAsync_Remove()
        {
            //Arrange
            var registers = await InsertAspNetModule();
            var aspNetModuleAlterado = registers.First();

            //Act
            aspNetModuleAlterado.aspNetMenus.Remove(aspNetModuleAlterado.aspNetMenus.First());
            var retorno = await _aspNetModuleRepository.UpdateAspNetModuleAsync(aspNetModuleAlterado);
            
            //Assert
            retorno.Should().BeEquivalentTo(aspNetModuleAlterado);
        }

        [Fact]
        public async Task UpdateAspNetModuleAsync_RemoveAll()
        {
            //Arrange
            var registers = await InsertAspNetModule();
            var aspNetModuleAlterado = registers.First();
            
            //Act
            aspNetModuleAlterado.aspNetMenus.Clear();
            var retorno = await _aspNetModuleRepository.UpdateAspNetModuleAsync(aspNetModuleAlterado);
            
            //Assert
            retorno.Should().BeEquivalentTo(aspNetModuleAlterado);
        }

        [Fact]
        public async Task UpdateAspNetModuleAsync_NotFound()
        {
            //Act
            var retorno = await _aspNetModuleRepository.UpdateAspNetModuleAsync(aspNetModule);
            
            //Assert
            retorno.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAspNetModuleAsync_Sucess()
        {
            //Arrange
            var registers = await InsertAspNetModule();
            
            //Act
            var retorno = await _aspNetModuleRepository.DeleteAspNetModuleAsync(registers.First().ModuleId);
            
            //Assert
            retorno.Should().BeEquivalentTo(registers.First());
        }

        [Fact]
        public async Task DeleteAspNetModuleAsync_NotFound()
        {
            //Act
            var retorno = await _aspNetModuleRepository.DeleteAspNetModuleAsync(1);
            
            //Assert
            retorno.Should().BeNull();
        }

        public void Dispose()
        {
            _contextTest.Database.EnsureDeleted();
        }
    }
}
