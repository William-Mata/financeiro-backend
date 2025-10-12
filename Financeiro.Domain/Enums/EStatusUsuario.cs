using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Financeiro.Domain.Enums;

public enum EStatusUsuario
{
    [Display(Name = "Inativo")]
    [Description("Inativo")]
    Inativo = 0,

    [Display(Name = "Ativo")]
    [Description("Ativo")]
    Ativo = 1,

    [Display(Name = "Bloqueado")]
    [Description("Bloqueado")]
    Bloqueado = 2,
}