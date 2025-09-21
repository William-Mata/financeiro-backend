using System.ComponentModel.DataAnnotations;

namespace Financeiro.Domain.Enums;

public enum StatusUsuario
{
    [Display(Name = "Inativo")]
    Inativo = 0,

    [Display(Name = "Ativo")]
    Ativo = 1,
}