using Domain.Commands;
using FluentValidation;

namespace WebApi.Validators.Commands
{
    public class AssignTaskCommandValidator : AbstractValidator<AssignTaskCommand>
    {
        public AssignTaskCommandValidator()
        {
            RuleFor(x => x.MemberId).NotNull().NotEmpty();
            RuleFor(x => x.TaskId).NotNull().NotEmpty();
        }
    }
}
