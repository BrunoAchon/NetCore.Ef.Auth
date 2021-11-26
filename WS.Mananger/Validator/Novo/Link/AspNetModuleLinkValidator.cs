using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetClientModule;
using WS.Mananger.Interfaces;

namespace WS.Mananger.Validator.Novo.Link
{
    public class AspNetModuleLinkValidator : AbstractValidator<AspNetClientModuleLinkNovo>
    {
        private readonly IAspNetModuleRepository _repository;
        public AspNetModuleLinkValidator(IAspNetModuleRepository repository)
        {
            _repository = repository;
            RuleFor(p => p.ModuleId).NotEmpty().NotNull().GreaterThan(0)
                .MustAsync(async (id, cancelar) =>
                {
                    return await ExistsInBase(id);
                }).WithMessage("Modulo não cadastrado");
        }

        private async Task<bool> ExistsInBase(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
