using GerenciadorDeEmpresas.Context;
using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.Repositories.Interfaces;

namespace GerenciadorDeEmpresas.Repositories
{
    public class TipoEmpresaRepository : Repository<TipoEmpresa>, ITipoEmpresaRepository
    {
        public TipoEmpresaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
