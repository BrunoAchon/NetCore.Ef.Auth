using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Shared.ModelViews.AspNetModule;
using WS.FakeData.AspNetMenuData;

namespace WS.FakeData.AspNetModuleData
{
    public class AspNetModuleNovoFaker : Faker<AspNetModuleNovo>
    {
        public AspNetModuleNovoFaker()
        {
            var ordem = new Faker().Random.Number(1, 50);

            RuleFor(pk => pk.Module, fk => fk.Commerce.Product());
            RuleFor(pk => pk.ImgMenu, fk => "bi bi-" + fk.Image.LoremPixelUrl());
            RuleFor(pk => pk.Ordem, fk => ordem);
            RuleFor(pk => pk.aspNetMenus, fk => new AspNetMenuNovoFaker().Generate(5));
        }
    }
}
