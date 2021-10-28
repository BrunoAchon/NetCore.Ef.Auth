using FluentValidation;
using WS.Core.Shared.ModelViews.AspNetMenu;
using WS.Mananger.Validator.Novo;

namespace WS.Mananger.Validator.Alterar
{
    public class AspNetMenuAlterarValidator : AbstractValidator<AspNetMenuAlterar>
    {
        public AspNetMenuAlterarValidator()
        {
            RuleFor(x => x.MenuId).NotNull().NotEmpty().GreaterThan(0);
            Include(new AspNetMenuNovoValidator());
        }
    }
}
