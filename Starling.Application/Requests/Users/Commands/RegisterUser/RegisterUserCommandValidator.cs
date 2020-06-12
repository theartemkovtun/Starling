using System.IO;
using FluentValidation;
using Starling.Domain.Repositories.Contracts;

namespace Starling.Application.Requests.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IUserRepository userRepository)
        {
            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username is required")
                .NotEmpty().WithMessage("Username cannot be empty")
                .MustAsync(async (_, username, cancellationToken) =>
                    await userRepository.FirstOrDefaultAsync(username) == null
                ).WithMessage("Username is already used. Select another one");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required")
                .Matches("^(?=.*[A-Z])(?=.*[\\W])(?=.*[0-9])(?=.*[a-z]).{8,30}$")
                .WithMessage("Invalid password. Password requires 8 to 30 characters with at least one lowercase, one uppercase character and special symbol");

            RuleFor(x => x.PasswordRepeat)
                .NotNull().WithMessage("Password repeat is required")
                .NotEmpty().WithMessage("Password repeat cannot be empty")
                .Must((model, field) => model.Password == field).WithMessage("Passwords do not match");
            
            RuleFor(x => x.PrivateKeyPassword)
                .NotNull().WithMessage("Private key password is required")
                .Matches("^(?=.*[A-Z])(?=.*[\\W])(?=.*[0-9])(?=.*[a-z]).{8,30}$")
                .WithMessage("Invalid private key password. Password requires 8 to 30 characters with at least one lowercase, one uppercase character and special symbol");
            
            
            RuleFor(x => x.PrivateKeyPasswordRepeat)
                .NotNull().WithMessage("Private key password repeat is required")
                .NotEmpty().WithMessage("Private key password repeat cannot be empty")
                .Must((model, field) => model.PrivateKeyPassword == field).WithMessage("Private key passwords do not match");
        }
    }
}