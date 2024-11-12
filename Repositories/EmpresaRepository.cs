using GerenciadorDeEmpresas.Context;
using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeEmpresas.Repositories;

public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
{
    public EmpresaRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Empresa>> GetAllEmpresasAsync()
    {
        return await DbSet
          .Include(u => u.TipoEmpresa)  // Incluir o PerfilUsuario  
          .ToListAsync();                 // Retornar todos os usuários com as entidades relacionadas
    }


    public async Task<IEnumerable<Empresa>> GetPorTipoEmpresaAsync(int tipoEmpresaId)
    {
        return await DbSet
            .Include(x => x.TipoEmpresa)
            .Where(x => x.TipoEmpresaId == tipoEmpresaId)
            .ToListAsync();
    }
    public async Task<Empresa> GetEmpresaAsync(int id)
    {
        return await DbSet
            .Include(u => u.TipoEmpresa)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

}
