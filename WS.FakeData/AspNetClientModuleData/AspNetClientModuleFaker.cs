using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.FakeData.AspNetClientData;

namespace WS.FakeData.AspNetClientModuleData
{
    public class AspNetClientModuleFaker : Faker<AspNetClientModule>
    {
        public AspNetClientModuleFaker(int ClientId)
        {
            RuleFor(ml => ml.ClientId, fk => ClientId);
            RuleFor(ml => ml.ModuleId, fk => new Faker().Random.Number(1, 999));
            RuleFor(ml => ml.Vencimento, fk => fk.Date.Between(fk.Date.Past(100), fk.Date.Recent(100)));
        }
    }
}
