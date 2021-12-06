using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Shared.ModelViews.AspNetClientModule;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.FakeData.AspNetClientModuleData
{
    public class AspNetClientModuleViewFaker : Faker<AspNetClientModuleView>
    {
        public AspNetClientModuleViewFaker()
        {
            RuleFor(ml => ml.ModuleId, fk => fk.Random.Number(1, 999));
            RuleFor(ml => ml.Vencimento, fk => fk.Date.Between(fk.Date.Past(100), fk.Date.Recent(100)));
        }
    }
}
