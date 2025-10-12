using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Data;

namespace Financeiro.Infrastructure.Repositories;

public class PerfilDeAcessoRepository : Repository<PerfilDeAcesso>, IPerfilDeAcessoRepository
{
    public PerfilDeAcessoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {  }
}