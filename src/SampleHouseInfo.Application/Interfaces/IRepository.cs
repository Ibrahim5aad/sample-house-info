using SampleHouseInfo.Domain.Entities;
using System.Threading.Tasks;

namespace SampleHouseInfo.Application.Interfaces;


/// <summary>
/// Interface IRepository represents repositories that can
/// execute full CRUD operations.
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TId">The type of the id.</typeparam>
/// <seealso cref="SampleHouseInfo.Application.Interfaces.IReadOnlyRepository<TEntity, TId>" />
/// <seealso cref="SampleHouseInfo.Application.Interfaces.IReadOnlyRepository<T>" />
public interface IRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
  Task<TEntity> AddAsync(TEntity entity);
  Task UpdateAsync(TEntity entity);
  Task DeleteAsync(TEntity entity);
}
