using GerenciadorDeEmpresas.Context;
using GerenciadorDeEmpresas.Models;
using GerenciadorDeEmpresas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeEmpresas.Repositories;

public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
    {
        return await DbSet
            .Include(u => u.PerfilUsuario)  // Incluir o PerfilUsuario
            .Include(u => u.Empresa)        // Incluir a Empresa
            .ToListAsync();                 // Retornar todos os usuários com as entidades relacionadas
    }
    public async Task<Usuario> GetUsuarioAsync(int id)
    {
        return await DbSet
           .Include(u => u.PerfilUsuario) // Incluir o PerfilUsuario
           .Include(u => u.Empresa) // Incluir a Empresa
           .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosPorPerfil(int perfilUsuarioId)
    {
        return await DbSet
            .Include(u => u.PerfilUsuario) // Incluir o PerfilUsuario
            .Include(u => u.Empresa) // Incluir a Empresa
            .Where(u => u.PerfilUsuarioId == perfilUsuarioId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosPorEmpresa(int empresaId)
    {
        return await DbSet
            .Include(u => u.PerfilUsuario) // Incluir o PerfilUsuario
            .Include(u => u.Empresa) // Incluir a Empresa
            .Where(u => u.EmpresaId == empresaId)
            .ToListAsync();
    }


    public async Task<bool> CPFExistsAsync(string cpf, int empresaId)
    {
        return await DbSet.AnyAsync(u => u.CPF == cpf && u.EmpresaId == empresaId);
    }

}
