using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.FakeData.AspNetClientMenuData;
using WS.FakeData.AspNetClientModuleData;

namespace WS.FakeData.AspNetClientData
{
    public class AspNetClientNovoFaker : Faker<AspNetClientNovo>
    {
        public AspNetClientNovoFaker()
        {
            var orgao = new Faker().Random.Number(1324, 7925);

            RuleFor(pk => pk.Orgao, fk => orgao);
            RuleFor(pk => pk.RazaoSocial, fk => fk.Company.CompanyName());
            RuleFor(pk => pk.Server, fk => fk.Internet.IpAddress().ToString());
            RuleFor(pk => pk.Banco, "DBGM0" + orgao);
            RuleFor(pk => pk.Vencimento, fk => fk.Date.Between(fk.Date.Past(100), fk.Date.Recent(100)));
            RuleFor(pk => pk.aspNetClientModules, fk => new AspNetClientModuleNovoFaker().Generate(5));
            RuleFor(pk => pk.aspNetClientMenus, fk => new AspNetClientMenuNovoFaker().Generate(5));
        }
    }
}
