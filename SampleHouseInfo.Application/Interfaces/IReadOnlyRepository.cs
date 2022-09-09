using SampleHouseInfo.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleHouseInfo.Application.Interfaces;


/// <summary>
/// Interface IReadOnlyRepository represents repositories 
/// that are not allowed to manipulate data.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IReadOnlyRepository<TEntity, TId> where TEntity : BaseEntity<TId>
{
  Task<TEntity> GetByIdAsync(TId id);
  Task<IReadOnlyList<TEntity>> GetAllAsync();
  Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize);

}
