using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Shared.ModelViews.AspNetMenu;

namespace WS.FakeData.AspNetMenuData
{
    public class AspNetMenuViewFaker : Faker<AspNetMenuView>
    {

        public AspNetMenuViewFaker()
        {
            var id = new Faker().Random.Number(1, 999);
            var ordem = new Faker().Random.Number(1, 50);

            RuleFor(pk => pk.MenuId, fk => id);
            RuleFor(pk => pk.Menu, fk => fk.Commerce.ProductMaterial());
            RuleFor(pk => pk.Ordem, fk => ordem);
        }
    }
}
