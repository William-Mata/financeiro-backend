using Financeiro.Application.DTOs.Usuario;
using FluentValidation;

namespace Financeiro.Application.Validators.Usuario;

public class UsuarioAlteracaoValidator : AbstractValidator<UsuarioAtualizacaoDto>
{
    public UsuarioAlteracaoValidator()
    {
        RuleFor(x => x.UsuarioId).NotNull()
                            .WithMessage("Necessário o ID do usuário a ser atualizado.");

        RuleFor(x => x.Nome).NotEmpty()
                       .WithMessage("Necessário o nome a ser atualizado.");

        RuleFor(x => x.Email).NotEmpty()
                            .EmailAddress()
                            .WithMessage("Necessário um E-mail válido.");

        RuleFor(x => x.Senha).MinimumLength(10).ChildRules(senha =>
        {
            senha.RuleFor(x => x).Matches("[A-Z]").WithMessage("Senha deve conter ao menos uma letra maiúscula.");
            senha.RuleFor(x => x).Matches("[a-z]").WithMessage("Senha deve conter ao menos uma letra minúscula.");
            senha.RuleFor(x => x).Matches("[0-9]").WithMessage("Senha deve conter ao menos um número.");
            senha.RuleFor(x => x).Matches("[^a-zA-Z0-9]").WithMessage("Senha deve conter ao menos um caracter especial.");
        }).WithMessage("Senha deve ter no mínimo 10 caracteres.");
    }
}