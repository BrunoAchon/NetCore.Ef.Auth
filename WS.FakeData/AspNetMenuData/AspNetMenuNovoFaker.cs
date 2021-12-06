using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Shared.ModelViews.AspNetMenu;

namespace WS.FakeData.AspNetMenuData
{
    public class AspNetMenuNovoFaker : Faker<AspNetMenuNovo>
    {
        public AspNetMenuNovoFaker()
        {
            var ordem = new Faker().Random.Number(1, 50);

            RuleFor(pk => pk.Menu, fk => fk.Commerce.ProductMaterial());
            RuleFor(pk => pk.Ordem, fk => ordem);
        }
    }
}
