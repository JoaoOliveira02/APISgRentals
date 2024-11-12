using GerenciadorDeEmpresas.Models;

namespace GerenciadorDeEmpresas.Repositories.Interfaces;

public interface IEmpresaRepository : IRepository<Empresa>
{
    Task<IEnumerable<Empresa>> GetAllEmpresasAsync();
    Task<IEnumerable<Empresa>> GetPorTipoEmpresaAsync(int tipoEmpresaId);
    Task<Empresa> GetEmpresaAsync(int id);
}
