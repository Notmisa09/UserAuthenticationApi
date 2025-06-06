using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>  
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres").When(x => !x.Name.IsNullOrEmpty());

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("El email no tiene un formato válido").When(x => !x.Email.IsNullOrEmpty());

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres").When(x => !x.Password.IsNullOrEmpty())
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula").When(x => !x.Password.IsNullOrEmpty())
                .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula").When(x => !x.Password.IsNullOrEmpty())
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número").When(x => !x.Password.IsNullOrEmpty())
                .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial").When(x => !x.Password.IsNullOrEmpty());
        }
    }
}
