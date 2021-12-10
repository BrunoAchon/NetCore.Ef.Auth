using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.FakeData.AspNetMenuData;

namespace WS.FakeData.AspNetModuleData
{
    public class AspNetModuleFaker : Faker<AspNetModule>
    {
        public AspNetModuleFaker()
        {
            var ModuleId = new Faker().Random.Number(1, 999);
            var ordem = new Faker().Random.Number(1, 50);

            RuleFor(pk => pk.ModuleId, fk => ModuleId);
            RuleFor(pk => pk.Module, fk => fk.Commerce.Product());
            RuleFor(pk => pk.ImgMenu, fk => "bi bi-" + fk.Image.LoremPixelUrl());
            RuleFor(pk => pk.Ordem, fk => ordem);
            // não fazer isso pois gera erro de Atachment
            //RuleFor(pk => pk.aspNetMenus, fk => new AspNetMenuFaker(ModuleId).Generate(2));
        }
    }
}
