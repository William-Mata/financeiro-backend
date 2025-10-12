using System.ComponentModel.DataAnnotations;

namespace Financeiro.Domain.Enums;

public enum EStatusPermissao
{
    [Display(Name = "Inativa")]
    Inativa = 0,

    [Display(Name = "Ativa")]
    Ativa = 1,
}