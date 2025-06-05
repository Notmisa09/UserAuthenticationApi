using FluentValidation;

namespace UserAuthenticationApi.Core.Application.Feautures.Users.Commands.Create
{
    public class UserCommandValidator : AbstractValidator<AddUsersCommand>
    {
        public UserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre no puede estar vacío")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email no puede estar vacío")
                .EmailAddress().WithMessage("El email no tiene un formato válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña no puede estar vacía")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula")
                .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula")
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número")
                .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial");

            RuleFor(x => x.Phones)
                .NotNull().WithMessage("Debe incluir al menos un teléfono")
                .Must(phones => phones.Any()).WithMessage("Debe incluir al menos un teléfono");
        }
    }
}
