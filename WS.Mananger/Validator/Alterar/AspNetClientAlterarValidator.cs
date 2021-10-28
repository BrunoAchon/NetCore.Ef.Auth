using FluentValidation;
using WS.Core.Shared.ModelViews;
using WS.Mananger.Validator.Novo;

namespace WS.Mananger.Validator.Alterar
{
    public class AspNetClientAlterarValidator : AbstractValidator<AspNetClientAlterar>
    {
        public AspNetClientAlterarValidator()
        {
            RuleFor(x => x.ClientId).NotNull().NotEmpty().GreaterThan(0);
            Include(new AspNetClientNovoValidator());
        }
    }
}
