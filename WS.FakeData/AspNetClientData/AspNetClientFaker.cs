using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.FakeData.AspNetClientMenuData;
using WS.FakeData.AspNetClientModuleData;

namespace WS.FakeData.AspNetClientData
{
    public class AspNetClientFaker : Faker<AspNetClient>
    {
        public AspNetClientFaker()
        {
            var clientId = new Faker().IndexFaker + 1;
            var orgao = new Faker().Random.Number(1324, 7925);

            RuleFor(pk => pk.ClientId, fk => clientId);
            RuleFor(pk => pk.Orgao, fk => orgao);
            RuleFor(pk => pk.RazaoSocial, fk => fk.Company.CompanyName());
            RuleFor(pk => pk.Server, fk => fk.Internet.IpAddress().ToString());
            RuleFor(pk => pk.Banco, "DBGM0" + orgao);
            RuleFor(pk => pk.Vencimento, fk => fk.Date.Between(fk.Date.Past(100), fk.Date.Recent(100)));
        }
    }
}
