﻿using FluentValidation;

namespace Net5WebTemplate.Application.Auth.Commands.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty().WithMessage("Email field is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(v => v.Password)
                .NotEmpty().WithMessage("Password field is required.");
        }
    }
}
