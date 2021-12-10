using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;

namespace WS.FakeData.AspNetClientMenuData
{
    public class AspNetClientMenuFaker : Faker<AspNetClientMenu>
    {
        public AspNetClientMenuFaker(int ClientId, int ModuleId)
        {
            RuleFor(mn => mn.ClientId, fk => ClientId);
            RuleFor(mn => mn.ModuleId, fk => ModuleId);
            RuleFor(mn => mn.MenuId, fk => fk.Random.Number(1, 999));
            RuleFor(mn => mn.Exibir, fk => fk.Random.Bool());
            RuleFor(mn => mn.Editar, fk => fk.Random.Bool());
            RuleFor(mn => mn.Excluir, fk => fk.Random.Bool());
            RuleFor(mn => mn.Inserir, fk => fk.Random.Bool());
        }
    }
}
