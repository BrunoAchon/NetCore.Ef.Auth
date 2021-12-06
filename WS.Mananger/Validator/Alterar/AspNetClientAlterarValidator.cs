using FluentValidation;
using WS.Core.Shared.ModelViews.AspNetClient;
using WS.Mananger.Interfaces;
using WS.Mananger.Interfaces.Repositories;
using WS.Mananger.Validator.Novo;

namespace WS.Mananger.Validator.Alterar
{
    public class AspNetClientAlterarValidator : AbstractValidator<AspNetClientAlterar>
    {
        public AspNetClientAlterarValidator(IAspNetModuleRepository moduleRepository, IAspNetMenuRepository menuRepository)
        {
            RuleFor(x => x.ClientId).NotNull().NotEmpty().GreaterThan(0);
            Include(new AspNetClientNovoValidator(moduleRepository, menuRepository));
        }
    }
}
