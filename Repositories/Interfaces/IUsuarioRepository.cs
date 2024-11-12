using GerenciadorDeEmpresas.Models;

namespace GerenciadorDeEmpresas.Repositories.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
    Task<Usuario> GetUsuarioAsync(int id);
    Task<IEnumerable<Usuario>> GetUsuariosPorPerfil(int perfilUsuarioId);
    Task<IEnumerable<Usuario>> GetUsuariosPorEmpresa(int empresaId);
    Task<bool> CPFExistsAsync(string cpf, int empresaId);
}
