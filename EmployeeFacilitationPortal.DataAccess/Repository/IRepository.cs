using EmployeeFacilitationPortal.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeFacilitationPortal.DataRepository.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll();

        Task Add(TEntity entity);

        Task Remove(TEntity entity);

        Task Update(TEntity entity);
        Task<TEntity> RemoveById(int id);
    }
}
