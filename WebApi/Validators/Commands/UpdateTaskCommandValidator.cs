using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Subject).NotEmpty();
            RuleFor(x => x.AssignedMemberId).NotNull();
        }
    }
}
