using EmployeePro.Dal.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace EmployeePro.Dal.Providers.Interfaces;

public interface ICrudProvider<TEntity> where TEntity : BaseEntity
{
    DbSet<TEntity> Query();
    Task Create(TEntity entity);
    Task<TEntity?> GetById(Guid? id);
    Task<List<TEntity>> GetAll();
    Task Update(TEntity entity);
    Task Delete(Guid id);
}