using FluentValidation;
using WS.Core.Shared.ModelViews;

namespace WS.Mananger.Validator.Novo
{
    public class AspNetClientNovoValidator : AbstractValidator<AspNetClientNovo>
    {
        public AspNetClientNovoValidator()
        {
            RuleFor(x => x.Orgao).NotNull().NotEmpty().WithMessage("O cliente não pode estar em branco");
            RuleFor(x => x.RazaoSocial).NotNull().NotEmpty().MinimumLength(2).MaximumLength(150);
            RuleFor(x => x.Server).NotNull().NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Banco).NotNull().NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Vencimento).NotNull().NotEmpty();
        }
    }
}
