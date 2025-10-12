using Financeiro.Domain.Entities;
using Financeiro.Domain.Interfaces.Repositories;
using Financeiro.Infrastructure.Data;

namespace Financeiro.Infrastructure.Repositories;

public class TelaRepository : Repository<Tela>, ITelaRepository
{
    public TelaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) {  }
}