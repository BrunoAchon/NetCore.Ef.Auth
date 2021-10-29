using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClient;

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


        //                public string  { get; set; }
        //public string Server { get; set; }
        //public string Banco { get; set; }
        //public DateTime? Vencimento { get; set; }
    }
    }
}
