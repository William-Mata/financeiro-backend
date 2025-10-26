using Financeiro.Application.DTOs.Usuario;
using FluentValidation;

namespace Financeiro.Application.Validators.Usuario;

public class UsuarioCadastroValidator : AbstractValidator<UsuarioCadastroDto>
{
    public UsuarioCadastroValidator()
    {
        RuleFor(x => x.Nome).NotEmpty()
                            .WithMessage("Forneça seu nome.");

        RuleFor(x => x.Email).NotEmpty()
                            .WithMessage("O e-mail é obrigatório.")
                            .EmailAddress()
                            .WithMessage("Forneça um e-mail válido.");

        RuleFor(x => x.Senha).NotEmpty()
                            .MinimumLength(10).WithMessage("Senha deve ter no mínimo 10 caracteres.")
                            .Matches("[A-Z]").WithMessage("Senha deve conter ao menos uma letra maiúscula.")
                            .Matches("[a-z]").WithMessage("Senha deve conter ao menos uma letra minúscula.")
                            .Matches("[0-9]").WithMessage("Senha deve conter ao menos um número.")
                            .Matches("[^a-zA-Z0-9]").WithMessage("Senha deve conter ao menos um caracter especial.");
    }
}