using GerenciadorDeEmpresas.Context;
using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.Repositories.Interfaces;

namespace GerenciadorDeEmpresas.Repositories;

public class PerfilUsuarioRepository : Repository<PerfilUsuario>, IPerfilUsuarioRepository
{
    public PerfilUsuarioRepository(AppDbContext context) : base(context)
    {
    }
}
