using GerenciadorDeEmpresas.Context;
using GerenciadorDeEmpresas.Repositories;
using GerenciadorDeEmpresas.Repositories.Interfaces;

namespace GerenciadorDeEmpresas.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IEmpresaRepository EmpresaRepository {  get; private set; }
    public IUsuarioRepository UsuarioRepository { get; private set; }
    public ITipoEmpresaRepository TipoEmpresaRepository { get; private set; }
    public IPerfilUsuarioRepository PerfilUsuarioRepository { get; private set; }

    public UnitOfWork(AppDbContext context, IEmpresaRepository empresaRepository, IUsuarioRepository usuarioRepository, ITipoEmpresaRepository tipoEmpresaRepository, IPerfilUsuarioRepository perfilUsuarioRepository)
    {
        _context = context;
        EmpresaRepository = new EmpresaRepository(_context);
        UsuarioRepository = new UsuarioRepository(_context);
        TipoEmpresaRepository = new TipoEmpresaRepository(_context);
        PerfilUsuarioRepository = new PerfilUsuarioRepository(_context);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
