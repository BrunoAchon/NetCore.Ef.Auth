using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.FakeData.AspNetMenuData
{
    public class AspNetMenuFaker : Faker<AspNetMenu>
    {
        public AspNetMenuFaker(int ModuleId)
        {
            var MenuId = new Faker().Random.Number(1, 999);

            RuleFor(pk => pk.ModuleId, fk => ModuleId);
            RuleFor(pk => pk.MenuId, fk => MenuId);
            RuleFor(pk => pk.Menu, fk => fk.Commerce.ProductMaterial());
            RuleFor(pk => pk.Ordem, fk => new Faker().Random.Number(1, 50));
        }
    }
}
