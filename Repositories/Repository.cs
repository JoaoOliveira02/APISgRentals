using GerenciadorDeEmpresas.Context;
using GerenciadorDeEmpresas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeEmpresas.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;

    // Propriedade para simplificar o acesso a _context.Set<T>()
    protected DbSet<T> DbSet => _context.Set<T>();

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        if (id == null)
        {
            throw new ArgumentNullException(nameof(id), "O id não pode ser nulo");
        }
        var idEncontrado = await DbSet.FindAsync(id);

        return idEncontrado;
    }
    public async Task<T> CreateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula");
        }

        await DbSet.AddAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity), "A entidade não pode ser nula");
        }

        DbSet.Update(entity);
        return entity;
    }
    public async Task<T> DeleteAsync(T entity)
    {
       
        if (entity == null)
        {
            throw new ArgumentException("Entidade não encontrada.");
        }

        // Remover a entidade encontrada
        DbSet.Remove(entity);
        return entity;
    }
}
