using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Data;

namespace Financeiro.Infrastructure.Repositories;

public class PerfilDeAcessoTelaPermissaoRepository : Repository<PerfilDeAcessoTelaPermissao>, IPerfilDeAcessoTelaPermissaoRepository
{
    public PerfilDeAcessoTelaPermissaoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {  }
}