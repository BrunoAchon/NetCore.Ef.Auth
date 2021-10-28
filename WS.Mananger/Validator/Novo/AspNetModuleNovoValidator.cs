using FluentValidation;
using WS.Core.Shared.ModelViews.AspNetModule;

namespace WS.Mananger.Validator.Novo
{
    public class AspNetModuleNovoValidator : AbstractValidator<AspNetModuleNovo>
    {
        public AspNetModuleNovoValidator()
        {
            RuleFor(x => x.Module).NotNull().NotEmpty().MaximumLength(50).WithMessage("O Modulo não pode estar em branco");
            RuleFor(x => x.ImgMenu).MaximumLength(100);
            RuleFor(x => x.Ordem).NotNull().NotEmpty();
        }
    }
}
