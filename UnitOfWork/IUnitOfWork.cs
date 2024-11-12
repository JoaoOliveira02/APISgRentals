using GerenciadorDeEmpresas.Repositories.Interfaces;

namespace GerenciadorDeEmpresas.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IEmpresaRepository EmpresaRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    ITipoEmpresaRepository TipoEmpresaRepository { get; }
    IPerfilUsuarioRepository PerfilUsuarioRepository { get; }
    Task<bool> SaveChangesAsync();
}
