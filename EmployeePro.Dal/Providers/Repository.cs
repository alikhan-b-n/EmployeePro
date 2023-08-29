using EmployeePro.Dal.Entities.Abstract;
using EmployeePro.Dal.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeePro.Dal.Providers;

public class Repository<TEntity> : ICrudProvider<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public DbSet<TEntity> Query()
    {
        return _dbSet;
    }

    public async Task Create(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity?> GetById(Guid? id)
    {
        var entity = id.Equals(null) ? null : await _dbSet.FirstAsync(x => x.Id == id);
        return entity;
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task Update(TEntity entity)
    {
        var result = _dbSet.Any(x => x.Id == entity.Id)
            ? _context.Entry(entity).State = EntityState.Modified
            : throw new ArgumentException();
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _dbSet.Remove(await _dbSet.FirstAsync(x => x.Id == id));
        await _context.SaveChangesAsync();
    }
}