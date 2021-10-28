using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using WS.Core.Shared.ModelViews.AspNetMenu;

namespace WS.Mananger.Validator.Novo
{
    public class AspNetMenuNovoValidator : AbstractValidator<AspNetMenuNovo>
    {
        public AspNetMenuNovoValidator()
        {
            RuleFor(x => x.Menu).NotNull().NotEmpty().MaximumLength(50).WithMessage("O menu não pode estar em branco");
            RuleFor(x => x.Ordem).NotNull().NotEmpty();
        }
    }
}
