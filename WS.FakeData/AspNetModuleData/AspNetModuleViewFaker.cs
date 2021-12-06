using Bogus;
using WS.Core.Shared.ModelViews.AspNetModule;
using WS.FakeData.AspNetMenuData;

namespace WS.FakeData.AspNetModuleData
{
    public class AspNetModuleViewFaker : Faker<AspNetModuleView>
    {
        public AspNetModuleViewFaker()
        {
            var id = new Faker().Random.Number(1, 999);
            var ordem = new Faker().Random.Number(1, 50);

            RuleFor(pk => pk.ModuleId, fk => id);
            RuleFor(pk => pk.Module, fk => fk.Commerce.Product());
            RuleFor(pk => pk.ImgMenu, fk => "bi bi-"+ fk.Image.LoremPixelUrl());
            RuleFor(pk => pk.Ordem, fk => ordem);
            RuleFor(pk => pk.aspNetMenus, fk => new AspNetMenuViewFaker().Generate(5));
        }
    }
}
