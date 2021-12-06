using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Domain;
using WS.Core.Shared.ModelViews.AspNetClientMenu;

namespace WS.FakeData.AspNetClientMenuData
{
    public class AspNetClientMenuViewFaker : Faker<AspNetClientMenuView>
    {
        public AspNetClientMenuViewFaker()
        {
            RuleFor(mn => mn.MenuId, fk => fk.Random.Number(1, 999));
            RuleFor(mn => mn.Exibir, fk => fk.Random.Bool());
            RuleFor(mn => mn.Editar, fk => fk.Random.Bool());
            RuleFor(mn => mn.Excluir, fk => fk.Random.Bool());
            RuleFor(mn => mn.Inserir, fk => fk.Random.Bool());
        }
    }
}
