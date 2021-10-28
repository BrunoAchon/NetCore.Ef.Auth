using FluentValidation;
using WS.Core.Shared.ModelViews.AspNetModule;
using WS.Mananger.Validator.Novo;

namespace WS.Mananger.Validator.Alterar
{
    public class AspNetModuleAlterarValidator : AbstractValidator<AspNetModuleAlterar>
    {
        public AspNetModuleAlterarValidator()
        {
            RuleFor(x => x.ModuleId).NotNull().NotEmpty().GreaterThan(0);
            RuleFor(x => x.Module).NotNull().NotEmpty().MaximumLength(50).WithMessage("O modulo não pode estar em branco");
            RuleFor(x => x.ImgMenu).MaximumLength(100);
            RuleFor(x => x.Ordem).NotNull().NotEmpty();
        }
    }
}
