using FluentValidation;
using Starling.Domain.Repositories.Contracts;
using Starling.Shared.Extensions;

namespace Starling.Application.Requests.Users.Commands.AuthorizeUser
{
    public class AuthorizeUserCommandValidator : AbstractValidator<AuthorizeUserCommand>
    {
        public AuthorizeUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(u => u.Username)
                .NotNull().WithMessage("Username is required")
                .NotEmpty().WithMessage("Username cannot be empty");

            RuleFor(u => u.Password)
                .NotNull().WithMessage("Password is required")
                .NotEmpty().WithMessage("Password cannot be empty");

            RuleFor(u => new {u.Username, u.Password})
                .MustAsync(async (model, _) => await userRepository.IsUserExists(model.Username, model.Password.Sha512()))
                .WithMessage("Invalid username or password");
        }
    }
}