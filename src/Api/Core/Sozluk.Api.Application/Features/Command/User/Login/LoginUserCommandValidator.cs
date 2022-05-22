using FluentValidation;
using Sozluk.Common.ViewModels.RequestModels;

namespace Sozluk.Api.Application.Features.Command.User;

public class LoginUserCommandValidator:AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.EmailAddress).NotNull()
                                                    .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                                                    .WithMessage("{PropertyName} not a valid email address");
        
        RuleFor(x => x.Password).NotNull()
                                                       .MinimumLength(6)
                                                       .WithMessage("{PropertyName} should least be {MinLength} characters");

    }
}