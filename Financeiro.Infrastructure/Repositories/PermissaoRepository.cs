using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Data;

namespace Financeiro.Infrastructure.Repositories;

public class PermissaoRepository : Repository<Permissao>, IPermissaoRepository
{
    public PermissaoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {  }
}