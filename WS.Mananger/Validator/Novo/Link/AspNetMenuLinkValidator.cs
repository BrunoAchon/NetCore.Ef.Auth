using FluentValidation;
using System.Threading.Tasks;
using WS.Core.Shared.ModelViews.AspNetClientMenu;
using WS.Mananger.Interfaces.Repositories;

namespace WS.Mananger.Validator.Novo.Link
{
    public class AspNetMenuLinkValidator : AbstractValidator<AspNetClientMenuNovo>
    {
        private readonly IAspNetMenuRepository _repository;
        public AspNetMenuLinkValidator(IAspNetMenuRepository repository)
        {
            _repository = repository;
            RuleFor(p => p.MenuId).NotEmpty().NotNull().GreaterThan(0)
                .MustAsync(async (id, cancelar) =>
                {
                    return await ExistsInBase(id);
                }).WithMessage("Menu não cadastrado");
        }

        private async Task<bool> ExistsInBase(int id)
        {
            return await _repository.ExistsAsync(id);
        }
    }
}
