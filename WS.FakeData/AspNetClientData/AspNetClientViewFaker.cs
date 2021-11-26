using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.FakeData.AspNetClientMenuData;
using WS.FakeData.AspNetClientModuleData;

namespace WS.FakeData.AspNetClientData
{
    public class AspNetClientViewFaker : Faker<AspNetClientView>
    {
        public AspNetClientViewFaker()
        {
            var id = new Faker().Random.Number(1, 999);
            var orgao = new Faker().Random.Number(1324, 7925);

            RuleFor(pk => pk.ClientId, fk => id);
            RuleFor(pk => pk.Orgao, fk => orgao);
            RuleFor(pk => pk.RazaoSocial, fk => fk.Company.CompanyName());
            RuleFor(pk => pk.Server, fk => fk.Internet.IpAddress().ToString());
            RuleFor(pk => pk.Banco, "DBGM0" + orgao);
            RuleFor(pk => pk.Vencimento, fk => fk.Date.Between(fk.Date.Past(100),fk.Date.Recent(100)));
            RuleFor(pk => pk.aspNetClientModules, fk => new AspNetClientModuleLinkViewFaker().Generate(5));
            //RuleFor(pk => pk.aspNetClientMenus, fk=> new AspNetClientMenuLinkViewFaker().Generate(5));
        }
    }
}
